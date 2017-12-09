using Newtonsoft.Json;

namespace MatoIndustry.Model
{


    public class Recipe
    {

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("ingredients")]
        public string Ingredients { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("sumary")]
        public string Sumary { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        public string[] IngredientsInfo { get; set; }

        public Method[] MethodInfo { get; set; }
    }

    public class RecipeDetailInfo
    {

        [JsonProperty("ctgIds")]
        public string[] CtgIds { get; set; }

        [JsonProperty("ctgTitles")]
        public string CtgTitles { get; set; }

        [JsonProperty("menuId")]
        public string MenuId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("recipe")]
        public Recipe Recipe { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }

    public class Method
    {

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("step")]
        public string Step { get; set; }
    }


}
