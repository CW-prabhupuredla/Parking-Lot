using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Source
{
    public class ParkingLotManagement
    {
        private uint _lotSize;
        private List<int> _freeSlots;

        /// <summary>
        /// This private member contains color as key and corresponding parked vehicles list as value  -- used for GetSlotNumbersFromColor, GetRegistrationNumbersFromColor
        /// </summary>
        private IDictionary<string, List<string>> _regOrSlotbyColorDict;

        /// <summary>
        /// This private member contains slot number as key and corresponding parked vehicle as value -- used for LotStatus
        /// </summary>
        private IDictionary<string, Vehicle> _parkedVehicles;
        
        /// <summary>
        /// This private member contains slot and corresponding parked vehicle registration number -- used for GetSlotNumberFromRegistrationNo
        /// </summary>
        private IDictionary<string, string> _slotandregnoDict;
        

        public string CreateParkingLot(string size)
        {
            uint.TryParse(size, out this._lotSize);
            _freeSlots = new List<int>();
            for (int i = 1; i <= this._lotSize; i++)
            {
                _freeSlots.Add(i);
            }
            _parkedVehicles = new Dictionary<String, Vehicle>();
            _regOrSlotbyColorDict = new Dictionary<string, List<string>>();
            _slotandregnoDict = new Dictionary<string, string>();
            return ("Created a parking lot with " + this._lotSize + " slots");
        }
       

        public string ParkVehicle(String regNo, String color)
        {
            if (this._lotSize == 0)
            {
                return ("Please create parking lot first");
            }
            else if (this._parkedVehicles.Count == this._lotSize)
            {
                return  ("Sorry, parking lot is full");
            }
            else
            {
                //to fullfill requirement that slots should be allocated which is first
                _freeSlots.Sort();
                String slot = _freeSlots[0].ToString();
                Vehicle Vehicle = new Vehicle(regNo, color);
                this._slotandregnoDict.Add(regNo, slot);
                this._parkedVehicles.Add(slot, Vehicle);
                if (this._regOrSlotbyColorDict.ContainsKey(color))
                {
                    List<string> regNoList = this._regOrSlotbyColorDict[color];
                    this._regOrSlotbyColorDict.Remove(color);
                    regNoList.Add(regNo);
                    this._regOrSlotbyColorDict.Add(color, regNoList);
                }
                else
                {
                    List<string> regNoList = new List<string>();
                    regNoList.Add(regNo);
                    this._regOrSlotbyColorDict.Add(color, regNoList);
                }
                _freeSlots.RemoveAt(0);
                return  ("Allocated slot number: " + slot);
            }
        }

        public string LotStatus()
        {
            if (this._lotSize == 0)
            {
                return ("Please create parking lot first");
            }
            else if (this._parkedVehicles.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Slot No.\tRegistration No.\tColor");
                Vehicle vehicle;
                for (int i = 1; i <= this._lotSize; i++)
                {
                    String key = i.ToString();
                    if (this._parkedVehicles.ContainsKey(key))
                    {
                        vehicle = this._parkedVehicles[key];
                        sb.AppendLine(i + "\t\t" + vehicle.GetRegistrationNo() + "\t\t" + vehicle.GetColor());
                    }
                }
                return sb.ToString();
            }
            else
            {
                return ("Parking lot is empty");
            }
        }

        public string Leave(string slotNo)
        {
            if (this._lotSize == 0)
            {
                return ("Please create parking lot first");
            }
            else if (this._parkedVehicles.Count > 0)
            {
                Vehicle leavingVehicle = this._parkedVehicles[slotNo];
                if (leavingVehicle != null)
                {
                    this._parkedVehicles.Remove(slotNo);
                    this._slotandregnoDict.Remove(leavingVehicle.GetRegistrationNo());
                    List<string> regNoList = this._regOrSlotbyColorDict[leavingVehicle.GetColor()];
                    if (regNoList.Contains(leavingVehicle.GetRegistrationNo()))
                    {
                        regNoList.Remove(leavingVehicle.GetRegistrationNo());
                    }

                    this._freeSlots.Add(int.Parse(slotNo));
                    return ("Slot number " + slotNo + " is free");
                }
                else
                {
                    return ("Slot number " + slotNo + " is already empty");
                }
            }
            else
            {
                return ("Parking lot is empty");
            }
        }

        public string GetRegistrationNumbersFromColor(String color) 
        {
		    if (this._lotSize == 0) 
            {
                return ("Please create parking lot first");
		    } 
            else if (this._regOrSlotbyColorDict.ContainsKey(color)) 
            {
			    List<String> regNoList = this._regOrSlotbyColorDict[color];
                StringBuilder sb = new StringBuilder();
			    for (int i = 0; i < regNoList.Count; i++) 
                {
                    sb.Append(regNoList[i] + ", ");
			    }
                //remove the trailing ", "
                return sb.ToString(0, sb.Length - 2);
		    } 
            else {
                return ("No registration numbers found");
		    }
	    }

        public string GetSlotNumbersFromColor(String color)
        {
            if (this._lotSize == 0)
            {
                return ("Please create parking lot first");
            }
            else if (this._regOrSlotbyColorDict.ContainsKey(color))
            {
                List<String> regNoList = this._regOrSlotbyColorDict[color];
                List<int> slotList = new List<int>();
                for (int i = 0; i < regNoList.Count; i++)
                {
                    slotList.Add(int.Parse(this._slotandregnoDict[regNoList[i]]));
                }
                //inorder to print ascending order
                slotList.Sort();
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < slotList.Count; j++)
                {
                    sb.Append(slotList[j] + ", ");
                }
                //remove the trailing ", "
                return sb.ToString(0, sb.Length - 2);
            }
            else
            {
                return ("No slot numbers found");
            }
        }

        public string GetSlotNumberFromRegistrationNo(String regNo)
        {
            if (this._lotSize == 0)
            {
                return ("Please create parking lot first");
            }
            else if (this._slotandregnoDict.ContainsKey(regNo))
            {
                return (this._slotandregnoDict[regNo]);
            }
            else
            {
                return ("Not found");
            }
        }
        /// <summary>
        /// used in unit Testing to access the private members
        /// </summary>
        /// <returns></returns>
        public int GetFreeSlots()
        {
            return _freeSlots.Count;
        }

        /// <summary>
        /// used in unit Testing to access the private members
        /// </summary>
        /// <param name="slotNo"></param>
        /// <returns></returns>
        public bool IsSlotAvailable(int slotNo)
        {
            return _freeSlots.Contains(slotNo);
        }
    }
}
