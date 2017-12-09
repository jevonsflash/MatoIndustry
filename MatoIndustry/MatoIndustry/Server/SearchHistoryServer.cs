using System.Collections.Generic;
using System.Threading.Tasks;
using MatoIndustry.Helper;
using MatoIndustry.Model;
using Newtonsoft.Json;

namespace MatoIndustry.Server
{
    public class SearchHistoryServer
    {

        private static SearchHistoryServer instance;

        private SearchHistoryServer()
        {
        }

        public static SearchHistoryServer Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchHistoryServer();
                }
                return instance;
            }
        }
        public async Task<List<SearchWordsInfo>> GetHistoryList()
        {
            List<SearchWordsInfo> respose;
            var filePath = "HistoryList.json";
            string text = await FileHelper.ReadAllTextAsync(filePath);
            if (!string.IsNullOrEmpty(text))
            {
                respose = JsonConvert.DeserializeObject<List<SearchWordsInfo>>(text);

            }
            else
            {
                respose = new List<SearchWordsInfo>();
            }
            return respose;
        }
        public async Task SaveHistoryList(List<SearchWordsInfo> historyList)
        {

            string filePath = "HistoryList.json";

            string jsonContent = JsonConvert.SerializeObject(historyList);
            await FileHelper.WriteTextAllAsync(filePath, jsonContent);

        }
        public async Task<List<SearchWordsInfo>> GetHotSearchList()
        {
            return new List<SearchWordsInfo>()
                {
                    new SearchWordsInfo("茄子"),
                    new SearchWordsInfo("咖喱饭"),
                    new SearchWordsInfo("竹笋"),
                    new SearchWordsInfo("苦瓜"),
                    new SearchWordsInfo("蛋炒饭"),
                    new SearchWordsInfo("牛肉"),
                    new SearchWordsInfo("清蒸鱼"),
                    new SearchWordsInfo("红烧肉"),
                };
        }

    }
}
