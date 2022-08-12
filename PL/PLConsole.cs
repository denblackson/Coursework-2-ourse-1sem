using System;
using System.Text;

namespace PL
{
    public class PLConsole
    {

        private CommandsHolder commands = new CommandsHolder();
        
        public PLConsole()
        {
           Console.OutputEncoding = Encoding.Unicode;
           Console.InputEncoding = Encoding.Unicode;
            
            while (true)
            {
                AskForCommand();
            }
        }
        
        
        public void AskForCommand()
        {
            Console.WriteLine("Оберіть команду:");
            Console.WriteLine("......................");
            
            Console.WriteLine("0: AddAccount [створити новий акаунт]");
            Console.WriteLine("1: AddCategory [створити нову категорію]");
            Console.WriteLine("2: AddTransaction [створити новий переказ]");
            Console.WriteLine();
            
            Console.WriteLine("3: DeleteAccount [видалити акаунт]");
            Console.WriteLine("4: DeleteCategory [видалити категорію]");
            Console.WriteLine("5: DeleteTransaction [видалити переказ]");
            Console.WriteLine();
            
            Console.WriteLine("6: UpdateAccount [змінити дані про акаунт]");
            Console.WriteLine("7: UpdateCategory [змінити дані про категорію]");
            Console.WriteLine("8: UpdateTransaction [змінити дані про переказ]");
            Console.WriteLine();


            Console.WriteLine("9: Move [переказ коштів між акаунтами]");
            Console.WriteLine("10: Find [знайти переказ по сумі чи даті]");
            Console.WriteLine("11: Period [знайти транзакції за певний період]");
            Console.WriteLine("12: AccountsInfo [інформація по акаунтах]");
            Console.WriteLine("13: CategoriesInfo [інформація по категоріям]");
            Console.WriteLine("......................");
            
            Console.Write("Номер: ");
            var command = Console.ReadLine();

            if (int.TryParse(command, out var value))
            {
                ExecuteCommand(value);
            }
            else
            {
                Console.WriteLine("Помилка вводу! Введіть номер команди!");
                return;
            }
            
        }

        private void ExecuteCommand(int num)
        {
            Console.WriteLine("++++++++++++++++++++");
            switch (num)
            {
                case 0:
                    commands.AddAccount();
                    Wait();
                    break;
                case 1:
                    commands.AddCategory();
                    Wait();
                    break;
                case 2:
                    commands.AddTransaction();
                    Wait();
                    break;
                case 3:
                    commands.DeleteAccount();
                    Wait();
                    break;
                case 4:
                    commands.DeleteCategory();
                    Wait();
                    break;
                case 5:
                    commands.DeleteTransaction();
                    Wait();
                    break;
                case 6:
                    commands.UpdateAccount();
                    Wait();
                    break;
                case 7:
                    commands.UpdateCategory();
                    Wait();
                    break;
                case 8:
                    commands.UpdateTransaction();
                    Wait();
                    break;
                case 9:
                    commands.Move();
                    Wait();
                    break;
                case 10:
                    commands.Find();
                    Wait();
                    break;
                case 11:
                    commands.Period();
                    Wait();
                    break;
                case 12:
                    commands.AccountsInfo();
                    Wait();
                    break;
                case 13:
                    commands.CategoriesInfo();
                    Wait();
                    break;

                default:
                    Console.WriteLine("Помилка! Введіть інсуючий номер команди!");
                    break;
            }
            
            
            Console.WriteLine("++++++++++++++++++++");
            
        }

        private void Wait()
        {
            Console.WriteLine("Готово! Натисніть будь-що щоб продовжити.");
            Console.ReadKey();
        }

    }
}