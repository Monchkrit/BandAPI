namespace BandAPI.Helpers
{
  public class BandResourceParameters
  {
    public string MainGenre { get; set; }
    public string SearchQuery { get; set; }

    const int maxPageSize = 5;
    public int PageNumber { get; set; } = 1;

    private int _pagesize = 2;

    public int PageSize { 
      get => _pagesize; 
      set => _pagesize = (value > maxPageSize) ? maxPageSize : value;
    }
  }
}