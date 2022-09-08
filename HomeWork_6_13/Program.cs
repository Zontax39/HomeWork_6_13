using System;
using System.Collections.Generic;

namespace HomeWork_6_13
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ServiceStation serviceStation = new ServiceStation();
            serviceStation.Start();
        }
    }

    internal static class UserUtils
    {
        public static int GetNumber(string text)
        {
            if (int.TryParse(text, out int number))
            {
                return number;
            }
            else
            {
                Console.WriteLine("Попробуйте ещё раз!");
            }
            return 0;
        }
    }

    internal class Breakage
    {
        public string Name { get; private set; }
        public int PriceRepair { get; private set; }
        public string RepairDetail { get; private set; }

        public Breakage(string name, string detail, int price)
        {
            Name = name;
            PriceRepair = price;
            RepairDetail = detail;
        }
    }

    internal class Client
    {
        public Breakage Breakage { get; private set; }
        public int Money { get; private set; }

        public Client()
        {
            Money = 20000;
            Breakage = FillBreakages();
        }

        private Breakage FillBreakages()
        {
            DB_Breakages dbBreakages = new DB_Breakages();
            return dbBreakages.GetRandomBreakage();
        }
    }

    internal class Detail
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Detail(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }

    internal class ServiceStation
    {
        private int _money;
        private int _penalty;
        private Storage _storage;

        public ServiceStation()
        {
            _penalty = 500;
            _money = 10000;
            _storage = new Storage();
        }

        public void Start()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.Clear();
                Console.WriteLine("Автоссервис:");
                Client client = new Client();
                Console.WriteLine("На ремонт приехал клиент!");
                Console.WriteLine($"Поломка:{client.Breakage.Name}. Цена за ремонт:{CalculateRepairPrice(client.Breakage)}");
                Console.WriteLine("Выбирите деталь для установки или введите \"esc\" для отказа");
                _storage.ShowAllDetails();
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "esc":
                        Rejection();
                        break;

                    default:
                        TryRepair(client.Breakage, UserUtils.GetNumber(userInput) - 1);
                        break;
                }
            }
        }

        private int CalculateRepairPrice(Breakage breakage)
        {
            int totalPrice;
            Detail currentDetail = _storage.GetDetail(breakage.RepairDetail);

            if (currentDetail != null)
            {
                totalPrice = currentDetail.Price + breakage.PriceRepair;
                return totalPrice;
            }
            else
            {
                Console.WriteLine("Детали нет, учтена цена только за замену!");
                totalPrice = breakage.PriceRepair;
                return totalPrice;
            }
        }

        private void Rejection()
        {
            Console.WriteLine($"Вы заплатили шраф: {_penalty}.");
            _money -= _penalty;
            Console.ReadKey();
        }

        private void TryRepair(Breakage breakage, int index)
        {
            Detail detail = _storage.GetDetail(index);

            if (detail == null)
            {
                Console.WriteLine("Нет такой детали !");
            }
            else if (breakage.RepairDetail == detail.Name)
            {
                Console.WriteLine($"Ремонт прошёл успешно вы заработали {CalculateRepairPrice(breakage)} ");
                _money += CalculateRepairPrice(breakage);
                _storage.RemoveDetail(detail);
            }
            else
            {
                Console.WriteLine($"Вы установили не верную деталь! Клиенту возмещено {breakage.PriceRepair}");
                _money -= breakage.PriceRepair;
            }
            Console.ReadKey();
        }
    }

    internal class Storage
    {
        private List<Detail> _details;

        public Storage()
        {
            _details = new List<Detail>();
            Fill();
        }

        public Detail GetDetail(string name)
        {
            foreach (Detail detail in _details)
            {
                if (name == detail.Name)
                {
                    return detail;
                }
            }
            return null;
        }

        public Detail GetDetail(int index)
        {
            if (index < _details.Count && index > 0)
            {
                Detail detail = _details[index];

                return detail;
            }
            else
            {
                return null;
            }
        }

        public void RemoveDetail(Detail detail)
        {
            _details.Remove(detail);
        }

        public void ShowAllDetails()
        {
            int index = 0;

            foreach (Detail detail in _details)
            {
                index++;
                Console.WriteLine($"{index}.{detail.Name}:{detail.Price}");
            }
        }

        private void Fill()
        {
            DB_Details dB_Details = new DB_Details();
            int minRangeRandom = 1;
            int maxRangeRandom = 16;
            Random random = new Random();
            int countrandom = random.Next(minRangeRandom, maxRangeRandom);

            for (int i = 0; i <= countrandom; i++)
            {
                _details.Add(dB_Details.GetRandomDetail());
            }
        }
    }

    class DB_Details
    {

        private Random _random = new Random();
        private List<Detail> _details;

        public DB_Details()
        {
            _details = new List<Detail>();
            _details.Add(new Detail("Тормозной диск", 2500));
            _details.Add(new Detail("Радиатор", 5000));
            _details.Add(new Detail("Масленный фильтр", 800));
            _details.Add(new Detail("Масло 4л.", 6000));
            _details.Add(new Detail("Фара", 2000));
            _details.Add(new Detail("Бампер", 10000));
            _details.Add(new Detail("Ремень ГРМ", 1500));
            _details.Add(new Detail("Стартер", 8000));
            _details.Add(new Detail("Генератор", 7000));
            _details.Add(new Detail("Помпа", 8000));
        }

        public Detail GetRandomDetail()
        {
            
            int index = _random.Next(0, _details.Count);
            return _details[index];
        }
    }

    class DB_Breakages
    {
        private List<Breakage> _breakages;
        
        public DB_Breakages()
        {
            _breakages= new List<Breakage>();
            _breakages.Add(new Breakage("Замена тормозного диска", "Тормозной диск", 1000));
            _breakages.Add(new Breakage("Протекший радиатор", "Радиатор", 5000));
            _breakages.Add(new Breakage("Лопнул масленный фильтр", "Масленный фильтр", 500));
            _breakages.Add(new Breakage("Замена масла в двигателе", "Масло 4л.", 800));
            _breakages.Add(new Breakage("Разбита фара", "Фара", 2000));
            _breakages.Add(new Breakage("Поцарапан бампер", "Бампер", 3000));
            _breakages.Add(new Breakage("Свистит ремень ГРМ", "Ремень ГРМ", 4000));
            _breakages.Add(new Breakage("Не крутится стартер", "Стартер", 6000));
            _breakages.Add(new Breakage("Не заряжаеться аккамулятор", "Генератор", 7000));
            _breakages.Add(new Breakage("Потек антифриз", "Помпа", 8000));
        }

        public Breakage GetRandomBreakage()
        {
            Random random = new Random();
            int index = random.Next(0, _breakages.Count);
            return _breakages[index];
        }
    }
}