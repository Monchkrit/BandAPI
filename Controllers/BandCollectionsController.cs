using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BandAPI.Helpers;
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

    [HttpGet("({ids})")]
    public IActionResult GetBandCollection([FromRoute]
      [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
      if (ids is null)
        return BadRequest();

      var bandEntities = _bandAlbumRepository.GetBands(ids);

      if (ids.Count() != bandEntities.Count())
        return NotFound();

      var returnBands = _mapper.Map<IEnumerable<BandDto>>(bandEntities);

      return Ok(returnBands);
    }

    [HttpPost]
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