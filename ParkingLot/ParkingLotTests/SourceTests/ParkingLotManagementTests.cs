using ParkingLot.Source;
using System;
using Xunit;

namespace ParkingLotTests.SourceTests
{
    public class ParkingLotManagementTests
    {
        ParkingLotManagement parkingLot = new ParkingLotManagement();
        
        [Fact]
        public void createParkingLot() 
        {
		    parkingLot.CreateParkingLot("6");
            Assert.True(parkingLot.GetFreeSlots() == 6);
	    }

        [Fact]
        public void ParkVehicle(String regNo, String color)
        {
            var str = parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            Assert.True(str.Equals("Please create parking lot first"));

            parkingLot.CreateParkingLot("6");
            parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            parkingLot.ParkVehicle("KA-01-HH-9999", "White");
            var output = parkingLot.ParkVehicle("KA-01-BB-0001", "Black");
            Assert.True(parkingLot.GetFreeSlots() == 3);
            parkingLot.ParkVehicle("KA-01-HH-7777", "Red");
            parkingLot.ParkVehicle("KA-01-HH-2701", "Blue");
            parkingLot.ParkVehicle("KA-01-HH-3141", "Black");
            Assert.True(parkingLot.GetFreeSlots() == 0);
        }

        [Fact]
        public void LotStatus()
        {
            var output = parkingLot.LotStatus();
            output.Replace(" ", "");
            var expectedOutput = "Slot No.RegistrationNoColor1KA-01-HH-1234White2KA-01-HH-9999White3KA-01-BB-0001Black5KA-01-HH-2701Blue6KA-01-HH-3141Black";
            Assert.True(String.Equals(output, expectedOutput, StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Leave(string slotNo)
        {
            var str = parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            Assert.True(str.Equals("Please create parking lot first"));

            parkingLot.CreateParkingLot("6");
            parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            parkingLot.ParkVehicle("KA-01-HH-9999", "White");
            parkingLot.ParkVehicle("KA-01-BB-0001", "Black");
            Assert.True(parkingLot.GetFreeSlots() == 3);
            parkingLot.ParkVehicle("KA-01-HH-7777", "Red");
            parkingLot.ParkVehicle("KA-01-HH-2701", "Blue");
            parkingLot.ParkVehicle("KA-01-HH-3141", "Black");
            parkingLot.Leave("4");
            Assert.True(parkingLot.GetFreeSlots() == 5 && !parkingLot.IsSlotAvailable(4));
        }

        [Fact]
        public void GetRegistrationNumbersFromColor(String color)
        {
            var str = parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            Assert.True(str.Equals("Please create parking lot first"));

            parkingLot.CreateParkingLot("6");
            parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            parkingLot.ParkVehicle("KA-01-HH-9999", "White");
            parkingLot.ParkVehicle("KA-01-BB-0001", "Black");
            Assert.True(parkingLot.GetFreeSlots() == 3);
            parkingLot.ParkVehicle("KA-01-HH-7777", "Red");
            parkingLot.ParkVehicle("KA-01-HH-2701", "Blue");
            parkingLot.ParkVehicle("KA-01-HH-3141", "Black");
            parkingLot.Leave("4");
            
            var output = parkingLot.GetRegistrationNumbersFromColor("White");
            output.Replace(" ", "");
            var expectedOutput = "KA-01-HH-1234,KA-01-HH-9999,KA-01-P-333";
            Assert.True(output.Equals(expectedOutput));
        }

        [Fact]
        public void GetSlotNumbersFromColor(String color)
        {
            var str = parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            Assert.True(str.Equals("Please create parking lot first"));

            parkingLot.CreateParkingLot("6");
            parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            parkingLot.ParkVehicle("KA-01-HH-9999", "White");
            parkingLot.ParkVehicle("KA-01-BB-0001", "Black");
            Assert.True(parkingLot.GetFreeSlots() == 3);
            parkingLot.ParkVehicle("KA-01-HH-7777", "Red");
            parkingLot.ParkVehicle("KA-01-HH-2701", "Blue");
            parkingLot.ParkVehicle("KA-01-HH-3141", "Black");
            parkingLot.Leave("4");
            
            var output = parkingLot.GetSlotNumbersFromColor("White");
            output.Replace(" ", "");
            var expectedOutput = "1,2,4";
            Assert.True(output.Equals(expectedOutput));

        }

        [Fact]
        public void GetSlotNumberFromRegistrationNo(String regNo)
        {
            var str = parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            Assert.True(str.Equals("Please create parking lot first"));

            parkingLot.CreateParkingLot("6");
            parkingLot.ParkVehicle("KA-01-HH-1234", "White");
            parkingLot.ParkVehicle("KA-01-HH-9999", "White");
            parkingLot.ParkVehicle("KA-01-BB-0001", "Black");
            Assert.True(parkingLot.GetFreeSlots() == 3);
            parkingLot.ParkVehicle("KA-01-HH-7777", "Red");
            parkingLot.ParkVehicle("KA-01-HH-2701", "Blue");
            parkingLot.ParkVehicle("KA-01-HH-3141", "Black");
            parkingLot.Leave("4");
            
            var output = parkingLot.GetSlotNumberFromRegistrationNo("KA-01-HH-3141");
            output.Replace(" ", "");
            var expectedOutput = "6";
            Assert.True(output.Equals(expectedOutput));

            output = parkingLot.GetSlotNumberFromRegistrationNo("MH-04-AY-1111");
            output.Replace(" ", "");
            expectedOutput = "Notfound";
            Assert.True(output.Equals(expectedOutput));
        }
    }
}
