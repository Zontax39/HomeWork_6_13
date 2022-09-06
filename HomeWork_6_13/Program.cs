using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_6_13
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }


        class Client
        {
            private List<Breakage> _breakages;
            
            public Client()
            {
                _breakages = new List<Breakage>();
            }


        }

        class Storage
        {
            private List<Detail> _details;

            public Storage() 
            {
                _details = new List<Detail>();
                FillingStorage();
            }

            private void FillingStorage()
            {
                int minRangeRandom = 1;
                int maxRangeRandom = 101; 
                Random random = new Random();
                int countrandom = random.Next(minRangeRandom, maxRangeRandom);

                for (int i = 0; i <= countrandom; i++)
                {
                    minRangeRandom = 1;
                    maxRangeRandom = 11;
                    int randomDetail = random.Next(minRangeRandom, maxRangeRandom);

                    switch (randomDetail)
                    {
                        case 1:
                            _details.Add(new Detail("Тормозной диск", 2500));
                            break;
                        case 2:
                            _details.Add(new Detail("Радиатор", 5000));
                            break;
                        case 3:
                            _details.Add(new Detail("Масленный фильтр", 800));
                            break;
                        case 4:
                            _details.Add(new Detail("Масло 4л.", 6000));
                            break;
                        case 5:
                            _details.Add(new Detail("Фара", 2000));
                            break;
                        case 6:
                            _details.Add(new Detail("Бампер", 10000));
                            break;
                        case 7:
                            _details.Add(new Detail("Ремень ГРМ", 1500));
                            break;
                        case 8:
                            _details.Add(new Detail("Стартер", 8000));
                            break;
                        case 9:
                            _details.Add(new Detail("Генератор", 7000));
                            break;
                        case 10:
                            _details.Add(new Detail("Помпа", 8000));
                            break;
                    }
                }
            }
        }

        class Detail
        {
            public string Name { get; private set; }
            public int Price { get; private set; }

            public Detail(string name, int price)
            {
                Name = name;
                Price = price;
            }
        }

        class Breakage
        {
            public string Name { get; private set; }
            public int PriceRepair { get; private set; }
            public Detail RepairDetail { get; private set; }

            public Breakage(string name, int price)
            {
                Name = name;
                PriceRepair = price;
            }
        }
    }
}
