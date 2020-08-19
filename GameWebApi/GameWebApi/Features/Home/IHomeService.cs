namespace GameWebApi.Features.Home
{
    using System.Threading.Tasks;
    using Models;

    public interface IHomeService
    {
        Task<InitialResponseData> GetInitialDate(InitialRequestData requestData);
    }
}
