using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {

        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }
        
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void UpdateAutoTest()
        {
            Auto auto1 = Target.GetAutoById(1);
            auto1.Tagestarif = 100;
            auto1.Marke = "VW";

            Target.UpdateAuto(auto1);

            Auto testCar = Target.GetAutoById(1);

            Assert.AreEqual("VW", testCar.Marke);
            Assert.AreEqual(100, testCar.Tagestarif);
            Assert.AreNotEqual(99, testCar.Tagestarif);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Kunde client = Target.GetKundeById(1);
            client.Vorname = "Anthony";
            client.Nachname = "Delay";

            Target.UpdateKunde(client);

            Kunde testClient = Target.GetKundeById(1);

            Assert.AreEqual("Delay", testClient.Nachname);
            Assert.AreEqual("Anthony", testClient.Vorname);
             
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Reservation reservation = Target.GetReservationByNr(1);
            reservation.Von = new DateTime(2017, 1, 1);
            reservation.Bis = new DateTime(2018, 1, 1);

            target.UpdateReservation(reservation);

            Reservation testReservation = Target.GetReservationByNr(1);

            Assert.AreEqual(new DateTime(2017, 1, 1), testReservation.Von);
            
        }

    }

}
