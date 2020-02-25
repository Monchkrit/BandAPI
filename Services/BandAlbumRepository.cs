using System;
using System.Collections.Generic;
using System.Linq;
using BandAPI.DbContexts;
using BandAPI.Entities;

namespace Services.IBandAlbumRepository
{
  public class BandAlbumRepository : IBandAlbumRepository
  {
    private readonly BandAlbumContext _context;
    public BandAlbumRepository(BandAlbumContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void AddAlbum(Guid bandID, Album album)
    {
      if (bandID == Guid.Empty)
        throw new ArgumentNullException(nameof(bandID));

      if (album == null)
        throw new ArgumentNullException(nameof(album));

      album.BandID = bandID;
      _context.Albums.Add(album);
    }

    public void AddBand(Band band)
    {
      if (band == null)
        throw new ArgumentNullException(nameof(band));

      _context.Bands.Add(band);      
    }

    public bool AlbumExists(Guid albumID)
    {
      if (albumID == Guid.Empty)
        throw new ArgumentNullException(nameof(albumID));

      return _context.Albums.Any(a => a.ID == albumID);
    }

    public bool BandExists(Guid bandID)
    {
      if (bandID == Guid.Empty)
        throw new ArgumentNullException(nameof(bandID));

      return _context.Bands.Any(b => b.ID == bandID);
    }

    public void DeleteAlbum(Album album)
    {
      if (album == null)
        throw new ArgumentNullException(nameof(album));

      _context.Albums.Remove(album);
    }

    public void DeleteBand(Band band)
    {
      if (band == null)
        throw new ArgumentNullException(nameof(band));

      _context.Bands.Remove(band);
    }

    public Album GetAlbum(Guid albumID)
    {
      if (albumID == Guid.Empty)
        throw new ArgumentNullException(nameof(albumID));
      
      return _context.Albums.FirstOrDefault(a => a.ID == albumID);
    }

    public Album GetAlbum(Guid bandID, Guid albumID)
    {
      if (bandID == Guid.Empty)
        throw new ArgumentNullException(nameof(bandID));

      if (albumID == Guid.Empty)
        throw new ArgumentNullException(nameof(albumID));

      return _context.Albums.Where(a => a.BandID == bandID && a.ID == albumID).First();
    }

    public IEnumerable<Album> GetAlbums(Guid bandID)
    {
      if (bandID == Guid.Empty)
      throw new ArgumentNullException();

      return _context.Albums.Where(b => b.BandID == bandID)
                            .OrderBy(b => b.ReleaseDate);
    }

    public Band GetBand(Guid bandID)
    {
      if (bandID == Guid.Empty)
        throw new ArgumentNullException(nameof(bandID));

      return _context.Bands.FirstOrDefault(b => b.ID == bandID);
    }

    public IEnumerable<Band> GetBands()
    {
      return _context.Bands.ToList();
    }

    public IEnumerable<Band> GetBands(IEnumerable<Guid> bandIDs)
    {
      if (bandIDs == null)
        throw new ArgumentNullException(nameof(bandIDs));

      return _context.Bands.Where(b => bandIDs.Contains(b.ID))
                            .OrderBy(b => b.Name).ToList();
    }

    public IEnumerable<Band> GetBands(string mainGenre)
    {
      if (string.IsNullOrWhiteSpace(mainGenre))
        return GetBands();

      mainGenre = mainGenre.Trim();
      return _context.Bands.Where(b => b.MainGenre == mainGenre);
    }

    public bool Save()
    {
      return (_context.SaveChanges() >= 0);
    }

    public void UpdateAlbum(Album album)
    {
      // Implemented in the controller
    }

    public void UpdateBand(Band band)
    {
      // Implemented in the controller
    }
  }
}