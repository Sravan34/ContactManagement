using System.ComponentModel.DataAnnotations;

namespace ContactManagementApp1
{
    public class Contacts
    {
        public int id { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string email { get; set; }
    }
}
