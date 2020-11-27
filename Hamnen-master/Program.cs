using System;
using System.Collections.Generic;

namespace Hamnen
{
    class Program
    {
        static Dictionary<int, List<Boat>> Port = new Dictionary<int, List<Boat>>();

        static int rejectedCount = 0;
        static int day = 0;
        static void Main(string[] args)
        {
            //set id for spots
            for (int i = 0; i < 25; i++)
            {
                Port.Add(i, new List<Boat>());
            }


            while (true)
            {
                AdvanceDay();
                DepartBoats();
                AddRandomBoats();
                PrintPort();
            }
        }

        static void AdvanceDay(int days = 1)
        {
            day++;
            for (int i = 0; i < Port.Count; )
            {
                var spot = Port[i];
                foreach (var boat in spot)
                {
                    boat.AdvanceDay(days);
                }
                if (spot.Count != 0)
                    i += spot[0].Size;
                else
                    i++;
            }
        }

        static void DepartBoats()
        {
            for (int i = 0; i < Port.Count; i++)
            {
                if (Port[i].Count == 1 && Port[i].Exists(c => c.DaysUntilDeparture == 0))
                {
                    Port[i].RemoveAt(0);
                }
                else if (Port[i].Count == 2)
                {
                    //Todo

                    if (Port[i][0].DaysUntilDeparture == 0)
                    {
                        Port[i].RemoveAt(0);
                        if (Port[i][0].DaysUntilDeparture == 0)
                            Port[i].RemoveAt(0);
                    }
                    else if (Port[i][1].DaysUntilDeparture == 0)
                    {
                        Port[i].RemoveAt(1);
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
                        Port[spot + j].Add(boat);
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
            Console.Clear();
            Console.WriteLine($"Day: {day}");
            Console.WriteLine($"Rejected: {rejectedCount}");
            for (int i = 0; i < Port.Count;)
            {
                string s;
                if (Port[i].Count == 0)
                {
                    Console.WriteLine(String.Format("{0, -10}", i + 1) + "Tom plats");
                    i++;
                }
                else if (Port[i].Count == 1 && Port[i][0].Size == 1)
                {
                    var boat = Port[i][0];
                    s = String.Format("{0, -10}", i + 1);

                    s += boat.ToString();
                    Console.WriteLine(s);
                    i += boat.Size;
                }
                else if (Port[i].Count == 1 && Port[i][0].Size > 1)
                {
                    var boat = Port[i][0];
                    s = String.Format("{0, -10}", $"{i + 1}-{i + 1 + boat.Size - 1}");

                    s += boat.ToString();
                    Console.WriteLine(s);
                    i += boat.Size;
                }
                else if (Port[i].Count == 2)
                {
                    foreach (var boat in Port[i])
                    {
                        s = String.Format("{0, -10}", i + 1);
                        s += boat.ToString();
                        Console.WriteLine(s);
                    }
                    i += 1;
                }
            }
            Console.ReadKey();
        }

        static Boat GetRandomBoat()
        {
            int n = new Random().Next(4);
            switch (n)
            {
                case 0:
                    return new MotorBoat();
                case 1:
                    return new SailBoat();
                case 2:
                    return new CargoShip();
                case 3:
                    return new RowingBoat();
                default:
                    break;
            }
            return null;
        }

        static int GetAvailableSpot(Boat boat)
        {
            for (int i = 0; i < Port.Count; i++)
            {
                if (boat.IdNr.StartsWith("R-") && Port[i].Count == 1 && Port[i].Exists(c => c.IdNr.StartsWith("R-")))
                {
                    int spot = i;
                    return spot;
                    //there is room for rowingboat!
                }
                else if (Port[i].Count == 0)
                {
                    bool spotAvailable = true;
                    int l = i + boat.Size;
                    int spot = i;
                    for (int j = i; j < l; j++)
                    {
                        if (j >= Port.Count)
                        {
                            return -1;
                        }
                        if (spotAvailable)
                        {
                            i = j;
                            spotAvailable = Port[j].Count == 0;
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
