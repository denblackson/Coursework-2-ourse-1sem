using System.Collections.Generic;

namespace DAL.Entities
{
    public class Account
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}