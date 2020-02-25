using AutoMapper;

namespace BandAPI.Profiles
{
  public class AlbumsProfile : Profile
  {
    public AlbumsProfile()
    {
      CreateMap<Entities.Album,Models.AlbumDto>().ReverseMap();
    }
  }
}