namespace BandAPI.Services
{
  public interface IPropertyValidationService
  {
    bool HasValidParameters<T>(string fields);
  }
}