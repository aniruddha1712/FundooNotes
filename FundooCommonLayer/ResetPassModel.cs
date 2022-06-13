using System.ComponentModel.DataAnnotations;

namespace FundooCommonLayer
{
    public class ResetPassModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}