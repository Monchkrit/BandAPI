using System;
using System.Collections.Generic;
using BandAPI.Entities;

namespace Services.IBandAlbumRepository
{
  public interface IBandAlbumRepository
  {
    IEnumerable<Album> GetAlbums(Guid bandID);
    Band GetBand(Guid bandID);
    void AddAlbum(Guid bandID, Album album);
    void UpdateAlbum(Album album);
    void DeleteAlbum(Album album);

    IEnumerable<Band> GetBands();
    IEnumerable<Band> GetBands(IEnumerable<Guid> bandIDs);
    void AddBand(Band band);
    void UpdateBand(Band band);

    bool BandExists(Guid bandID);
    bool AlbumExists(Guid albumID);
    bool Save();
  }
}