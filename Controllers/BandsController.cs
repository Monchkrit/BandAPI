using System;
using System.Collections.Generic;
using BandAPI.Models;
using BandAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Services.IBandAlbumRepository;
using AutoMapper;
using System.Text.Json;
using BandAPI.Services;

namespace BandAPI.Controllers
{
  [ApiController]
  [Route("api/bands")]
  public class BandsController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IBandAlbumRepository _bandAlbumRepository;
    private readonly IPropertyMappingService _propertyMappingService;

    public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper,
                          IPropertyMappingService propertyMappingService)
    {
      _bandAlbumRepository = bandAlbumRepository ??
        throw new ArgumentNullException(nameof(bandAlbumRepository));

      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

      _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(PropertyMappingService));
    }

    [HttpGet(Name = "GetBands")]
    [HttpHead]
    public IActionResult getBands([FromQuery]BandResourceParameters bandResourceParameters)
    { 
      if (!_propertyMappingService.ValidMappingExists<BandDto, Entities.Band>
        (bandResourceParameters.OrderBy))
        return BadRequest();

      var bandsFromRepo = _bandAlbumRepository.GetBands(bandResourceParameters);

      var previousPageLink = bandsFromRepo.HasPrevious ? CreateBandsUri(bandResourceParameters, UriType.PreviousPage) : null;
      var nextPageLink = bandsFromRepo.HasNext ? CreateBandsUri(bandResourceParameters, UriType.NextPage) : null;

      var metaData = new {
        totalCount = bandsFromRepo.TotalCount,
        pageSize = bandsFromRepo.PageSize,
        currentPage = bandsFromRepo.CurrentPage,
        totalPages = bandsFromRepo.TotalPages,
        previousPageLink,
        nextPageLink
      };

      Response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData));

      return new OkObjectResult(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo).ShapeData(bandResourceParameters.Fields));
    }

    [HttpGet("{bandID}", Name = "getBand")]
    public IActionResult getBand(Guid bandID)
    {
      var bandFromRepo = _bandAlbumRepository.GetBand(bandID);
      if (bandFromRepo == null)
        return NotFound();

      return new OkObjectResult(_mapper.Map<BandDto>(bandFromRepo));
    }

    [HttpPost]
    public ActionResult<BandDto> CreateBand([FromBody]BandForCreatingDto band)
    {
      var bandEntity = _mapper.Map<Entities.Band>(band);

      bandEntity.ID = Guid.NewGuid();

      _bandAlbumRepository.AddBand(bandEntity);
      _bandAlbumRepository.Save();

      var bandToReturn = _mapper.Map<BandDto>(bandEntity);

      return CreatedAtRoute("getBand", new { bandID = bandToReturn.ID}, bandToReturn);
    }

    [HttpPost]
    [Route("{bandID}")]
    public ActionResult<AlbumDto> CreateAlbumForBand(Guid bandID, [FromBody]AlbumForCreatingDto album)
    {
      if (!_bandAlbumRepository.BandExists(bandID))
        return NotFound();

      var albumEntity = _mapper.Map<Entities.Album>(album);

      _bandAlbumRepository.AddAlbum(bandID, albumEntity);
      _bandAlbumRepository.Save();

      var returnAlbum = _mapper.Map<AlbumDto>(albumEntity);

      return CreatedAtRoute("GetAlbumForBand", new { bandID = bandID, albumID = returnAlbum.ID}, returnAlbum);
    }

    [HttpOptions]
    public IActionResult GetBandOptions()
    {
      Response.Headers.Add("Allow", "GET, POST, DELETE, HEAD, OPTIONS");
      return Ok();
    }

    [HttpDelete("{bandID}")]
    public ActionResult DeleteBand(Guid bandID)
    {
      var bandFromRepo = _bandAlbumRepository.GetBand(bandID);
      if (bandFromRepo is null)
        return NotFound();

      _bandAlbumRepository.DeleteBand(bandFromRepo);
      _bandAlbumRepository.Save();

      return NoContent();
    }
  
    private string CreateBandsUri(BandResourceParameters bandResourceParameters, UriType uriType)
    {
      switch (uriType)
      {
        case UriType.PreviousPage:
          return Url.Link("GetBands", new 
          { 
            fields = bandResourceParameters.Fields,
            orderBy = bandResourceParameters.OrderBy,
            pageNumber = bandResourceParameters.PageNumber - 1,
            pageSize = bandResourceParameters.PageSize,
            mainGenre = bandResourceParameters.MainGenre,
            searchQuery = bandResourceParameters.SearchQuery
          });

        case UriType.NextPage:
          return Url.Link("GetBands", new 
          { 
            fields = bandResourceParameters.Fields,
            orderBy = bandResourceParameters.OrderBy,
            pageNumber = bandResourceParameters.PageNumber + 1,
            pageSize = bandResourceParameters.PageSize,
            mainGenre = bandResourceParameters.MainGenre,
            searchQuery = bandResourceParameters.SearchQuery
          });

        default:
          return Url.Link("GetBands", new 
          { 
            fields = bandResourceParameters.Fields,
            orderBy = bandResourceParameters.OrderBy,
            pageNumber = bandResourceParameters.PageNumber,
            pageSize = bandResourceParameters.PageSize,
            mainGenre = bandResourceParameters.MainGenre,
            searchQuery = bandResourceParameters.SearchQuery
          });
      }
    }
  }
}