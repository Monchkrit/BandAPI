using System;
using System.Collections.Generic;
using AutoMapper;
using BandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Services.IBandAlbumRepository;

namespace BandAPI.Controllers
{
  [ApiController]
  [Route("api/bandcollections")]
  public class BandCollectionsController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IBandAlbumRepository _bandAlbumRepository;

    public BandCollectionsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
    {
      _bandAlbumRepository = bandAlbumRepository ??
        throw new ArgumentNullException(nameof(bandAlbumRepository));

      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public ActionResult<IEnumerable<BandDto>> CreateBandCollection([FromBody] IEnumerable<BandForCreatingDto> bandCollection)
    {
      var bandEntities = _mapper.Map<IEnumerable<Entities.Band>>(bandCollection);

      foreach (var band in bandEntities)
      {
        _bandAlbumRepository.AddBand(band);
      }

      _bandAlbumRepository.Save();

      return Ok();
    }
  }
}