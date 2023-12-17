using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;


namespace infrastructure.Repositories
{
    public class BlogRepository
    {
        private NpgsqlDataSource _dataSource;

        public BlogRepository(NpgsqlDataSource datasource)
        {
            _dataSource = datasource;
        }

        public async Task<IEnumerable<BlogFeedQuery>> GetBlogForFeedAsync()
        {
            const string sql = @"
SELECT Id, Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage
FROM blog_schema.Blog;";

            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                return await conn.QueryAsync<BlogFeedQuery>(sql);
            }
        }

        public async Task<BlogFeedQuery?> GetBlogByIdAsync(int id)
        {
            const string sql = @"
SELECT Id, Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage
FROM blog_schema.Blog
WHERE Id = @Id;";

            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                return await conn.QueryFirstOrDefaultAsync<BlogFeedQuery>(sql, new { Id = id });
            }
        }

        public async Task<int> CreateBlogAsync(Blog blog)
        {
            const string sql = @"
INSERT INTO blog_schema.Blog (Title, Summary, Content, PublicationDate, CategoryId, FeaturedImage)
VALUES (@Title, @Summary, @Content, @PublicationDate, @CategoryId, @FeaturedImage)
RETURNING Id;";

            using (var conn = await _dataSource.OpenConnectionAsync())
            {
                return await conn.ExecuteScalarAsync<int>(sql, blog);
            }
        }
    }
}
