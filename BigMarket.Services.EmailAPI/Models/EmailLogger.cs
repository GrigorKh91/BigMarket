using System.ComponentModel.DataAnnotations;

namespace BigMarket.Services.EmailAPI.Models
{
    public sealed class EmailLogger
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Message { get; set; }
        public DateTime? EmailSent { get; set; }
    }
}
