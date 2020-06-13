using System.Reflection;

namespace BandAPI.Services
{
  public class PropertyValidationService : IPropertyValidationService
  {
    public bool HasValidParameters<T>(string fields)
    {
      if (string.IsNullOrWhiteSpace(fields))
        return true;

      var fieldsAfterSplit = fields.Split(",");

      foreach (var field in fieldsAfterSplit)
      {
        var propertyName = field.Trim();
        var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (propertyInfo is null)
          return false;
      }

      return true;
    }
  }
}