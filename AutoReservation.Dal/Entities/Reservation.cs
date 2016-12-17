using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationNr { get; set; }
        [ForeignKey("Auto")]
        public int CarId { get; set; }
        [ForeignKey("Kunde")]
        public int ClientId { get; set; }
        public virtual Auto Car { get; set; }
        public virtual Kunde Client { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime From { get; set; } 
        [Column(TypeName = "datetime")]
        public DateTime To { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }  
    }
}
