using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationsNr { get; set; }
        [ForeignKey("Auto")]
        public int AutoId { get; set; }
        [ForeignKey("Kunde")]
        public int KundeId { get; set; }
        public virtual Auto Auto { get; set; }
        public virtual Kunde Kunde { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Von { get; set; } 
        [Column(TypeName = "datetime")]
        public DateTime Bis { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }  
    }
}
