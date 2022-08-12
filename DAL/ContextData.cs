using System.Collections.Generic;
using DAL.Entities;

namespace DAL
{
    public class ContextData
    {
        
        public List<Category> Categories { get; set; } = new List<Category>();
        
        public List<Account> Accounts { get; set; } = new List<Account>();
        
        
        public Dictionary<string, int> IDs { get; set; } = new Dictionary<string, int>()
        {
            {"accountID", 0},
            {"categoryID", 0},
            {"transactionID", 0}
        };
    }
}