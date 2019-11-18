using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Source
{
    public class Vehicle
    {
        private string RegistrationNo { get; set; }
        private string Color { get; set; }
        public Vehicle(string registrationNo, string color)
        {
            this.RegistrationNo = registrationNo;
            this.Color = color;
        }
        public string GetColor()
        {
            return Color;
        }
        public string GetRegistrationNo()
        {
            return RegistrationNo;
        }
    }
}
