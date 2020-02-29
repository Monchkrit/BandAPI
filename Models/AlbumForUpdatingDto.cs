using System;
using System.ComponentModel.DataAnnotations;
using BandAPI.Attributes;

namespace BandAPI.Models
{  
  public class AlbumForUpdatingDto : AlbumManipulationDto
  {
    [Required(ErrorMessage = "The updated album record requires description.")]
    public override string Description { get => base.Description; set => base.Description = value; }
  }
}