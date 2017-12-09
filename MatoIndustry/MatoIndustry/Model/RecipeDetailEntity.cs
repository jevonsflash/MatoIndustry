using Newtonsoft.Json;

namespace MatoIndustry.Model
{

    public class RecipeDetailEntity
    {

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("result")]
        public RecipeDetailInfo Result { get; set; }

        [JsonProperty("retCode")]
        public string RetCode { get; set; }
    }
}
