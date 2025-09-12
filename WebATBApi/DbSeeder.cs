using AutoMapper;
using Core.Interfaces;
using Domain;
using Domain.Constants;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebATBApi
{
    public static class DbSeeder
    {
        public static async Task SeedData(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            //Цей об'єкт буде верта посилання на конткетс, який зараєстрвоано в Progran.cs
            var context = scope.ServiceProvider.GetRequiredService<AppDbAtbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

            context.Database.Migrate();

            await SeedRoles(context, roleManager);
            
        }

        private static async Task SeedRoles(AppDbAtbContext context, RoleManager<RoleEntity> roleManager)
        {
            if (!context.Roles.Any())
            {
                foreach (var role in Roles.AllRoles)
                {
                    var result = await roleManager.CreateAsync(new(role));
                    if (!result.Succeeded)
                    {
                        Console.WriteLine("Error Create Role {0}", role);
                    }
                }
            }
        }
    }
}
