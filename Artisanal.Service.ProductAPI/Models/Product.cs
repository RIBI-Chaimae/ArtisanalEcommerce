﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Artisanal.Service.ProductAPI.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required,StringLength(25)]
        public string ProductName { get; set; }
        [Range(1,1000)]
        public double Price { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }

    }
}
