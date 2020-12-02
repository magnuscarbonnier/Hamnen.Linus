using System;

namespace Hamnen
{
    class Program
    {
        static Boat[] Port = new Boat[25];
        static int rejectedCount = 0;
        static int day = 0;
        static void Main(string[] args)
        {
            while (true)
            {
                AdvanceDay();
                DepartBoats();
                AddRandomBoats();
                Console.Clear();
                PrintPort();
                Console.ReadKey();
            }
        }

        static void AdvanceDay(int days = 1)
        {
            day++;
            for (int i = 0; i < Port.Length;)
            {
                Boat spot = Port[i];
                if (spot != null)
                {
                    spot.AdvanceDay(days);
                    i += spot.Size;
                }
                else
                {
                    i++;
                }
            }
        }

        static void DepartBoats()
        {
            for (int i = 0; i < Port.Length; i++)
            {
                if (Port[i]?.DaysUntilDeparture == 0)
                {
                    int n = Port[i].Size;
                    for (int j = 0; j < n; j++)
                    {
                        Port[i + j] = null;
                    }
                }
            }
        }

        static void AddRandomBoats(int n = 5)
        {
            for (int i = 0; i < n; i++)
            {
                Boat boat = GetRandomBoat();
                int spot = GetAvailableSpot(boat);
                if (spot != -1)
                {
                    for (int j = 0; j < boat.Size; j++)
                    {
                        Port[spot + j] = boat;
                    }
                }
                else
                {
                    rejectedCount++;
                }
            }
        }

        static void PrintPort()
        {
            Console.WriteLine($"Day: {day}");
            Console.WriteLine($"Rejected: {rejectedCount}");
            for (int i = 0; i < Port.Length;)
            {
                if (Port[i] == null)
                {
                    Console.WriteLine(String.Format("{0, -10}", i) + "Tom plats");
                    i++;
                }
                else
                {
                    Boat boat = Port[i];
                    string s;
                    if (boat is MotorBoat)
                    {
                        s = String.Format("{0, -10}", i);
                    }
                    else
                    {
                        s = String.Format("{0, -10}", $"{i}-{i + boat.Size - 1}");
                    }
                    s += boat.ToString();
                    Console.WriteLine(s);
                    i += boat.Size;
                }
            }
        }

        static Boat GetRandomBoat()
        {
            int n = new Random().Next(3);
            switch (n)
            {
                case 0:
                    return new MotorBoat();
                case 1:
                    return new SailBoat();
                case 2:
                    return new CargoShip();
                default:
                    break;
            }
            return null;
        }

        static int GetAvailableSpot(Boat boat)
        {
            for (int i = 0; i < Port.Length; i++)
            {
                if (Port[i] == null)
                {
                    bool spotAvailable = true;
                    int l = i + boat.Size;
                    int spot = i;
                    for (int j = i; j < l; j++)
                    {
                        if (j >= Port.Length)
                        {
                            return -1;
                        }
                        if (spotAvailable)
                        {
                            i = j;
                            spotAvailable = Port[j] == null;
                        }
                        else
                        {
                            i = j;
                            break;
                        }
                    }
                    if (spotAvailable)
                    {
                        return spot;
                    }
                }
            }
            return -1;
        }
    }
}
