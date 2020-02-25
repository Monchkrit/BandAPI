using System;
using System.Collections.Generic;
using BandAPI.Models;
using BandAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Services.IBandAlbumRepository;
using AutoMapper;

namespace BandAPI.Controllers
{
  [ApiController]
  [Route("api/bands")]
  public class BandsController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IBandAlbumRepository _bandAlbumRepository;

    public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
    {
      _bandAlbumRepository = bandAlbumRepository ??
        throw new ArgumentNullException(nameof(bandAlbumRepository));

      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }

    [HttpGet]
    [HttpHead]
    public ActionResult<IEnumerable<BandDto>> getBands([FromQuery]string mainGenre)
    {
      // throw new Exception("Exception testing of bands");
      var bandsFromRepo = _bandAlbumRepository.GetBands(mainGenre);
      // var bandsDto = new List<BandDto>();

      // foreach (var band in bandsFromRepo)
      // {
        // bandsDto.Add(new BandDto()
        // {
        //   ID = band.ID,
        //   Name = band.Name,
        //   MainGenre = band.MainGenre,
        //   FoundedYearsAgo = $"{band.Founded.ToString("yyyy")} ({band.Founded.GetYearsAgo()} years ago)"
        // });        
      // }
      return new OkObjectResult(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo));
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