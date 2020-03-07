using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace BandAPI.Helpers
{
  public static class IEnumerableExtension
  {
    public static IEnumerable<ExpandoObject> ShapeData<TSource> (this IEnumerable<TSource> source, string fields)
    {
      if (source is null)
        throw new ArgumentNullException(nameof(source));

      var objectList = new List<ExpandoObject>();
      var propertyInfoList = new List<PropertyInfo>();

      if (string.IsNullOrWhiteSpace(fields))
      {
        var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        propertyInfoList.AddRange(propertyInfos);
      }
      else
      {
        var fieldsAfterSplit = fields.Split(",");
        foreach (var field in fieldsAfterSplit)
        {
          var propertyName = field.Trim();
          var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

          if (propertyInfo is null)
            throw new Exception(propertyName.ToString() + " was not found");

          propertyInfoList.Add(propertyInfo);
        }

        foreach (TSource sourceObject in source)
        {
          var dataShapedObject = new ExpandoObject();

          foreach(var propertyInfo in propertyInfoList)
          {
            var propertyValue = propertyInfo.GetValue(sourceObject);

            ((IDictionary<string, object>) dataShapedObject).Add(propertyInfo.Name, propertyValue);
          }

          objectList.Add(dataShapedObject);
        }        
      }
      return objectList;
    }
  }
}