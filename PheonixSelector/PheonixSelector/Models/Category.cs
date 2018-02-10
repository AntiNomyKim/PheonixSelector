using System;
using System.Collections.Generic;
using System.Text;

namespace PheonixSelector.Models
{
   public class Category
    {
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public List<Item> ItemList { get; set; }
    }
}
