using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expense_tracker.Model
{
    public class Item
    {
        public int id { get; set; }
        public DateTime? date { get; set; }
        public string? description { get; set; }
        public decimal? amount { get; set; }
        public Item(int id, string description, decimal amount)
        {
            this.id = id;
            this.description = description;
            this.amount = amount;
            this.date = DateTime.Now;
        }
    }
}
