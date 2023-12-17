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
   
    public async Task<bool> UpdateBlogAsync(Blog blog)
    {
        var sql = @"
UPDATE blog_schema.Blog
SET Title = @Title,
    Summary = @Summary,
    Content = @Content,
    PublicationDate = @PublicationDate,
    CategoryId = @CategoryId,
    FeaturedImage = @FeaturedImage
WHERE Id = @Id;";

        using (var conn = await _dataSource.OpenConnectionAsync())
        {
            var affected = await conn.ExecuteAsync(sql, blog);
            return affected > 0;
        }
    }
    public async Task<bool> DeleteBlogAsync(int id)
    {
        var sql = "DELETE FROM blog_schema.Blog WHERE Id = @Id;";

        using (var conn = await _dataSource.OpenConnectionAsync())
        {
            var affected = await conn.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }
    }

    
}