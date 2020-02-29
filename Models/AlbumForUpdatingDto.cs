using System;

namespace BandAPI.Models
{
  public class AlbumForUpdatingDto
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
  }
}