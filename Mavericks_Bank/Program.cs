using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Repositories;
using Mavericks_Bank.Services;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MavericksBankContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionString"));
            });

            builder.Services.AddScoped<IRepository<string,Validation>,ValidationRepository>();
            builder.Services.AddScoped<IRepository<int, Customers>, CustomersRepository>();
            builder.Services.AddScoped<IRepository<int, BankEmployees>, BankEmployeesRepository>();
            builder.Services.AddScoped<IRepository<int, Admin>, AdminRepository>();
            builder.Services.AddScoped<IRepository<int, Banks>, BanksRepository>();
            builder.Services.AddScoped<IRepository<string, Branches>, BranchesRepository>();
            builder.Services.AddScoped<IRepository<long, Accounts>, AccountsRepository>();
            builder.Services.AddScoped<IRepository<long, Beneficiaries>, BeneficiariesRepository>();
            builder.Services.AddScoped<IRepository<int, Loans>, LoansRepository>();
            builder.Services.AddScoped<IRepository<int, Transactions>, TransactionsRepository>();

            builder.Services.AddScoped<IBanksAdminService, BanksService>();
            builder.Services.AddScoped<IBranchesAdminService, BranchesService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
