using Newtonsoft.Json;

namespace MatoIndustry.Model
{


    public class RecipeListResult
    {

        [JsonProperty("curPage")]
        public int CurPage { get; set; }

        [JsonProperty("list")]
        public RecipeDetailInfo[] List { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class RecipeListEntity
    {

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("result")]
        public RecipeListResult Result { get; set; }

        [JsonProperty("retCode")]
        public string RetCode { get; set; }
    }

}
