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

    [HttpGet("{albumID}")]
    public ActionResult<AlbumDto> GetAlbumForBand(Guid bandID, Guid albumID)
    {
      if (!_bandAlbumRepository.BandExists(bandID))
        return NotFound();

      var albumFromRepo = _bandAlbumRepository.GetAlbum(bandID, albumID);
      if (albumFromRepo == null)
        return NotFound();
      
      return new OkObjectResult(_mapper.Map<AlbumDto>(albumFromRepo));
    }
  }
}