namespace BL.Interfaces;

public interface IScraperService
{
    public Task Discover(string filter);
}