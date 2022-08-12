using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using BLL.EntitiesDTO;

namespace PL
{
    public class CommandsHolder
    {
        private readonly Service service;
        private readonly CommandsHelper helper;

        public CommandsHolder()
        {
            service = new Service();
            helper = new CommandsHelper(service);
        }


        //Console.WriteLine("0: AddAccount [створити новий акаунт]");
        public void AddAccount()
        {
            Console.WriteLine("Введіть ім'я нового акаунту:");
            var name = helper.ReadFilledString();
            
            Console.WriteLine("Введіть описання нового акаунту:");
            var description = helper.ReadAnyString();

            
            var newAccount = new AccountDTO()
            {
                Name = name,
                Description = description
            };
            
            service.AddAccount(newAccount);
        }
        
        //Console.WriteLine("1: AddCategory [створити нову категорію]");
        public void AddCategory()
        {
            Console.WriteLine("Введіть ім'я нової категорії:");
            var name = helper.ReadFilledString();

            Console.WriteLine("Введіть описання нової категорії:");
            var description = helper.ReadAnyString();

            var  newCategory = new CategoryDTO()
            {
                Name = name,
                Description = description
            };
            
            service.AddCategory(newCategory);
        }

        //Console.WriteLine("2: AddTransaction [створити новий переказ]");
        public void AddTransaction()
        {
            Console.WriteLine("Введіть кількість грошей:");
            decimal money = helper.ReadDecimal();
            
            Console.WriteLine("Введіть описання транзакції:");
            string description = helper.ReadAnyString();

            int accountID = helper.ReadAccountID();
            
            int categoryID = helper.ReadCategoryID();
            
            
            var newTransaction = new TransactionDTO()
            {
                Description = description,
                Date = DateTime.Now,
                Money = money,
                CategoryID = categoryID
            };
            
            service.AddTransaction(newTransaction, accountID);
        }
        
        

        //Console.WriteLine("3: DeleteAccount [видалити акаунт]");
        public void DeleteAccount()
        {
            Console.WriteLine("Оберіть акаунт для видалення:");
            int accountID = helper.ReadAccountID();
            service.DeleteAccount(accountID);
        }

        //Console.WriteLine("4: DeleteCategory [видалити категорію]");
        public void DeleteCategory()
        {
            Console.WriteLine("Оберіть категорію для видалення:");
            int categoryID = helper.ReadCategoryID();
            service.DeleteCategory(categoryID);
        }

        //Console.WriteLine("5: DeleteTransaction [видалити переказ]");
        public void DeleteTransaction()
        {
            Console.WriteLine("Введіть ID транзакції для видалення:");
            int ID = helper.ReadInt();
            
            var accounts = service.GetAllAccounts();
            var transactions = accounts.SelectMany(acc => service.GetAllTransactions(acc)).ToList();

            if (transactions.Find(t => t.ID == ID) == null)
            {
                Console.WriteLine("Помилка! Транзакція з таким ID не знайдена!");
                return;
            }
            
            service.DeleteTransaction(ID);
        }
        

        //Console.WriteLine("6: UpdateAccount [змінити дані про акаунт]");
        public void UpdateAccount()
        {
            int id = helper.ReadAccountID();
            
            Console.WriteLine("Введіть нове ім'я або нічого щоб не змінювати його.");
            var name = helper.ReadAnyString();

            Console.WriteLine("Введіть нове описання або нічого щоб не змінювати його.");
            var description = helper.ReadAnyString();


            var originalAccount = service.GetAllAccounts().Find(a => a.ID == id);

            var  newAccount = new AccountDTO()
            {
                ID = id,
                Name = name == "" ? originalAccount.Name : name,
                Description = description == "" ? originalAccount.Description : description
            };

            service.UpdateAccountInfo(newAccount);
        }

        //Console.WriteLine("7: UpdateCategory [змінити дані про категорію]");
        public void UpdateCategory()
        {
            int id = helper.ReadCategoryID();
            
            Console.WriteLine("Введіть нове ім'я або нічого щоб не змінювати його.");
            var name = helper.ReadAnyString();
            
            Console.WriteLine("Введіть нове описання або нічого щоб не змінювати його.");
            var description = helper.ReadAnyString();
            
            
            var originalCategory = service.GetAllCategories().Find(c => c.ID == id);

            var newCategory = new CategoryDTO()
            {
                ID = id,
                Name = name == "" ? originalCategory.Name : name,
                Description = description == "" ? originalCategory.Description : description
            };

            service.UpdateCategoryInfo(newCategory);
        }

        //Console.WriteLine("8: UpdateTransaction [змінити дані про переказ]");
        public void UpdateTransaction()
        {
            Console.WriteLine("Редагування транзакцій заборонено. Ви можете видалити транзакцію та створити нову.");
        }


        //Console.WriteLine("9: Move [переказ коштів між акаунтами]");
        public void Move()
        {
            Console.WriteLine("Акаунт з якого відправляються кошти:");
            int origAccountId = helper.ReadAccountID();
            
            Console.WriteLine("Акаунт на який відправляються кошти:");
            int destinationAccId = helper.ReadAccountID();
            
            int categoryId = helper.ReadCategoryID();
            
            Console.WriteLine("Кількість коштів:");
            decimal money = helper.ReadDecimal();

            service.MoveMoney(origAccountId, destinationAccId, money, categoryId);
        }
        
