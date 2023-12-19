using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;

        public AdminService(AdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            return await _adminRepository.GetAdminByIdAsync(id);
        }

        public async Task<bool> AuthenticateAdminAsync(string modelUsername, string modelPassword)
        {
            var admin = await _adminRepository.GetAdminByUsernameAsync(modelUsername);
            if (admin != null)
            {
                var hashedPassword = HashPassword(modelPassword);
                return admin.PasswordHash == hashedPassword;
            }

            return false;
        }

        public async Task<Admin> CreateAdminAsync(Admin admin)
        {
            admin.PasswordHash = HashPassword(admin.PasswordHash);
            return await _adminRepository.AddAdminAsync(admin);
        }

       

        public async Task DeleteAdminAsync(int id)
        {
            await _adminRepository.DeleteAdminAsync(id);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
