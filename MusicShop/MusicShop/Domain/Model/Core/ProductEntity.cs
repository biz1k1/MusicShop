﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace MusicShop.Domain.Model.Core
{
    [Index(nameof(Name), IsUnique = true)]
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InStock { get; set; }
        public double Price { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}
