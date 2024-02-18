namespace SportApp.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserFormModel
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required] 
        [Phone] 
        public string PhoneNumber { get; set; } = null!;
    }
}
