using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {

        private static IQueryable<Reservation> GetReservationQueryable(AutoReservationContext db)
        {
            return db.Reservations
                .Include(r => r.Auto)
                .Include(r => r.Kunde);
        }
        private void ReloadAndSave(AutoReservationContext db, object entryObject)
        {
            var entry = db.Entry(entryObject);
            entry.Reload();
            db.SaveChanges();
        }

        private static LocalOptimisticConcurrencyException<T> CreateLocalOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            var dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new LocalOptimisticConcurrencyException<T>($"Update {typeof(Auto).Name}: Concurrency-Fehler", dbEntity);
        }
        /*      ------------------  AUTO  ----------------------  */
        public List<Auto> Autos
        {
            get
            {
                using (AutoReservationContext db = new AutoReservationContext())
                {
                    return (from auto in db.Cars
                            select auto).ToList();
                }
            }
        }

        public Auto GetAutoById(int id)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                return (from auto in db.Cars
                        where auto.Id == id
                        select auto).FirstOrDefault();
            }
        }

        public void DeleteAuto(Auto auto)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                db.Entry(auto).State = EntityState.Deleted;
                db.Cars.Remove(auto);
                db.SaveChanges();
            }
        }

        public void InsertAuto(Auto auto)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                db.Entry(auto).State = EntityState.Added;
                db.Cars.Add(auto);
                db.SaveChanges();
            }
        }
        public void UpdateAuto(Auto auto)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                try
                {
                    db.Entry(auto).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ReloadAndSave(db, auto);
                    throw new LocalOptimisticConcurrencyException<Auto>("");
                }
            }
        }

        /*      ------------------  KUNDE  ----------------------  */
        public List<Kunde> Kunden
        {
            get
            {
                using (AutoReservationContext db = new AutoReservationContext())
                {
                    return (from kunde in db.Clients
                            select kunde).ToList();
                }
            }
        }

        public Kunde GetKundeById(int id)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                return (from kunde in db.Clients
                        where kunde.Id == id
                        select kunde).FirstOrDefault();
            }
        }

        public void DeleteKunde(Kunde kunde)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                db.Entry(kunde).State = EntityState.Deleted;
                db.Clients.Remove(kunde);
                db.SaveChanges();
            }
        }

        public void InsertKunde(Kunde kunde)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                db.Entry(kunde).State = EntityState.Added;
                db.Clients.Add(kunde);
                db.SaveChanges();
            }
        }

        public void UpdateKunde(Kunde kunde)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                try
                {
                    db.Entry<Kunde>(kunde).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ReloadAndSave(db, kunde);
                    throw new LocalOptimisticConcurrencyException<Kunde>("");
                }
            }
        }

        /*      ------------------  RESERVATÌON  ----------------------  */
        public List<Reservation> Reservationen
        {
            get
            {
                using (AutoReservationContext db = new AutoReservationContext())
                {
                    return GetReservationQueryable(db).ToList();
                }
            }
        }

        public Reservation GetReservationByNr(int nr)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                return (from reservation in GetReservationQueryable(db)
                        where reservation.ReservationNr == nr
                        select reservation).FirstOrDefault();
            }
        }
       

        public void DeleteReservation(Reservation reservation)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                reservation = GetReservationByNr(reservation.ReservationNr);

                db.Entry(reservation).State = EntityState.Deleted;

                db.Reservations.Remove(reservation);
                db.SaveChanges();
            }
        }

        public void InsertReservation(Reservation reservation)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                reservation.Car = GetAutoById(reservation.CarId);
                reservation.Client = GetKundeById(reservation.ClientId);

                db.Entry(reservation).State = EntityState.Added;
                db.Reservations.Add(reservation);
                db.SaveChanges();
            }
        }
        public void UpdateReservation(Reservation reservation)
        {
            using (AutoReservationContext db = new AutoReservationContext())
            {
                try
                {
                    db.Entry(reservation).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ReloadAndSave(db, reservation);
                    throw new LocalOptimisticConcurrencyException<Reservation>("");
                }
            }
        }



    }
}