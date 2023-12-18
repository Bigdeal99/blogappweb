using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories
{
    public class CommentRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public CommentRepository(NpgsqlDataSource datasource)
        {
            _dataSource = datasource;
        }

        public async Task<BlogFeedQuery.CommentFeedQuery> GetCommentByIdAsync(int Id)
        {
            string sql = @"
SELECT Id, Name, Email, Text, PublicationDate, BlogPostId
FROM blog_schema.Comment
WHERE Id = @Id;";
            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                return await conn.QueryFirstOrDefaultAsync<BlogFeedQuery.CommentFeedQuery>(sql, new { Id = Id });
            }
        }

        public async Task<IEnumerable<BlogFeedQuery.CommentFeedQuery>> GetCommentsForFeedAsync()
        {
            string sql = @"
SELECT Id, Name, Email, Text, PublicationDate, BlogPostId
FROM blog_schema.Comment;";
            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                return await conn.QueryAsync<BlogFeedQuery.CommentFeedQuery>(sql);
            }
        }
        public async Task DeleteCommentAsync(int Id)
        {
            string sql = @"
DELETE FROM blog_schema.Comment
WHERE Id = @id;";

            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                await conn.ExecuteAsync(sql, new { Id });
            }
        }
        

        public object? CreatecommentAsync(string Name, string Email, string Text, DateTime PublicationDate,
            int BlogPostId)
        {
            string sql = @"
INSERT INTO blog_schema.Comment (Name, Email, Text, PublicationDate, BlogPostId)
VALUES (@Name, @Email, @Text, @PublicationDate, @BlogPostId);";

            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<Comment>(sql, new { Name, Email, Text, PublicationDate, BlogPostId });
            }

        }

        public object? UpdateCommentAsync(string Name, string Email, string Text, DateTime PublicationDate,
            int BlogPostId)
        {
            string sql = @"
UPDATE blog_schema.Comment
SET Name = @Name, Email = @Email, Text = @Text, PublicationDate = @PublicationDate, BlogPostId = @BlogPostId
WHERE Id = @Id;";

            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<Comment>(sql, new { Name, Email, Text, PublicationDate, BlogPostId });

            }
        }

       
    }
}


        
