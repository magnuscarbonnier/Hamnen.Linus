using System;

namespace Hamnen
{
    public abstract class Boat
    {
        public int Size { get; protected set; }
        public string IdNr { get; protected set; }
        public int Weight { get; protected set; }
        public int MaxSpeed { get; protected set; }
        public int DaysUntilDeparture { get; protected set; }
        public void AdvanceDay(int days = 1)
        {
            DaysUntilDeparture -= days;
        }
    }

    public class MotorBoat : Boat
    {
        public int HorsePower { get; protected set; }
        public MotorBoat()
        {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVXYZ".ToCharArray();
            Random rand = new Random();
            Size = 1;
            IdNr = $"M-{chars[rand.Next(chars.Length)]}{chars[rand.Next(chars.Length)]}{chars[rand.Next(chars.Length)]}";
            Weight = rand.Next(200, 3001);
            MaxSpeed = rand.Next(0, 61);
            DaysUntilDeparture = 3;
            HorsePower = rand.Next(10, 1001);
        }
        public override string ToString()
        {
            return String.Format("{0, -15}", "Motorbåt") + String.Format("{0, -10}", this.IdNr) + this.DaysUntilDeparture;
        }
    }

    public class SailBoat : Boat
    {
        public int Length { get; protected set; }
        public SailBoat()
        {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVXYZ".ToCharArray();
            Random rand = new Random();
            Size = 2;
            IdNr = $"S-{chars[rand.Next(chars.Length)]}{chars[rand.Next(chars.Length)]}{chars[rand.Next(chars.Length)]}";
            Weight = rand.Next(800, 6001);
            MaxSpeed = rand.Next(0, 13);
            DaysUntilDeparture = 4;
            Length = rand.Next(10, 61);
        }
        public override string ToString()
        {
            return String.Format("{0, -15}", "Segelbåt") + String.Format("{0, -10}", this.IdNr) + this.DaysUntilDeparture;
        }
    }

    public class CargoShip : Boat
    {
        public int NrOfContainers { get; protected set; }
        public CargoShip()
        {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVXYZ".ToCharArray();
            Random rand = new Random();
            Size = 4;
            IdNr = $"L-{chars[rand.Next(chars.Length)]}{chars[rand.Next(chars.Length)]}{chars[rand.Next(chars.Length)]}";
            Weight = rand.Next(3000, 20001);
            MaxSpeed = rand.Next(0, 21);
            DaysUntilDeparture = 6;
            NrOfContainers = rand.Next(0, 501);
        }
        public override string ToString()
        {
            return String.Format("{0, -15}", "Lastfartyg") + String.Format("{0, -10}", this.IdNr) + this.DaysUntilDeparture;
        }
    }

    public class RowingBoat : Boat
    {
        public int MaxPassengers { get; protected set; }
        public RowingBoat()
        {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVXYZ".ToCharArray();
            Random rand = new Random();
            Size = 1;
            IdNr = $"R-{chars[rand.Next(chars.Length)]}{chars[rand.Next(chars.Length)]}{chars[rand.Next(chars.Length)]}";
            Weight = rand.Next(100, 300);
            MaxSpeed = rand.Next(0, 3);
            DaysUntilDeparture = 1;
            MaxPassengers = rand.Next(1, 6);
        }
        public override string ToString()
        {
            return String.Format("{0, -15}", "Roddbåt") + String.Format("{0, -10}", this.IdNr) + this.DaysUntilDeparture;
        }
    }
}
