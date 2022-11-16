using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace JamesWebApp.Models
{
    public class AspNetUsers
    {
        //used bool for bit due to needing a true or false value
        public string Id { get; set; }
        public string ?UserName { get; set; }
        public string  ?NormalizedUserName { get; set; }
        //my regular expression
        //[Required(ErrorMessage = "Email id is required")]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please enter a valid email address")]
        public string ?Email { get; set; }
        public string ?NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ?PasswordHash { get; set; }
        public string ?SecurityStamp { get; set; }
        public string ?ConcurrencyStamp { get; set; }
        public string ?PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset ?LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
