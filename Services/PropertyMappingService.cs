using System;
using System.Collections.Generic;
using System.Linq;
using BandAPI.Entities;
using BandAPI.Models;

namespace BandAPI.Services
{

  public class PropertyMappingService : IPropertyMappingService
  {
    private Dictionary<string, PropertyMappingValue> _bandPropertyMapping =
      new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
      {
        {"ID", new PropertyMappingValue(new List<string>() {"ID"}) },
        {"Name", new PropertyMappingValue(new List<string>() {"Name"}) },
        {"MainGenre", new PropertyMappingValue(new List<string>() {"MainGenre"}) },
        {"FoundedYearsAgo", new PropertyMappingValue(new List<string>() {"Founded"}, true) }
      };

    private IList<IPropertyMappingMarker> _propertyMappings = new List<IPropertyMappingMarker>();

    public PropertyMappingService()
    {
      _propertyMappings.Add(new PropertyMapping<BandDto, Band>(_bandPropertyMapping));
    }

    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
    {
      var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

      if (matchingMapping.Count() == 1)
        return matchingMapping.First().MappingDictionary;

      throw new Exception("Failed to locate mapping");
    }

    public bool ValidMappingExists<TSource, TDestination>(string fields)
    {
      var propertyMapping = GetPropertyMapping<TSource, TDestination>();

      if (string.IsNullOrWhiteSpace(fields))
        return true;

      var fieldsAfterSplit = fields.Split(",");

      foreach (var field in fieldsAfterSplit)
      {
        var trimmedField = field.Trim();

        var indexOfSpace = trimmedField.IndexOf(" ");
        var propertyName = indexOfSpace == -1 ? trimmedField : trimmedField.Remove(indexOfSpace);

        if (!propertyMapping.ContainsKey(propertyName))
          return false;
      }

      return true;
    }
  }
}