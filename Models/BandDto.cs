using System;

namespace BandAPI.Models
{
  public class BandDto
  {
    public Guid ID { get; set; }    
    public string Name { get; set; }
    public string FoundedYearsAgo { get; set; }
    public string MainGenre { get; set; }
  }
}