using System;

namespace BandAPI.Models
{
  public class AlbumForCreatingDto
  {
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Guid BandID { get; set; }
  }
}