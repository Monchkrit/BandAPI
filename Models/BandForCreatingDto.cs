using System;
using System.Collections.Generic;

namespace BandAPI.Models
{
  public class BandForCreatingDto
  {
    public Guid ID { get; set; }
    public string Name { get; set; }
    public DateTime Founded { get; set; }
    public string MainGenre { get; set; }
    public ICollection<AlbumForCreatingDto> Albums { get; set; } = new List<AlbumForCreatingDto>();
  }
}