using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPProject.Models.Home
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
    }
}