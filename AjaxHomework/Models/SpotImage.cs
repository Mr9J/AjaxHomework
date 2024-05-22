using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AjaxHomework.Models;

public partial class SpotImage
{
    [Key]
    public int ImageId { get; set; }

    public int? SpotId { get; set; }

    public string? ImageTitle { get; set; }

    public string? ImagePath { get; set; }
}
