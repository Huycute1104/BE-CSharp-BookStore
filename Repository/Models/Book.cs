﻿using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Repository.Models
{
    public partial class Book
    {
        public Book()
        {
            Carts = new HashSet<Cart>();
            Images = new HashSet<Image>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BookId { get; set; }
        public string BookName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
