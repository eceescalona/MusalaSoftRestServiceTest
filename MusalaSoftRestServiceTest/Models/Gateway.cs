using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusalaSoftRestServiceTest.Models
{
    public class Gateway
    {
        public Gateway()
        {
            Peripherals = new HashSet<Peripheral>();
        }

        [Key]
        [DisplayName("Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("IPv4 Address")]
        public string IpAddress { get; set; }

        [MaxLength(10, ErrorMessage = "You can't have more than 10 Peripherals associated with the Gateway")]
        public ICollection<Peripheral> Peripherals { get; set; }
    }
}
