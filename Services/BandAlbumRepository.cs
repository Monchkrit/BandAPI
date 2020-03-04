using System;
using System.Collections.Generic;
using System.Linq;
using BandAPI.DbContexts;
using BandAPI.Entities;
using BandAPI.Helpers;

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
      if (album is null)
        throw new ArgumentNullException(nameof(album));

      _context.Albums.Remove(album);
    }

    public void DeleteBand(Band band)
    {
      if (band is null)
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

      return _context.Albums.Where(a => a.BandID == bandID && a.ID == albumID).FirstOrDefault();
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

    public PagedList<Band> GetBands(BandResourceParameters bandResourceParameters)
    {
      if (bandResourceParameters is null)
      {
        throw new ArgumentNullException(nameof(bandResourceParameters));
      }

      var mainGenre = bandResourceParameters.MainGenre;
      var searchQuery = bandResourceParameters.SearchQuery;

      if (string.IsNullOrWhiteSpace(mainGenre) && string.IsNullOrWhiteSpace(searchQuery))
      {      
        var collection = _context.Bands as IQueryable<Band>;
          
        return PagedList<Band>.Create(collection, bandResourceParameters.PageNumber, bandResourceParameters.PageSize);
      }
        
      using (var bands = _context)
      {
        if (!string.IsNullOrWhiteSpace(mainGenre) && !string.IsNullOrWhiteSpace(searchQuery))
        {
          mainGenre = mainGenre.Trim();
          searchQuery = searchQuery.Trim();
          var collection = (from b in bands.Bands
                      where b.MainGenre == mainGenre && b.Name.Contains(searchQuery)
                      select b);

          return PagedList<Band>.Create(collection, bandResourceParameters.PageNumber, bandResourceParameters.PageSize);
        }
        else if (!string.IsNullOrWhiteSpace(mainGenre))
        {
          mainGenre = mainGenre.Trim();
          var collection = (from b in bands.Bands
                      where b.MainGenre == mainGenre
                      select b);

          return PagedList<Band>.Create(collection, bandResourceParameters.PageNumber, bandResourceParameters.PageSize);
        }
        else if (!string.IsNullOrWhiteSpace(searchQuery))
        {
          searchQuery = searchQuery.Trim();
          var collection = (from b in bands.Bands
                      where b.Name.Contains(searchQuery)
                      select b);
                      
          return PagedList<Band>.Create(collection, bandResourceParameters.PageNumber, bandResourceParameters.PageSize);
        }
      }
      
      return null;
    }

    public bool Save()
    {
      return (_context.SaveChanges() >= 0);
    }

    public void UpdateAlbum(Album album)
    {
      // Implemented in the AlbumsController
    }

    public void UpdateBand(Band band)
    {
      // Implemented in the BandsController
    }
  }
}