using AutoMapper;
using BandAPI.Models;

namespace BandAPI.Profiles
{
  public class AlbumsProfile : Profile
  {
    public AlbumsProfile()
    {
      CreateMap<Entities.Album,Models.AlbumDto>().ReverseMap();
      CreateMap<AlbumForCreatingDto, Entities.Album>();
      CreateMap<AlbumForUpdatingDto, Entities.Album>();
    }
  }
}