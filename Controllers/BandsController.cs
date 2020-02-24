using System;
using Microsoft.AspNetCore.Mvc;
using Services.IBandAlbumRepository;

namespace BandAPI.Controllers
{
  [ApiController]
  [Route("api/bands")]
  public class BandsController : ControllerBase
  {
    private readonly IBandAlbumRepository _bandAlbumRepository;

    public BandsController(IBandAlbumRepository bandAlbumRepository)
    {
      _bandAlbumRepository = bandAlbumRepository ??
        throw new ArgumentNullException(nameof(bandAlbumRepository));
    }

    [HttpGet]
    public IActionResult getBands()
    {
      var bandsFromRepo = _bandAlbumRepository.GetBands();

      return new JsonResult(bandsFromRepo);
    }
    [HttpGet("{bandID}")]
    public IActionResult getBand(Guid bandID)
    {
      var bandFromRepo = _bandAlbumRepository.GetBand(bandID);
      if (bandFromRepo == null)
        return NotFound();

      return new OkObjectResult(bandFromRepo);
    }
  }
}