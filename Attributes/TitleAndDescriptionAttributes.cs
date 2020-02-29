using System.ComponentModel.DataAnnotations;
using BandAPI.Models;

namespace BandAPI.Attributes
{
  public class TitleAndDescriptionAttributes : ValidationAttribute
  {
      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
        var album = (AlbumManipulationDto)validationContext.ObjectInstance;

        if (album.Title == album.Description)
        {
          return new ValidationResult("The title and description need to be different. Validation failed.", new[] { "AlbumManipulationDto" });
        }

        return ValidationResult.Success;
      }
  }
}