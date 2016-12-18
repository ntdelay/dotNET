using System;
using System.Diagnostics;
using AutoReservation.Common.Interfaces;
using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using AutoReservation.Dal.Entities;
using AutoReservation.BusinessLayer;
using System.ServiceModel;


namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private readonly AutoReservationBusinessComponent component;

        public AutoReservationService()
        {
            component = new AutoReservationBusinessComponent();
        }

        public List<AutoDto> Autos
        {
            get
            {
                return DtoConverter.ConvertToDtos(component.Autos);
            }
        }

        public List<KundeDto> Kunden
        {
            get
            {
                return DtoConverter.ConvertToDtos(component.Kunden);
            }
        }

        public List<ReservationDto> Reservationen
        {
            get
            {
                return DtoConverter.ConvertToDtos(component.Reservationen);
            }
        }


        public ReservationDto GetReservationByNr(int nr)
        {
            Reservation reservation = component.GetReservationByNr(nr);
            return reservation.ConvertToDto();
        }


        public AutoDto GetAutoById(int id)
        {
            Auto auto = component.GetAutoById(id);
            return auto.ConvertToDto();
        }



        public KundeDto GetKundeById(int id)
        {
            Kunde kunde = component.GetKundeById(id);
            return kunde.ConvertToDto();
        }


        public void DeleteAuto(AutoDto auto)
        {
            component.DeleteAuto(auto.ConvertToEntity());
        }

        public void DeleteKunde(KundeDto kunde)
        {
            component.DeleteKunde(kunde.ConvertToEntity());
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            component.DeleteReservation(reservation.ConvertToEntity());
        }



        public void InsertAuto(AutoDto auto)
        {
            Auto autoEntity = auto.ConvertToEntity();
            component.InsertAuto(autoEntity);
        }

        public void InsertKunde(KundeDto kunde)
        {
            Kunde kundeEntity = kunde.ConvertToEntity();
            component.InsertKunde(kundeEntity);
        }






        public void InsertReservation(ReservationDto reservation)
        {
            Reservation reservationEntity = reservation.ConvertToEntity();
            component.InsertReservation(reservationEntity);
        }



        public void UpdateAuto(AutoDto auto)
        {
            try
            {
                Auto autoEntity = auto.ConvertToEntity();
                component.UpdateAuto(autoEntity);
            }
            catch (LocalOptimisticConcurrencyException<Auto>)
            {
                throw new FaultException("can't update car");
            }
        }




        public void UpdateKunde(KundeDto kunde)
        {
            try
            {
                Kunde kundeEntity = kunde.ConvertToEntity();
                component.UpdateKunde(kundeEntity);
            }
            catch (LocalOptimisticConcurrencyException<Kunde>)
            {
                throw new FaultException("can't updating customer");
            }
        }



        public void UpdateReservation(ReservationDto reservation)
        {
            try
            {
                Reservation reservationEntity = reservation.ConvertToEntity();
                component.UpdateReservation(reservationEntity);
            }
            catch (LocalOptimisticConcurrencyException<Reservation> ex)
            {
                throw new FaultException("can't updating reservation");
            }
        }



        private static void WriteActualMethod()
        {
            Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
        }
    }
}