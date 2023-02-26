using System.ComponentModel.DataAnnotations;

namespace SAQAYA.UserAPI.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool MarketingConsent { get; set; }
    }
}
