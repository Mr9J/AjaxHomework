using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AjaxHomework.Models;

[Table("Address")]
public partial class Address
{
    [Key]
    public int Id { get; set; }

    [Column("city")]
    [StringLength(10)]
    public string? City { get; set; }

    [Column("site_id")]
    [StringLength(50)]
    public string? SiteId { get; set; }

    [Column("road")]
    [StringLength(200)]
    public string? Road { get; set; }
}
