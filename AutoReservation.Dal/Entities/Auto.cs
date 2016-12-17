using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
 public class Auto {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20)]

        public string Brand { get; set; }
        public int Daylefare { get; set; }
        public int? Basefare { get; set; }
        public int Carclass { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }











    }
}
