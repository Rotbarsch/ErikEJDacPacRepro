using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ReproDB.Entities.Entities
{
[Table("language", Schema = "mat2")]
    public partial class MATLanguage1
    {
        [Key]
        [Column("language")]
        public int Language { get; set; }
        [Column("rfc_1766")]
        [StringLength(5)]
        public string Rfc1766 { get; set; } = null!;
        [Column("text")]
        [StringLength(255)]
        public string? Text { get; set; }
    }
}
