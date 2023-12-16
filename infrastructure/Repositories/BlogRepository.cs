using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class BlogRepository
{
    private NpgsqlDataSource _dataSource;

    public BlogRepository(NpgsqlDataSource datasource)
    {
        _dataSource = datasource;
    }

    public async Task<IEnumerable<BlogFeedQuery>> GetBlogForFeedAsync()
    {
        string sql = $@"
SELECT Id as {nameof(Blog.Id)},
       Title as {nameof(Blog.Title)},
       Summary as {nameof(Blog.Summary)},
       Content as {nameof(Blog.Content)},
       PublicationDate as {nameof(Blog.PublicationDate)},
       CategoryId as {nameof(Blog.CategoryId)},
       FeaturedImage as {nameof(Blog.FeaturedImage)}
FROM blog_schema.Blog;";
        using (var conn = await _dataSource.OpenConnectionAsync())
        {
            return await conn.QueryAsync<BlogFeedQuery>(sql);
        }
    }

    public async Task<BlogFeedQuery?> GetBlogByIdAsync(int id)
    {
        string sql = $@"
SELECT Id as {nameof(Blog.Id)},
       Title as {nameof(Blog.Title)},
       Summary as {nameof(Blog.Summary)},
       Content as {nameof(Blog.Content)},
       PublicationDate as {nameof(Blog.PublicationDate)},
       CategoryId as {nameof(Blog.CategoryId)},
       FeaturedImage as {nameof(Blog.FeaturedImage)}
FROM blog_schema.Blog
WHERE Id = @Id;";

        using (var conn = await _dataSource.OpenConnectionAsync())
        {
            return await conn.QueryFirstOrDefaultAsync<BlogFeedQuery>(sql, new { BlogId = id });
        }
    }
}