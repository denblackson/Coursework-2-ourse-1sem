using System;
using BLL;

namespace PL
{
    public class CommandsHelper
    {
        private readonly Service service;

        public CommandsHelper(Service service)
        {
            this.service = service;
        }


        public int ReadCategoryID()
        {
            var allCats = service.GetAllCategories();

            if (allCats.Count == 0)
            {
                Console.WriteLine("Увага! Не існує жодної категорії. Буде використана пуста категорія з ID = 0.");
                return 0;
            }

            Console.WriteLine("Оберіть категорію:\n");


            for (var index = 0; index < allCats.Count; index++)
            {
                var c = allCats[index];
                Console.WriteLine($"{index}: (ID {c.ID}) {c.Name}");
            }

            while (true)
            {
                Console.WriteLine("Введіть номер: ");
                var line = Console.ReadLine();

                if (int.TryParse(line, out var catIndex))
                {
                    if (catIndex >= 0 && catIndex < allCats.Count)
                    {
                        return allCats[catIndex].ID;
                    }
                    else
                    {
                        Console.WriteLine("Помилка! Введіть коректний номер категорії зі списку вище!!");
                    }

                }
                else
                {
                    Console.WriteLine("Помилка! Введіть номер категорії!");
                }
            }
        }


        public int ReadAccountID()
        {
            var allAccs = service.GetAllAccounts();

            if (allAccs.Count == 0)
            {
                Console.WriteLine("Увага! Не існує жодного акаунта. Буде використан стандартний акаунт з ID = 0.");
                return 0;
            }

            Console.WriteLine("Оберіть акаунт:\n");


            for (var index = 0; index < allAccs.Count; index++)
            {
                var a = allAccs[index];
                Console.WriteLine($"{index}: (ID {a.ID}) {a.Name}");
            }

            while (true)
            {
                Console.WriteLine("Введіть номер: ");
                var line = Console.ReadLine();

                if (int.TryParse(line, out var accIndex))
                {
                    if (accIndex >= 0 && accIndex < allAccs.Count)
                    {
                        return allAccs[accIndex].ID;
                    }
                    else
                    {
                        Console.WriteLine("Помилка! Введіть коректний номер акаунта зі списку вище!!");
                    }

                }
                else
                {
                    Console.WriteLine("Помилка при отриманні числа! Введіть номер акаунта!");
                }
            }
        }


        public string ReadAnyString()
        {
            var str = Console.ReadLine();
            return str == null ? "" : str.Trim();
        }
        
        
        public string ReadFilledString()
        {
            while (true)
            {
                var str = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(str))
                {
                    Console.WriteLine("Помилка! Строка пуста! Введіть коректну строку:");
                }
                else
                {
                    return str.Trim();
                }
            }
        }

        public int ReadInt()
        {
            while (true)
            {
                var str = Console.ReadLine();


                if (int.TryParse(str, out var value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Помилка при отриманні числа! Введіть коректе число:");
                }
            }
        }
        
        public decimal ReadDecimal()
        {
            while (true)
            {
                var str = Console.ReadLine();


                if (decimal.TryParse(str, out var value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Помилка! Введіть коректе число:");
                }
            }
        }


        public bool ReadBool()
        {
            while (true)
            {
                Console.WriteLine("Yes/No:");
                var str = Console.ReadLine() ?? "";
                str = str.Trim().ToLower();

                switch (str)
                {
                    case "":
                    case "y":
                    case "yes":
                    case "так":
                    case "т":
                    case "+":
                        return true;
                    case "n":
                    case "no":
                    case "ні":
                    case "н":
                    case "-":
                        return false;
                    default:
                        Console.WriteLine("Помилка! Введіть Yes (Y) або No (N)}!");
                        break;
                }
            }
        }

        public DateTime ReadDate()
        {
            while (true)
            {
                var str = Console.ReadLine();


                if (DateTime.TryParse(str, out var value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Помилка при отриманні дати! Введіть коректну строку формату дд.мм.рррр:");
                }
            }
        }
    }
}