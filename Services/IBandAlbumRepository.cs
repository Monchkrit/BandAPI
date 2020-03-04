using System;
using System.Collections.Generic;
using BandAPI.Entities;
using BandAPI.Helpers;

namespace Services.IBandAlbumRepository
{
  public interface IBandAlbumRepository
  {
    IEnumerable<Album> GetAlbums(Guid bandID);
    Album GetAlbum(Guid bandID, Guid albumID);
    void AddAlbum(Guid bandID, Album album);
    void UpdateAlbum(Album album);
    void DeleteAlbum(Album album);

    Band GetBand(Guid bandID);
    IEnumerable<Band> GetBands();
    IEnumerable<Band> GetBands(IEnumerable<Guid> bandIDs);
    PagedList<Band> GetBands(BandResourceParameters bandResourceParameters);
    void AddBand(Band band);
    void UpdateBand(Band band);

    bool BandExists(Guid bandID);
    bool AlbumExists(Guid albumID);
    bool Save();
    void DeleteBand(Band band);
  }
}