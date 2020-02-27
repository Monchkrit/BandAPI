using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandAPI.Entities
{
  public class Band
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public DateTime Founded { get; set; }

    [MaxLength(50)]
    public string MainGenre { get; set; }
    public ICollection<Album> Albums { get; set; } = new List<Album>();
  }
}