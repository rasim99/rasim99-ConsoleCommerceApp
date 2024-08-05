
using Core.Constants;
using Core.Entities;
using Data.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Data;

public static class DbInitializer
{
    private static readonly AppDbContext _context;
    static DbInitializer()
    {
        _context = new AppDbContext();
    }
    public static void SeedData()
    {
        SeedAdmin();
    }
    private static void SeedAdmin()
    {
        if (!_context.Admins.Any())
        {
            Admin admin = new Admin()
            {
                Name = "Jhon",
                Surname ="Doe",
                Email="admin.jhon@gmail.com",
                CreateAt = DateTime.Now
            };
            PasswordHasher<Admin> passwordHasher = new();
            admin.Password = passwordHasher.HashPassword(admin, "a1b2c3d4");
            _context.Admins.Add(admin);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                Messages.ErrorOccuredMessage();

            }
        }
    }
}
