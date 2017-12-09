using System.Threading.Tasks;
using MatoIndustry.Interface;
using MatoIndustry.Model;
using Xamarin.Forms;

namespace MatoIndustry.Helper
{
    public class HttpHelper
    {
        private IHttpHelper httpHelper => DependencyService.Get<IHttpHelper>();

        public string Request<T>(T config) where T : RequestData, new()
        {
            var result = httpHelper.Request(config);
            return result;
        }

        public async Task<string> GetUrlResposeAsnyc(string url)
        {
            var result = await httpHelper.GetUrlResposeAsnyc(url);
            return result;


        }
    }
}
