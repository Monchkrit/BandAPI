using System;
using System.ComponentModel.DataAnnotations;
using BandAPI.Attributes;

namespace BandAPI.Models
{
  [TitleAndDescriptionAttributes(ErrorMessage = "Title must be different from description.")]
  public abstract class AlbumManipulationDto
  {
    [Required(ErrorMessage = "The Title is Required.")]
    [MaxLength(200, ErrorMessage = "The Title Must be Up To 200 Characters")]
    public string Title { get; set; }
    
    [MaxLength(400, ErrorMessage = "The Description Must be Up to 400 Characters")]
    public virtual string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
  }
}