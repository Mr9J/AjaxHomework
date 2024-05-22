using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AjaxHomework.Models;

public partial class Spot
{
    [Key]
    public int SpotId { get; set; }

    public int? CategoryId { get; set; }

    [StringLength(50)]
    public string? SpotTitle { get; set; }

    public string? SpotDescription { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    public string? TrafficInfo { get; set; }

    [StringLength(20)]
    public string? Longitude { get; set; }

    [StringLength(20)]
    public string? Latitude { get; set; }

    public string? OpenTime { get; set; }

    public string? ContactPhone { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }
}