        //Console.WriteLine("10: Find [знайти переказ по сумі чи даті]");
        public void Find()
        {
            var accounts = service.GetAllAccounts();
            var transactions = accounts.SelectMany(acc => service.GetAllTransactions(acc)).ToList();
            
            
            Console.WriteLine("Для пошуку по даті введіть [date], для пошуку по сумі введіть [money]:");
            var str = helper.ReadFilledString().ToLower();

            if (str.Equals("date"))
            {
                Console.WriteLine("Введіть дату для пошуку:");
                var date = helper.ReadDate();
                
                var transactionsByDate = transactions.FindAll(trans => trans.Date.Day == date.Day);
                transactionsByDate.Sort((t1, t2) => t1.Date.CompareTo(t2.Date));
                
                Console.WriteLine("   Транзакції:");
                foreach (var t in transactionsByDate)
                {
                    Console.WriteLine("      "+t.ToString());
                }
            }
            else if (str.Equals("money"))
            {
                Console.WriteLine("Введіть суму для пошуку:");
                var money = helper.ReadDecimal();

                var transList = transactions.FindAll(trans => trans.Money == money || trans.Money == -money);
                transList.Sort((t1, t2) => t1.Date.CompareTo(t2.Date));
                
                Console.WriteLine("   Транзакції:");
                foreach (var t in transList)
                {
                    Console.WriteLine("      "+t.ToString());
                }
            }
            else
            {
                Console.WriteLine($"Хибний аргумент: {str}");
            }
        }
        
        //Console.WriteLine("11: Period [знайти транзакції за певний період]");
        public void Period()
        {
            Console.WriteLine("Введіть дату А:");
            var dateA = helper.ReadDate();
            
            Console.WriteLine("Введіть дату Б:");
            var dateB = helper.ReadDate();
            dateB = dateB.AddDays(1).AddTicks(-1);
            
            var accounts = service.GetAllAccounts();
            var transactions = accounts.SelectMany(acc => service.GetAllTransactions(acc)).ToList();

  
            var transList = transactions.FindAll(trans => trans.Date >= dateA.Date && trans.Date <= dateB);
            transList.Sort((t1, t2) => t1.Date.CompareTo(t2.Date));
            Console.WriteLine($"Транзакції за період з ({dateA.Date}) по ({dateB}):");

            var counter = dateA.Day;
            List<TransactionDTO> singleDayTrans = new List<TransactionDTO>();
            for (var index = 0; index < transList.Count; index++)
            {
                var t = transList[index];
                
                if (t.Date.Day != counter)
                {
                    counter = t.Date.Day;
                    
                    PrintSortedByCatTransactions(singleDayTrans);
                    
                    singleDayTrans.Clear();
                    
                    Console.WriteLine($"Дата: {t.Date.ToString("dd.MM.yyyy")}:");
                }

                singleDayTrans.Add(t);
                
            }

            PrintSortedByCatTransactions(singleDayTrans);
            
            
            void PrintSortedByCatTransactions(List<TransactionDTO> singleDayTransList)
            {
                var categoriesSorted = GetTransListByCategories(singleDayTransList);

                foreach (var catTrDict in categoriesSorted)
                {
                    if (catTrDict.Value == null || catTrDict.Value.Count == 0) continue;

                    Console.WriteLine($" Категорія {catTrDict.Key.Name} (ID:{catTrDict.Key.ID}):");
                    var trans = catTrDict.Value;
                    
                    Console.WriteLine("   Транзакції:");
                    foreach (var t in trans)
                    {
                        Console.WriteLine("      "+t.ToString());
                    }
                }
            }

            Dictionary<CategoryDTO, List<TransactionDTO>> GetTransListByCategories(List<TransactionDTO> list)
            {
                var res = new Dictionary<CategoryDTO, List<TransactionDTO>>();

                foreach (var cat in service.GetAllCategories())
                {
                    res.Add(cat, list.FindAll(t => t.CategoryID == cat.ID));
                }

                return res;
            }
        }
        
        //Console.WriteLine("12: AccountsInfo [інформація по акаунтах]");
        public void AccountsInfo()
        {
            var accounts = service.GetAllAccounts();

            Console.WriteLine("Виводити список транзакцій?");
            var fullPrint = helper.ReadBool();

            Console.WriteLine("Акаунти:");
            foreach (var account in accounts)
            {
                var money = service.GetAccountMoney(account.ID);
                Console.WriteLine($"ID {account.ID}: Ім'я: {account.Name}, загальна сума грошей: {money}, описання: {account.Description}.");
                
                if (fullPrint)
                {
                    Console.WriteLine("   Транзакції:");
                    var tr = service.GetAllTransactions(account);
                    foreach (var t in tr)
                    {
                        Console.WriteLine("      "+t.ToString());
                    }
                    Console.WriteLine();

                }
            }
        }
        
        //Console.WriteLine("13: CategoriesInfo [інформація по категоріям]");
        public void CategoriesInfo()
        {
            var categories = service.GetAllCategories();
            var accounts = service.GetAllAccounts();
            var transactions = accounts.SelectMany(acc => service.GetAllTransactions(acc)).ToList();

            Console.WriteLine("Виводити список транзакцій?");
            var fullPrint = helper.ReadBool();
            
            foreach (var c in categories)
            {
                Console.WriteLine($"ID {c.ID}: Ім'я: {c.Name}, описання: {c.Description}.");
                if (fullPrint)
                {
                    Console.WriteLine("   Транзакції:");
                    var transList = transactions.FindAll(trans => trans.CategoryID == c.ID);
                    transList.Sort((t1, t2) => t1.Date.CompareTo(t2.Date));
                    foreach (var t in transList)
                    {
                        Console.WriteLine("      "+t.ToString());
                    }
                    Console.WriteLine();
                }
            }
        }
        
        
    }
}