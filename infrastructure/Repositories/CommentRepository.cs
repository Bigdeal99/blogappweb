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
        public IEnumerable<CommentFeedQuery> GetCommentsForFeedAsync()
        {
            string sql = $@"
SELECT Id as {nameof(CommentFeedQuery.Id)}, 
       Name as{nameof(CommentFeedQuery.Name)}, 
       Email as{nameof(CommentFeedQuery.Email)}, 
       Text as{nameof(CommentFeedQuery.Text)}, 
       PublicationDate as{nameof(CommentFeedQuery.PublicationDate)}, 
        BlogId as {nameof(CommentFeedQuery.BlogId)}
FROM blog_schema.Comment;
";
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.Query<CommentFeedQuery>(sql);
            }
        }
        public async Task<Comment> GetCommentByIdAsync(int Id)
        {
            string sql = @"
SELECT c.Id, c.Name, c.Email, c.Text, c.PublicationDate, c.BlogId, 
       b.Title as BlogTitle, b.Summary as BlogSummary
FROM blog_schema.Comment c
JOIN blog_schema.Blog b ON c.BlogId = b.Id
WHERE c.Id = @Id;";
            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                return await conn.QueryFirstOrDefaultAsync<Comment>(sql, new { Id = Id });
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
       


        public Comment UpdateCommentAsync(int Id, string Name, string Email, string Text, DateTime PublicationDate, int BlogId)
        {
            var sql = $@"
UPDATE blog_schema.Comment SET Name = @Name, Email = @Email, Text = @Text, PublicationDate = @PublicationDate, BlogId = @BlogId, 
WHERE Id = @Id
RETURNING Id as {nameof(Comment.Id)}, 
       Name as{nameof(Comment.Name)}, 
       Email as{nameof(Comment.Email)}, 
       Text as{nameof(Comment.Text)}, 
       PublicationDate as{nameof(Comment.PublicationDate)}, 
        BlogId as {nameof(CommentFeedQuery.BlogId)}
";
            using (var conn = _dataSource.OpenConnection())
            {
                return  conn.QueryFirst<Comment>(sql, new { Id, Name, Email, Text, PublicationDate, BlogId});
            }
            
        }
       
       

        public Comment CreateCommentAsync(string Name, string Email, string Text, DateTime PublicationDate, int BlogId)
        {
            var sql = $@"
    INSERT INTO blog_schema.Comment (Name, Email, Text, PublicationDate, BlogId)
    VALUES (@Name, @Email, @Text, @PublicationDate,@BlogId)
    RETURNING Id as {nameof(Comment.Id)}, 
       Name as{nameof(Comment.Name)}, 
       Email as{nameof(Comment.Email)}, 
       Text as{nameof(Comment.Text)}, 
       PublicationDate as{nameof(Comment.PublicationDate)}, 
        BlogId as {nameof(CommentFeedQuery.BlogId)}
        ";
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<Comment>(sql, new {  Name, Email, Text, PublicationDate, BlogId});
            }
        }

        
    }
}


