using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories
{
    public class AdminRepository
    {
        private NpgsqlDataSource _dataSource;

        public AdminRepository(NpgsqlDataSource datasource)
        {
            _dataSource = datasource;
        }

        public async Task<Admin> GetAdminByIdAsync(int Id)
        {
            string sql = @"
SELECT Id, Username, PasswordHash
FROM blog_schema.User
WHERE Id = @Id;";

            using (var conn = _dataSource.OpenConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<Admin>(sql, new { Id });
            }
        }

        public async Task<Admin> GetAdminByUsernameAsync(string Username)
        {
            string sql = @"
SELECT Id, Username, PasswordHash
FROM blog_schema.User
WHERE Username = @Username;";

            using (var conn = _dataSource.OpenConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<Admin>(sql, new { Username });
            }
        }

        public async Task<Admin> AddAdminAsync(Admin Admin)
        {
            string sql = @"
INSERT INTO blog_schema.User (Username, PasswordHash)
VALUES (@Username, @PasswordHash)
RETURNING Id;";

            using (var conn = _dataSource.OpenConnection())
            {
                Admin.Id = await conn.ExecuteScalarAsync<int>(sql, Admin);
                return Admin;
            }
        }

        

        public async Task DeleteAdminAsync(int Id)
        {
            string sql = @"
DELETE FROM blog_schema.User
WHERE Id = @Id;";

            using (var conn = _dataSource.OpenConnection())
            {
                await conn.ExecuteAsync(sql, new { Id });
            }
        }
    }
}
