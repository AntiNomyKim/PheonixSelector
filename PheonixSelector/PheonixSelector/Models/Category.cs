﻿using SQLite;

namespace PheonixSelector.Models
{
    public class Category
    {
        [PrimaryKey]
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
    }
}
