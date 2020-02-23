using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandAPI.Entities
{
  public class Album
  {
    [Key]
    public Guid ID { get; set; }



    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [MaxLength(400)]
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }

    [ForeignKey("BandID")]
    public Band Band { get; set; }
    public Guid BandID { get; set; }
  }
}