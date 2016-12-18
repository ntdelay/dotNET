using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
  public class Kunde {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public DateTime Geburtsdatum { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }






    }

}
