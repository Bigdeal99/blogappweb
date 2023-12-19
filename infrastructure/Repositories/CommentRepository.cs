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

        public async Task<CommentFeedQuery> GetCommentByIdAsync(int Id)
        {
            string sql = @"
SELECT c.Id, c.Name, c.Email, c.Text, c.PublicationDate, c.BlogId, 
       b.Title as BlogTitle, b.Summary as BlogSummary
FROM blog_schema.Comment c
JOIN blog_schema.Blog b ON c.BlogId = b.Id
WHERE c.Id = @Id;";
            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                return await conn.QueryFirstOrDefaultAsync<CommentFeedQuery>(sql, new { Id = Id });
            }
        }

        public async Task<IEnumerable<CommentFeedQuery>> GetCommentsForFeedAsync()
        {
            string sql = @"
SELECT Id, Name, Email, Text, PublicationDate, BlogId
FROM blog_schema.Comment;";
            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                return await conn.QueryAsync<CommentFeedQuery>(sql);
            }
        }

        public async Task <bool>DeleteCommentAsync(int Id)
        {
            string sql = @"
DELETE FROM blog_schema.Comment
WHERE Id = @Id;";

            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                var rowsAffected = await conn.ExecuteAsync(sql, new { Id = Id });
                return rowsAffected > 0;
            }
        }
       


        public async Task<object?> UpdateCommentAsync(int Id, string Name, string Email, string Text, DateTime PublicationDate, int BlogId)
        {
            string sql = @"
UPDATE blog_schema.Comment 
SET Name = @Name, 
    Email = @Email, 
    Text = @Text, 
    PublicationDate = @PublicationDate, 
    BlogId = @BlogId, 
WHERE Id = @Id;";
            using (var conn = _dataSource.OpenConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<Comment>(sql, new { Id, Name, Email, Text, PublicationDate, BlogId});
            }        }
        
       

        public object? CreateCommentAsync(string Name, string Email, string Text, DateTime PublicationDate, int BlogId)
        {
            string sql = @"
    INSERT INTO blog_schema.Comment 
    (Name, Email, Text, PublicationDate, BlogId)
    VALUES (@Name, @Email, @Text, @PublicationDate,@BlogId)
    RETURNING Id;";
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<Comment>(sql, new { Name, Email, Text, PublicationDate, BlogId});
            }        }
    }
}
