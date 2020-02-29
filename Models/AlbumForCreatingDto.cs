using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BandAPI.Attributes;

namespace BandAPI.Models
{
  [TitleAndDescriptionAttributes]
  public class AlbumForCreatingDto // : IValidatableObject
  {
    public Guid ID { get; set; }
    [Required]
    public string Title { get; set; }
    [MaxLength(400)]
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }

    // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    // {
    //   if (Title == Description)
    //   {
    //     yield return new ValidationResult("The title and description need to be different", new[] { "AlbumForCreatingDto" });
    //   }
    // }
  }
}