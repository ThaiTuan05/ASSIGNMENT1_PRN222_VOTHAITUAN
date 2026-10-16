using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.Entities;

[Table("SystemAccount")]
public partial class SystemAccount
{
    [Key]
    [Column("AccountID")]
    public short AccountId { get; set; }

    [Required(ErrorMessage = "Account name is required.")]
    [StringLength(100)]
    public string? AccountName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(70)]
    public string? AccountEmail { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public int? AccountRole { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(70)]
    public string? AccountPassword { get; set; }

    [InverseProperty("CreatedBy")]
    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
