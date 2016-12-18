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

        public string Marke { get; set; }
        public int Tagestarif { get; set; }
        public int? Basistarif { get; set; }
        public int AutoKlasse { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }











    }
}
