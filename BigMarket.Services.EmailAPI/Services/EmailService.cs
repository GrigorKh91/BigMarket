using BigMarket.Services.EmailAPI.Data;
using BigMarket.Services.EmailAPI.Models;
using BigMarket.Services.EmailAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BigMarket.Services.EmailAPI.Services
{
    public sealed class EmailService(DbContextOptions<AppDbContext> dbOptions) : IEmailService
    {
        private readonly DbContextOptions<AppDbContext> _dbOptions = dbOptions;

        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new();

            message.AppendLine("<br/>Cart Email Requested ");
            message.AppendLine("<br/>Total " + cartDto.CartHeader.CartTotal);
            message.Append("<br/>");
            message.Append("<ul>");
            foreach (var item in cartDto.CartDetalis)
            {
                message.Append("<li>");
                message.Append(item.Product.Name + " x " + item.Count);
                message.Append("</li>");
            }
            message.Append("</ul>");
            await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
        }

        public async Task RegisterUserEmailAndLog(string email)
        {
            // TODO change hard codes
            string message = "User Registration Successful. <br/> Email : " + email;
            await LogAndEmail(message, "bigmarket@gmail.com");
        }

        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLogge = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };

                await using var _db = new AppDbContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLogge);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
