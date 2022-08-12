using System.Collections.Generic;

namespace BLL.EntitiesDTO
{
    public class AccountDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> TransactionIDs { get; set; } = new List<int>();
    }
}