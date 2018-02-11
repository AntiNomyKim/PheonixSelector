using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PheonixSelector.Models
{
   public class Category
    {
        [PrimaryKey]
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
    }
}
