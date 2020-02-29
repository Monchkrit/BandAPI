using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BandAPI.Attributes;

namespace BandAPI.Models
{
  [TitleAndDescriptionAttributes(ErrorMessage = "Title must be different from description.")]
  public class AlbumForCreatingDto //: IValidatableObject
  {
    public Guid ID { get; set; }
    [Required(ErrorMessage = "The Title is Required.")]
    [MaxLength(200, ErrorMessage = "The Title Must be Up To 200 Characters")]
    public string Title { get; set; }
    [MaxLength(400, ErrorMessage = "The Description Must be Up to 400 Characters")]
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