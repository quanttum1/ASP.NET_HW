﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiPizushi.Data.Entities;

[Table("tblCategories")]
public class CategoryEntity : BaseEntity<long>
{
    [StringLength(250)]
    public string Name { get; set; } = String.Empty;

    [StringLength(200)]
    public string Image { get; set; } = String.Empty;    
    
    [StringLength(250)]
    public string Slug { get; set; } = String.Empty;
}
