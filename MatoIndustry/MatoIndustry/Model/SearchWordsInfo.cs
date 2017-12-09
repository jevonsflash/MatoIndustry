using System;
using Newtonsoft.Json;

namespace MatoIndustry.Model
{
    public class SearchWordsInfo
    {
        public SearchWordsInfo(string words)
        {
            this.Words = words;
            this.SearchDate = DateTime.Now;
        }
        [JsonProperty]
        public string Words { get; set; }

        [JsonProperty]
        public DateTime SearchDate { get; set; }
    }
}
