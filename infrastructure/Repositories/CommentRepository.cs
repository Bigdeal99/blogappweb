
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class CommentRepository
{
    private NpgsqlDataSource _dataSource;

    public CommentRepository(NpgsqlDataSource datasource)
    {
        _dataSource = datasource;
    }
   
    public async Task<BlogFeedQuery.CommentFeedQuery> GetCommentByIdAsync(int Id)
    {
        string sql = $@"
SELECT Id as {nameof(Comment.Id)},
       Name as {nameof(Comment.Name)},
       Email as {nameof(Comment.Email)},
       Text as {nameof(Comment.Text)},
        PublicationDate as {nameof(Comment.PublicationDate)},
       BlogPostId as {nameof(Comment.BlogPostId)}
FROM blog_schema.Comment
WHERE Id = @Id;";
        using (var conn = await _dataSource.OpenConnectionAsync())
        {
            return await conn.QueryFirstOrDefaultAsync<BlogFeedQuery.CommentFeedQuery>(sql, new { Id = Id });
        }
    }


    public async Task<IEnumerable<BlogFeedQuery.CommentFeedQuery>> GetCommenForFeedAsync()
    {
        string sql = $@"
SELECT Id as {nameof(Comment.Id)},
       Name as {nameof(Comment.Name)},
       Email as {nameof(Comment.Email)},
       Text as {nameof(Comment.Text)},
        PublicationDate as {nameof(Comment.PublicationDate)},
       BlogPostId as {nameof(Comment.BlogPostId)}
FROM blog_schema.Comment
WHERE Id = @Id;";
        using (var conn = await _dataSource.OpenConnectionAsync())
        {
            return await conn.QueryAsync<BlogFeedQuery.CommentFeedQuery>(sql);
        }
    }

    public async Task DeleteCommentAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public bool DoesCommenttWithNameExist(string name)
    {
        throw new NotImplementedException();
    }


    public object? CreateCommentAsync(string name, string email, string text, DateTime publicationDate, int blogPostId)
    {
        throw new NotImplementedException();
    }

    public object? UpdateCommentAsync(int id, string name, string email, string text, DateTime publicationDate, int blogPostId)
    {
        throw new NotImplementedException();
    }
}