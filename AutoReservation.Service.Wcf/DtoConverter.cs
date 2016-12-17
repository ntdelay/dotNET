using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.Service.Wcf
{
    public static class DtoConverter
    {
        #region Auto
        public static Auto ConvertToEntity(this AutoDto dto)
        {
            if (dto == null) { return null; }

            Auto auto = new Auto();
            auto.Carclass = (int)dto.AutoKlasse;
            auto.Id = dto.Id;
            auto.Brand = dto.Marke;
            auto.Daylefare = dto.Tagestarif;
            auto.RowVersion = dto.RowVersion;
            auto.Basefare = dto.Basistarif;

            return auto;
        }
        public static AutoDto ConvertToDto(this Auto entity)
        {
            if (entity == null) { return null; }

            AutoDto dto = new AutoDto
            {
                Id = entity.Id,
                Marke = entity.Brand,
                Tagestarif = entity.Daylefare,
                RowVersion = entity.RowVersion
            };

            dto.AutoKlasse = (AutoKlasse)entity.Carclass;
            dto.Basistarif = entity.Basefare;

            return dto;
        }
        public static List<Auto> ConvertToEntities(this IEnumerable<AutoDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<AutoDto> ConvertToDtos(this IEnumerable<Auto> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }
        #endregion
        #region Kunde
        public static Kunde ConvertToEntity(this KundeDto dto)
        {
            if (dto == null) { return null; }

            return new Kunde
            {
                Id = dto.Id,
                Lastname = dto.Nachname,
                Name = dto.Vorname,
                Birthday = dto.Geburtsdatum,
                RowVersion = dto.RowVersion
            };
        }
        public static KundeDto ConvertToDto(this Kunde entity)
        {
            if (entity == null) { return null; }

            return new KundeDto
            {
                Id = entity.Id,
                Nachname = entity.Lastname,
                Vorname = entity.Name,
                Geburtsdatum = entity.Birthday,
                RowVersion = entity.RowVersion
            };
        }
        public static List<Kunde> ConvertToEntities(this IEnumerable<KundeDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<KundeDto> ConvertToDtos(this IEnumerable<Kunde> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }
        #endregion
        #region Reservation
        public static Reservation ConvertToEntity(this ReservationDto dto)
        {
            if (dto == null) { return null; }

            Reservation reservation = new Reservation
            {
                ReservationNr = dto.ReservationsNr,
                From = dto.Von,
                To = dto.Bis,
                CarId = dto.Auto.Id,
                ClientId = dto.Kunde.Id,
                RowVersion = dto.RowVersion
            };

            return reservation;
        }
        public static ReservationDto ConvertToDto(this Reservation entity)
        {
            if (entity == null) { return null; }

            return new ReservationDto
            {
                ReservationsNr = entity.ReservationNr,
                Von = entity.From,
                Bis = entity.To,
                RowVersion = entity.RowVersion,
                Auto = ConvertToDto(entity.Car),
                Kunde = ConvertToDto(entity.Client)
            };
        }
        public static List<Reservation> ConvertToEntities(this IEnumerable<ReservationDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }
        public static List<ReservationDto> ConvertToDtos(this IEnumerable<Reservation> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }
        #endregion

        private static List<TTarget> ConvertGenericList<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> converter)
        {
            if (source == null) { return null; }
            if (converter == null) { return null; }

            return source.Select(converter).ToList();
        }
    }

}
