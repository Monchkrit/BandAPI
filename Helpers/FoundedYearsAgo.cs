using System;

namespace BandAPI.Helpers
{
  public static class FoundedYearsAgo
  {
    public static int GetYearsAgo(this DateTime dateTime)
    {
      var currentDate = DateTime.Now;
        int yearsAgo = currentDate.Year - dateTime.Year;

        return yearsAgo;
    }
  }
}