using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AjaxHomework.Models;

public partial class Member
{
    [Key]
    public int MemberId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? Email { get; set; }

    public int? Age { get; set; }

    [StringLength(50)]
    public string? FileName { get; set; }

    public byte[]? FileData { get; set; }

    [StringLength(200)]
    public string? Password { get; set; }

    [StringLength(200)]
    public string? Salt { get; set; }
}
