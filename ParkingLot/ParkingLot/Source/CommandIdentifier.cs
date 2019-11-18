using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Source
{
    public class CommandIdentifier
    {
        private ParkingLotManagement parkingLot;
        public CommandIdentifier()
        {
            parkingLot = new ParkingLotManagement();
        }
        public string CallCorrespondingFunction(string command)
        {
            string[] str = command.Split(' ');
            CommandTypeEnum commandType;
            string output = string.Empty;
            if (Enum.TryParse(str[0], out commandType))
            {
                switch(commandType)
                {
                    case CommandTypeEnum.create_parking_lot:
                        output = parkingLot.CreateParkingLot(str[1]);
                        break;
                    case CommandTypeEnum.park:
                        output = parkingLot.ParkVehicle(str[1], str[2]);
                        break;
                    case CommandTypeEnum.status:
                        output = parkingLot.LotStatus();
                        break;
                    case CommandTypeEnum.leave:
                        output = parkingLot.Leave(str[1]);
                        break;
                    case CommandTypeEnum.registration_numbers_for_cars_with_colour:
                        output = parkingLot.GetRegistrationNumbersFromColor(str[1]);
                        break;
                    case CommandTypeEnum.slot_numbers_for_cars_with_colour:
                        output = parkingLot.GetSlotNumbersFromColor(str[1]);
                        break;
                    case CommandTypeEnum.slot_number_for_registration_number:
                        output = parkingLot.GetSlotNumberFromRegistrationNo(str[1]);
                        break;
                    case CommandTypeEnum.exit:
                        Environment.Exit(0);
                        break;
                }    
            }
            else
            {
                output = "Command not found";
            }
            return output;
        }
    }
}
