using System;

namespace DAL.Entities
{
    public class Transaction
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public DateTime Date { get; set; }
        public decimal Money { get; set; }
        public string Description { get; set; }
        
    }
}