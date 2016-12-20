using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            List<AutoDto> carList = Target.Autos;

            Assert.IsNotNull(carList);
        }

        [TestMethod]
        public void GetKundenTest()
        {
            List<KundeDto> clientList = Target.Kunden;

            Assert.IsNotNull(clientList);
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            List<ReservationDto> reservationList = Target.Reservationen;

            Assert.IsNotNull(reservationList);
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            AutoDto car = Target.GetAutoById(1);

            Assert.AreEqual(1, car.Id);
            Assert.AreEqual("Fiat Punto", car.Marke);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            KundeDto client = Target.GetKundeById(1);

            Assert.AreEqual("Anna", client.Vorname);
            Assert.AreEqual("Nass", client.Nachname);
            
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            ReservationDto reservation = Target.GetReservationByNr(1);

            Assert.AreEqual(1, reservation.ReservationsNr);
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            AutoDto car = Target.GetAutoById(20);

            Assert.IsNull(car);
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            KundeDto client = Target.GetKundeById(20);

            Assert.IsNull(client);
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {

            ReservationDto reservation = Target.GetReservationByNr(20);

            Assert.IsNull(reservation);
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
            int carCount = Target.Autos.Count;

            AutoDto car = new AutoDto();
            car.Marke = "VW";

            Target.InsertAuto(car);

            int newCarCount = Target.Autos.Count;

            Assert.AreEqual(newCarCount, carCount + 1);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            int clientCount = Target.Kunden.Count;

            KundeDto client = new KundeDto();
            client.Nachname = "Delay";
            client.Vorname = "Anthony";
            client.Geburtsdatum = new DateTime(1963, 1, 1);

            Target.InsertKunde(client);

            int newCount = Target.Kunden.Count;

            Assert.AreEqual(newCount, clientCount + 1);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            int reservationCount = Target.Reservationen.Count;

            AutoDto car = Target.GetAutoById(1);
            KundeDto client = Target.GetKundeById(1);

            ReservationDto reservation = new ReservationDto();
            reservation.Von = new DateTime(2017, 1, 1);
            reservation.Bis = new DateTime(2018, 1, 1);
            reservation.Auto = car;
            reservation.Kunde = client;

            Target.InsertReservation(reservation);

            int newCount = Target.Reservationen.Count;
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
            int carCount = Target.Autos.Count;

            AutoDto car = Target.GetAutoById(1);
            Target.DeleteAuto(car);

            int newCount = Target.Autos.Count + 1;

            Assert.AreEqual(newCount, carCount);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            int clientCount = Target.Kunden.Count;

            KundeDto client = Target.GetKundeById(1);
            Target.DeleteKunde(client);

            int newCount = Target.Kunden.Count + 1;

            Assert.AreEqual(newCount, clientCount);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            int reservationCount = Target.Reservationen.Count;

            ReservationDto reservation = Target.GetReservationByNr(1);
            Target.DeleteReservation(reservation);

            int newCount = Target.Reservationen.Count + 1;

            Assert.AreEqual(newCount, reservationCount);
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            AutoDto car = Target.GetAutoById(1);
            car.Marke = "VW Polo";

            Target.UpdateAuto(car);

            AutoDto testCar = Target.GetAutoById(1);

            Assert.AreEqual(testCar.Marke, "VW Polo");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            KundeDto client = Target.GetKundeById(1);
            client.Vorname = "Adrian";

            Target.UpdateKunde(client);

            KundeDto testClient = Target.GetKundeById(1);

            Assert.AreEqual(testClient.Vorname, "Adrian");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            ReservationDto reservation = Target.GetReservationByNr(1);
            reservation.Von = new DateTime(2017, 1, 1);

            Target.UpdateReservation(reservation);

            ReservationDto testReservation = Target.GetReservationByNr(1);

            Assert.AreEqual(reservation.Von, testReservation.Von);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto carTest1 = Target.GetAutoById(1);
            AutoDto carTest2 = Target.GetAutoById(1);
            carTest1.Marke = "Vw";
            carTest2.Marke = "BMW";
            Target.UpdateAuto(carTest1);
            Target.UpdateAuto(carTest2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto clientTest1 = Target.GetKundeById(1);
            KundeDto clientTest2 = Target.GetKundeById(1);
            clientTest1.Vorname = "Adrian";
            clientTest2.Vorname = "Anthony";
            Target.UpdateKunde(clientTest1);
            Target.UpdateKunde(clientTest2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            ReservationDto reservationTest1 = Target.GetReservationByNr(1);
            ReservationDto reservationTest2 = Target.GetReservationByNr(1);
            reservationTest1.Bis = new DateTime(2017, 1, 1);
            reservationTest2.Bis = new DateTime(2018, 1, 1);
            Target.UpdateReservation(reservationTest1);
            Target.UpdateReservation(reservationTest2);
        }

        #endregion
    }
}
