using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        //Autos
        List<AutoDto> Autos { [OperationContract] get; }

        [OperationContract]
        AutoDto GetAutoById(int id);

        [OperationContract]
        void InsertAuto(AutoDto auto);

        [OperationContract]
        void UpdateAuto(AutoDto auto);

        [OperationContract]
        void DeleteAuto(AutoDto auto);
        //Kunden
        List<KundeDto> Kunden { [OperationContract]  get; }

        [OperationContract]
        KundeDto GetKundeById(int id);

        [OperationContract]
        void InsertKunde(KundeDto kunde);

        [OperationContract]
        void UpdateKunde(KundeDto kunde);

        [OperationContract]
        void DeleteKunde(KundeDto kunde);

        //Reservation
        List<ReservationDto> Reservationen { [OperationContract]  get; }

        [OperationContract]
        ReservationDto GetReservationByNr(int nr);

        [OperationContract]
        void InsertReservation(ReservationDto reservation);

        [OperationContract]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract]
        void DeleteReservation(ReservationDto reservation);


    }
}
