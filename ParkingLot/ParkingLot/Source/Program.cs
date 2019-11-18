using System;
using ParkingLot.Source;
using System.IO;
public class Program
{
    public static void Main(string[] args)
    {
        CommandIdentifier commandIdentifier = new CommandIdentifier();

        while (true)
        {
            string command = Console.ReadLine();
            var output = commandIdentifier.CallCorrespondingFunction(command);

            Console.WriteLine(output);
            Console.WriteLine();
        }
    } 
}