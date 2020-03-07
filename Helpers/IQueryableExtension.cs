using System.Collections.Generic;
using System.Linq;
using BandAPI.Services;
using System.Linq.Dynamic.Core;
using System;

namespace BandAPI.Helpers
{
  public static class IQueryableExtension
  {
    public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,
      Dictionary<string, PropertyMappingValue> mappingDictionary)
      {
        if (source == null)
          throw new ArgumentNullException(nameof(source));

        if (source == null)
          throw new ArgumentNullException(nameof(mappingDictionary));

        if (string.IsNullOrWhiteSpace(orderBy))
          return source;

        var orderBySplit = orderBy.Split(",");
        var orderByString = "";

        foreach (var orderByClause in orderBySplit)
        {
          var trimmedOrderBy = orderByClause.Trim();
          var orderDesc = trimmedOrderBy.EndsWith(" desc");
          var indexOfSpace = trimmedOrderBy.IndexOf(" ");
          var propertyName = indexOfSpace == -1 ? trimmedOrderBy : 
            trimmedOrderBy.Remove(indexOfSpace);

          if (!mappingDictionary.ContainsKey(propertyName))
            throw new ArgumentException("Mapping doesn't exist for " + propertyName);

          var propertyMappingValue = mappingDictionary[propertyName];

          if (propertyMappingValue == null)
            throw new ArgumentNullException(nameof(propertyMappingValue));

          foreach (var destination in propertyMappingValue.DestinationProperties.Reverse())
          {
            if (propertyMappingValue.Reverse)
              orderDesc = !orderDesc;
            
            orderByString = orderByString + (!string.IsNullOrWhiteSpace(orderByString) ? "," : "") + destination +
            (orderDesc ? " descending": " ascending");
          }
        }
        return source.OrderBy(orderByString);       
      }
  }
}