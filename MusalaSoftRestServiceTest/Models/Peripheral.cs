using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusalaSoftRestServiceTest.Models
{
    public class Peripheral
    {
        [Key]
        [DisplayName("UID Number")]
        public int UIDNumber { get; set; }

        [Required]
        public string Vendor { get; set; }

        [Required]
        [DisplayName("Create Date")]
        public DateTime CreateDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [ForeignKey("Gateway")]
        [DisplayName("Gateway")]
        public string GatewayId { get; set; }

        [JsonIgnore]
        public virtual Gateway Gateway { get; set; }
    }




    public enum Status
    {
        online = 1,
        offline = 2
    }
}
