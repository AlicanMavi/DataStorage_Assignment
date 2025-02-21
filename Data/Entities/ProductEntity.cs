﻿using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ProductName { get; set; } = null!;

    public decimal ProductPrice { get; set; }
}