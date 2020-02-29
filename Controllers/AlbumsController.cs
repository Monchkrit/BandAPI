using System;
using System.Collections.Generic;
using AutoMapper;
using BandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Services.IBandAlbumRepository;

namespace BandAPI.Controllers
{
  [ApiController]
  [Route("api/bands/{bandID}/albums")]
  public class AlbumsController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IBandAlbumRepository _bandAlbumRepository;

    public AlbumsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
    {
      _bandAlbumRepository = bandAlbumRepository ??
        throw new ArgumentNullException(nameof(bandAlbumRepository));

      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }

    [HttpGet]    
    public ActionResult<IEnumerable<AlbumDto>> GetAlbumsForBand(Guid bandID)
    {
      if (!_bandAlbumRepository.BandExists(bandID))
        return NotFound();

      var albumsFromRepo = _bandAlbumRepository.GetAlbums(bandID);

      return new OkObjectResult(_mapper.Map<IEnumerable<AlbumDto>>(albumsFromRepo));
    }

    [HttpGet("{albumID}", Name = "GetAlbumsForBand")]
    public ActionResult<AlbumDto> GetAlbumForBand(Guid bandID, Guid albumID)
    {
      if (!_bandAlbumRepository.BandExists(bandID))
        return NotFound();

      var albumFromRepo = _bandAlbumRepository.GetAlbum(bandID, albumID);
      if (albumFromRepo == null)
        return NotFound();
      
      return new OkObjectResult(_mapper.Map<AlbumDto>(albumFromRepo));
    }

    [HttpPost]
    public ActionResult<AlbumDto> CreateAlbumForBand(Guid bandID, [FromBody]AlbumForCreatingDto album)
    {
      if (!_bandAlbumRepository.BandExists(bandID))
        return NotFound();

      var albumEntity = _mapper.Map<Entities.Album>(album);

      _bandAlbumRepository.AddAlbum(bandID, albumEntity);
      _bandAlbumRepository.Save();

      var returnAlbum = _mapper.Map<AlbumDto>(albumEntity);

      return CreatedAtRoute("GetAlbumsForBand", new { bandID = bandID, albumID = returnAlbum.ID}, returnAlbum);
    }

    [HttpPut]
    [Route("{albumID}")]
    public ActionResult UpdateAlbumForBand(Guid bandID, Guid albumID, [FromBody]AlbumForUpdatingDto album)
    {
      if (!_bandAlbumRepository.BandExists(bandID))
        return NotFound();

      var albumFromRepo = _bandAlbumRepository.GetAlbum(bandID, albumID);
      if (albumFromRepo is null)
        return NotFound();

      _mapper.Map(album, albumFromRepo);

      _bandAlbumRepository.UpdateAlbum(albumFromRepo);
      _bandAlbumRepository.Save();

      return NoContent();
    }
  }
}