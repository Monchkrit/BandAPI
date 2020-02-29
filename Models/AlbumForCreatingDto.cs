using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BandAPI.Attributes;

namespace BandAPI.Models
{
  
  public class AlbumForCreatingDto : AlbumManipulationDto //: IValidatableObject
  {
    // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    // {
    //   if (Title == Description)
    //   {
    //     yield return new ValidationResult("The title and description need to be different", new[] { "AlbumForCreatingDto" });
    //   }
    // }
  }
}