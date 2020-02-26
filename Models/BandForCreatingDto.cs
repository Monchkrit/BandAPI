using System;

namespace BandAPI.Models
{
  public class BandForCreatingDto
  {
    public Guid ID { get; set; }
    public string Name { get; set; }
    public DateTime Founded { get; set; }
    public string MainGenre { get; set; }
  }
}