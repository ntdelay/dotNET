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
        public string Lastname { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }






    }

}
