using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MatoIndustry.Interface;
using MatoIndustry.Model;
using MatoIndustry.Droid.Helper;
using Xamarin.Forms;


[assembly: Dependency(typeof(HttpHelper))]

namespace MatoIndustry.Droid.Helper
{

    public class HttpHelper : IHttpHelper
    {
        public string Request<T>(T config) where T : RequestData, new()
        {
            // 请求URL
            var requestURL = config.Url;
            // 将数据包对象转换成QueryString形式的字符串
            string @params = ParseQueryString(config.FormData);
            var isPost = config.Method.Equals("post", StringComparison.CurrentCultureIgnoreCase);

            if (!isPost)
            {
                // get方式 拼接请求url
                var sep = requestURL.Contains('?') ? "&" : "?";
                requestURL += sep + @params;
            }

            var req = (HttpWebRequest)WebRequest.Create(requestURL);
            req.Method = config.Method;
            //req.Referer = "http://music.163.com/";

            if (isPost)
            {
                // 写入post请求包
                var formData = Encoding.UTF8.GetBytes(@params);
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = formData.LongLength;
                req.GetRequestStream().Write(formData, 0, formData.Length);
            }

            // 发送http请求 并读取响应内容返回
            return new StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd();
        }
        /// <summary>
        /// 将对象转换成QueryString形式的字符串
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns></returns>
        private string ParseQueryString(object obj)
        {
            return string.Join("&", obj.GetType().GetProperties().Select(x => string.Format("{0}={1}", x.Name, x.GetValue(obj))));
        }

        /// <summary>
        /// 获取返回字符串获取网络的返回的字符串数据
        /// </summary>
        /// <param name="url"></param>
        public async Task<string> GetUrlResposeAsnyc(string url)
        {
            Uri uri = new Uri(url);
            HttpClient httpClient = new HttpClient();
            string result = await httpClient.GetStringAsync(uri).ConfigureAwait(false);
            return result;
        }

    }
}