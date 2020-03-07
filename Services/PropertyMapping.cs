using System;
using System.Collections.Generic;

namespace BandAPI.Services
{
  public class PropertyMapping<Tsource, Tdestination> : IPropertyMappingMarker
  {
    public Dictionary<string, PropertyMappingValue> MappingDictionary { get; set; }

    public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
      MappingDictionary = mappingDictionary ??
        throw new ArgumentNullException(nameof(mappingDictionary));
    }
  }
}