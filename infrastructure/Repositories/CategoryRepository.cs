using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class CategoryRepository
{
    private NpgsqlDataSource _dataSource;

    public CategoryRepository(NpgsqlDataSource datasource)
    {
        _dataSource = datasource;
    }

    public async Task<Category> GetBlogsByCategoryIdAsync(int Id)
    {
        string sql = @"
SELECT c.*, b.*
FROM blog_schema.Category c
LEFT JOIN blog_schema.Blog b ON c.Id = b.CategoryId
WHERE c.Id = @Id;";

        using (var conn = _dataSource.OpenConnection())
        {
            var categoryDictionary = new Dictionary<int, Category>();

            var categories = await conn.QueryAsync<Category, Blog, Category>(
                sql,
                (category, blog) =>
                {
                    if (!categoryDictionary.TryGetValue(category.Id, out var currentCategory))
                    {
                        currentCategory = category;
                        currentCategory.Blog = new List<Blog>();
                        categoryDictionary.Add(currentCategory.Id, currentCategory);
                    }

                    if (blog != null && !currentCategory.Blog.Any(b => b.Id == blog.Id))
                    {
                        currentCategory.Blog.Add(blog);
                    }

                    return currentCategory;
                },
                new { Id },
                splitOn: "Id" // Assuming 'Id' is the first column of both Category and Blog tables
            );

            return categories.FirstOrDefault();
        }

    }


   

    public Category UpdateCategoryAsync(int Id, string Name, string Description)
    {
        string sql = @"
INSERT INTO blog_schema.Category (Name, Description)
VALUES (@Name, @Description)
RETURNING *;";
        using (var conn = _dataSource.OpenConnection())
        {

            return conn.QueryFirst<Category>(sql, new { Id, Name, Description });
        }
    }

    public bool DoesCategorytWithNameExist(string Name)
    {
        var sql = @"SELECT COUNT(*) FROM blog_schema.Category WHERE Name = @Name;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { Name }) == 1;
        }    }

    public object? CreateCategoryAsync(string Name, string Description)
    {
    {
        string sql = @"
    INSERT INTO blog_schema.Category 
    (Name, Description)
    VALUES (@Name, @Description)
    RETURNING Id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Category>(sql, new { Name, Description});
        }    }    }


    public async Task<bool> DeleteCategoryAsync(int Id)
    {
        string sql = @"
DELETE FROM blog_schema.Category 
WHERE Id = @Id;";

        using (var conn = await _dataSource.OpenConnectionAsync())
        {
            var rowsAffected = await conn.ExecuteAsync(sql, new { Id = Id });
            return rowsAffected > 0;
        }    }
}
