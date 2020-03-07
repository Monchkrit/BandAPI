using System;
using System.Collections.Generic;
using System.Dynamic;

namespace BandAPI.Helpers
{
  public static class IEnumerableExtension
  {
    public static IEnumerable<ExpandoObject> ShapeData<TSource>
                                              (this IEnumerable<TSource> source, 
                                              string fields)
    {
      if (source is null)
        throw new ArgumentNullException(nameof(source));

      var objectList = new List<ExpandoObject>();

      return null;
    }
  }
}