using System;

namespace BLL.EntitiesDTO
{
    public class TransactionDTO
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public DateTime Date { get; set; }
        public decimal Money { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Транзакція [ID{ID}]: {Money} грошей переведено {Date.ToString("dd.MM.yyyy")} числа о {Date.ToString("HH:mm:ss")}. " +
                   $"Категорія: {CategoryID}. Описання: {Description}.";
        }
    }
}