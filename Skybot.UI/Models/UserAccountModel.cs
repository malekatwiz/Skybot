using System.ComponentModel.DataAnnotations;

namespace Skybot.UI.Models
{
    public class UserAccountModel
    {
        [Required]
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
    }
}
