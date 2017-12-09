using System.Threading.Tasks;
using MatoIndustry.Model;

namespace MatoIndustry.Interface
{
    public interface IHttpHelper
    {
        string Request<T>(T config) where T : RequestData, new();
        Task<string> GetUrlResposeAsnyc(string url);
    }

}
