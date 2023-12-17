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
    
    
    public async Task<bool> DeleteBlogAsync(int Id)
    {
        string sql = @"
DELETE FROM blog_schema.Blog 
WHERE Id = @Id;";

        using (var conn = await _dataSource.OpenConnectionAsync())
        {
            var rowsAffected = await conn.ExecuteAsync(sql, new { Id = Id });
            return rowsAffected > 0;
        }
    }


    public async Task<object?> fddf(int blog, string updateBlogDto, string dtoSummary, string dtoContent, DateTime dtoPublicationDate, int dtoCategoryId)
    {
        string sql = @"
UPDATE blog_schema.Blog 
SET Title = @Title, 
    Summary = @Summary, 
    Content = @Content, 
    PublicationDate = @PublicationDate, 
    CategoryId = @CategoryId, 
    FeaturedImage = @FeaturedImage
WHERE Id = @Id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return await conn.QueryFirstOrDefaultAsync<Blog>(sql, new { blog, updateBlogDto, dtoSummary, dtoContent, dtoPublicationDate, dtoCategoryId});
        }
    }

    public object? CreateBlogAsync(string Title, string Summary, string Content, DateTime PublicationDate, int CategoryId, string FeaturedImage)
    {
        string sql = @"
    INSERT INTO blog_schema.Blog 
    (Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage)
    VALUES (@Title, @Summary, @Content, @PublicationDate, @CategoryId, @FeaturedImage)
    RETURNING Id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Blog>(sql, new { Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage});
        }    }

    public bool DoesBlogtWithNameExist(string Title)
    {
        var sql = @"SELECT COUNT(*) FROM blog_schema.Blog WHERE Title = @Title;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { Title }) == 1;
        }    }

    public object? UpdateBlogAsync(int Id, string Title, string Summary, string Content, DateTime PublicationDate, int CategoryId, string FeaturedImage)
    {
        string sql = @"
    INSERT INTO blog_schema.Blog 
    (Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage)
    VALUES (@Title, @Summary, @Content, @PublicationDate, @CategoryId, @FeaturedImage)
    RETURNING Id;";
        
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Blog>(sql, new { Id, Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage});
        }
        
        
    }
}