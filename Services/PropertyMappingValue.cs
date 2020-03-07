using System;
using System.Collections.Generic;

namespace BandAPI.Services
{
  public class PropertyMappingValue
  {
    public IEnumerable<string> DestinationProperties { get; set; }
    public bool Reverse { get; set; }

    public PropertyMappingValue(IEnumerable<string> destinationProperties, bool reverse = false)
    {
        DestinationProperties = destinationProperties ??
          throw new ArgumentNullException(nameof(destinationProperties));
        Reverse = reverse;
    }
  }
}