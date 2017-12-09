using Newtonsoft.Json;

namespace MatoIndustry.Model
{
    public class CategoryInfo
    {

        [JsonProperty("ctgId")]
        public string CtgId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }
    }

    public class Child
    {

        [JsonProperty("categoryInfo")]
        public CategoryInfo CategoryInfo { get; set; }

        [JsonProperty("childs")]
        public Child[] Childs { get; set; }
    }
    public class RecipeCategoryEntity
    {

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("result")]
        public Child Result { get; set; }

        [JsonProperty("retCode")]
        public string RetCode { get; set; }
    }

}
