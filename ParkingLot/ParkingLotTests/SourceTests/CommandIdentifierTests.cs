using ParkingLot.Source;
using Xunit;

namespace ParkingLotTests.SourceTests
{
    public class CommandIdentifierTests
    {
        CommandIdentifier commandIdentifier = new CommandIdentifier();
        [Fact]
        public void CallCorrespondingFunction()
        {
            string output;
            output = commandIdentifier.CallCorrespondingFunction("park KA-01-HH-1234 White");
            Assert.True(output.Equals("Allocated slot number: 1"));
            output = commandIdentifier.CallCorrespondingFunction("leave 4");
            Assert.True(output.Equals("Slot number 4 is free"));
            output = commandIdentifier.CallCorrespondingFunction("remove_all_vehicles");
            Assert.True(output.Equals("Command not found"));
        }
    }
}
