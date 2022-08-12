using System;
using BLL;
using BLL.EntitiesDTO;
using Xunit;

namespace Tests
{
    public class ServiceTests
    {

        #region TestData
        private AccountDTO account0 = new AccountDTO()
        {
            Name = "Acc0 Name",
            Description = "Acc0 Description"
        };
        
        private AccountDTO account1 = new AccountDTO()
        {
            Name = "Acc1 Name",
            Description = "Acc1 Description"
        };

        private TransactionDTO transaction0 = new TransactionDTO()
        {
            Date = new DateTime(2021,12,10),
            CategoryID = 0,
            Description = "T0 description",
            Money = 250
        };
        
        private TransactionDTO transaction1 = new TransactionDTO()
        {
            Date = new DateTime(2021,12,11),
            CategoryID = 0,
            Description = "T1 description",
            Money = -400
        };
        
        private TransactionDTO transaction2 = new TransactionDTO()
        {
            Date = new DateTime(2021,12,12),
            CategoryID = 1,
            Description = "T2 description",
            Money = 700
        };

        private CategoryDTO category0 = new CategoryDTO()
        {
            Name = "C0",
            Description = "C0 category description",
        };
        
        private CategoryDTO category1 = new CategoryDTO()
        {
            Name = "C1",
            Description = "C1 category description",
        };
        #endregion
        
        
        [Fact]
        public void AddAccountTest()
        {
            var service = new Service();
            service.ClearContextData();
            
            service.AddAccount(account0);
            service.AddAccount(account1);
            
            Assert.Equal(2, service.GetAllAccounts().Count);
            Assert.Equal("Acc0 Name", service.GetAllAccounts()[0].Name);
            Assert.Equal("Acc1 Description", service.GetAllAccounts()[1].Description);
        }
        
        
        [Fact]
        public void DeleteAccountTest()
        {
            var service = new Service();
            service.ClearContextData();
            
            service.AddAccount(account0);
            service.AddAccount(account1);
            service.DeleteAccount(account0.ID);
            
            Assert.Equal(1, service.GetAllAccounts().Count);
            Assert.Equal("Acc1 Description", service.GetAllAccounts()[0].Description);

            Assert.Throws<Exception>(()=>service.DeleteAccount(12345));
        }
        
        [Fact]
        public void UpdateAccountInfo()
        {
            var service = new Service();
            service.ClearContextData();
            
            service.AddAccount(account0);
            service.AddAccount(account1);
            
        AccountDTO account0New = new AccountDTO()
        {
            ID = 0,
            Name = "Acc0 NewName",
            Description = "Acc0 NewDescription"
        };
        
        AccountDTO account1New = new AccountDTO()
        {
            ID = 1,
            Name = "Acc1 Name",
            Description = "Acc1 NewDescription"
        };
            
            service.UpdateAccountInfo(account0New);
            service.UpdateAccountInfo(account1New);
            
            Assert.Equal("Acc0 NewName", service.GetAllAccounts()[0].Name);
            Assert.Equal("Acc1 Name", service.GetAllAccounts()[1].Name);
            
            Assert.Equal("Acc0 NewDescription", service.GetAllAccounts()[0].Description);
            Assert.Equal("Acc1 NewDescription", service.GetAllAccounts()[1].Description);
        }
        
        [Fact]
        public void GetAllAccountsTest()
        {
            var service = new Service();
            service.ClearContextData();
            
            service.AddAccount(account0);
            service.AddAccount(account1);
            
            service.AddTransaction(transaction0, 0);
            service.AddTransaction(transaction1, 0);
            service.AddTransaction(transaction2, 1);

            var accounts = service.GetAllAccounts();
            
            Assert.Equal(2, accounts[0].TransactionIDs.Count);
            Assert.Equal(1, accounts[1].TransactionIDs.Count);
            
            Assert.Equal(1, accounts[0].TransactionIDs[1]);
            Assert.Equal(2, accounts[1].TransactionIDs[0]);
            
        }
        
        
        [Fact]
        public void GetAccountMoneyTest()
        {
            var service = new Service();
            service.ClearContextData();
            
            service.AddAccount(account0);
            service.AddAccount(account1);
            
            service.AddTransaction(transaction0, 0);
            service.AddTransaction(transaction1, 0);
            service.AddTransaction(transaction2, 1);

            var money0 = service.GetAccountMoney(0);
            var money1 = service.GetAccountMoney(1);
            
            Assert.Equal(-150, money0);
            Assert.Equal(700, money1);
            
            
        }
        
        
        [Fact]
        public void MoveMoneyTest()
        {
            var service = new Service();
            service.ClearContextData();
            
            service.AddAccount(account0);
            service.AddAccount(account1);
            
            service.AddTransaction(transaction0, 0);
            service.AddTransaction(transaction1, 0);
            service.AddTransaction(transaction2, 1);
            
            service.MoveMoney(1, 0, 300, 0);

            var money0 = service.GetAccountMoney(0);
            var money1 = service.GetAccountMoney(1);
            
            Assert.Equal(150, money0);
            Assert.Equal(400, money1);
        }
        
        
        [Fact]
        public void AddCategoryTest()
        {
            var service = new Service();
            service.ClearContextData();
            
            service.AddCategory(category0);
            service.AddCategory(category1);
            
            Assert.Equal(2, service.GetAllCategories().Count);
            Assert.Equal("C0", service.GetAllCategories()[0].Name);
            Assert.Equal("C1 category description", service.GetAllCategories()[1].Description);
        }

    }
}