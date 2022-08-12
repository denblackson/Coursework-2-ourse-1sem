using System;
using System.Collections.Generic;
using System.Linq;
using BLL.EntitiesDTO;
using DAL;
using DAL.Entities;

namespace BLL
{
    public class Service
    {
        private readonly Context _context;

        private IDGenerator categoryIDG;
        private IDGenerator transactionIDG;
        private IDGenerator accountIDG;

        public Service()
        {
            _context = new Context();
            _context.JSON_DeSerialize();
            
            categoryIDG = new IDGenerator(_context, "categoryID");
            transactionIDG = new IDGenerator(_context, "transactionID");
            accountIDG = new IDGenerator(_context, "accountID");
        }

        public void ClearContextData()
        {
            _context.ContextData = new ContextData();
            _context.JSON_Serialize();
            
            categoryIDG = new IDGenerator(_context, "categoryID");
            transactionIDG = new IDGenerator(_context, "transactionID");
            accountIDG = new IDGenerator(_context, "accountID");
        }


        public void AddAccount(AccountDTO accountDTO)
        {
            int id = accountIDG.GetNextID();

            Account account = new Account()
            {
                ID = id,
                Name = accountDTO.Name,
                Description = accountDTO.Description
            };

            _context.ContextData.Accounts.Add(account);
            _context.JSON_Serialize();
        }


        public void AddCategory(CategoryDTO categoryDTO)
        {
            int id = categoryIDG.GetNextID();

            Category category = new Category()
            {
                ID = id,
                Name = categoryDTO.Name,
                Description = categoryDTO.Description
            };

            _context.ContextData.Categories.Add(category);
            _context.JSON_Serialize();
        }


        public void AddTransaction(TransactionDTO transactionDTO, int accountID)
        {
            Account acc = _context.ContextData.Accounts.Find(account => account.ID == accountID);
            
            Transaction newTransaction = new Transaction()
            {
                ID = transactionIDG.GetNextID(),
                CategoryID = transactionDTO.CategoryID,
                Date = transactionDTO.Date,
                Description = transactionDTO.Description,
                Money = transactionDTO.Money
            };
            
            acc.Transactions.Add(newTransaction);
            
            _context.JSON_Serialize();
        }

        public void MoveMoney(int originAccID, int destAccID, decimal moneyAmount, int categoryID)
        {
            var originAccName = GetAllAccounts().Find(a => a.ID == originAccID).Name;
            var destinationAccName = GetAllAccounts().Find(a => a.ID == destAccID).Name;
            
            var transFrom = new TransactionDTO()
            {
                Date = DateTime.Now,
                Description = $"Переказ до акаунта {destinationAccName} (ID:{destAccID})",
                Money = -moneyAmount,
                CategoryID = categoryID
            };
            
            var transTo = new TransactionDTO()
            {
                Date = DateTime.Now,
                Description = $"Переказ з акаунта {originAccName} (ID:{originAccID})",
                Money = moneyAmount,
                CategoryID = categoryID
            };
            
            AddTransaction(transFrom, originAccID);
            AddTransaction(transTo, destAccID);
        }

        public List<TransactionDTO> GetAllTransactions(AccountDTO accountDTO)
        {
            Account account = _context.ContextData.Accounts.Find(a => a.ID == accountDTO.ID);
            List<Transaction> transactions = account.Transactions;

            return transactions.Select(t =>
                new TransactionDTO()
                {
                    ID = t.ID,
                    CategoryID = t.CategoryID,
                    Date = t.Date,
                    Description = t.Description,
                    Money = t.Money

                }).ToList();
        }


        public void DeleteTransaction(int id)
        {
            List<Account> accList = _context.ContextData.Accounts;

            foreach (var acc in accList)
            {
                foreach (var transaction in acc.Transactions)
                {
                    if (transaction.ID == id)
                    {
                        acc.Transactions.Remove(transaction);
                        _context.JSON_Serialize();
                        return;
                    }
                }
            }
            
            throw new Exception("Транзакція не знайдена!");
        }

        public void DeleteAccount(int id)
        {
            Account account = _context.ContextData.Accounts.Find(acc => acc.ID == id);
            
            if (account == null) throw new Exception("Намагаєтесь відалити нульовий аккаунт!");

            _context.ContextData.Accounts.Remove(account);
            
            _context.JSON_Serialize();
        }

        public void DeleteCategory(int id)
        {
            Category category = _context.ContextData.Categories.Find(c => c.ID == id);
            
            if (category == null) throw new Exception("Намагаєтесь видалити нульову категорію!");
            
            _context.ContextData.Categories.Remove(category);
            
            _context.JSON_Serialize();
        }


        public void UpdateAccountInfo(AccountDTO 
            accountDTO)
        {
            Account account = _context.ContextData.Accounts.Find(a => a.ID == accountDTO.ID);
            
            account.Name = accountDTO.Name;
            account.Description = accountDTO.Description;
            
            _context.JSON_Serialize();
        }


        public List<AccountDTO> GetAllAccounts()
        {
            List<Account> accounts = _context.ContextData.Accounts;

            return accounts.Select(acc =>
                new AccountDTO()
                {
                    ID = acc.ID,
                    Name = acc.Name,
                    Description = acc.Description,
                    TransactionIDs = acc.Transactions.Select(transaction => transaction.ID).ToList()
                }).ToList();
        }

        public void UpdateCategoryInfo(CategoryDTO categoryDTO)
        {
            Category category = _context.ContextData.Categories.Find(c => c.ID == categoryDTO.ID);
            
            category.Name = categoryDTO.Name;
            category.Description = categoryDTO.Description;
            
            _context.JSON_Serialize();
        }


        public List<CategoryDTO> GetAllCategories()
        {
            List<Category> categories = _context.ContextData.Categories;

            return categories.Select(cat =>
                new CategoryDTO()
                {
                    ID = cat.ID,
                    Name = cat.Name,
                    Description = cat.Description
                }).ToList();
        }


        public decimal GetAccountMoney(int accountID)
        {
            Account account = _context.ContextData.Accounts.Find(a => a.ID == accountID);
            return account.Transactions.Sum(t => t.Money);
        }
    }
}