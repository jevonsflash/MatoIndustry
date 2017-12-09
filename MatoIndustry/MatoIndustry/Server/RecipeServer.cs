using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MatoIndustry.Helper;
using MatoIndustry.Model;
using Newtonsoft.Json;

namespace MatoIndustry.Server
{
    public class RecipeServer
    {
        private static string key = "2084cc9421ea9";
        private const int pageSize = 10;
        private static readonly HttpHelper HttpHelper = new HttpHelper();
        /// <summary>
        /// 获取菜谱分类
        /// </summary>
        /// <returns></returns>
        public async Task<RecipeCategoryEntity> GetRecipeCategoryEntity()
        {
            var result = new RecipeCategoryEntity();
            string url = StaticURLHelper.RecipeCategory;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("key", key);
            var jsonstr = await GetJSON(url, dic);
            result = JsonConvert.DeserializeObject<RecipeCategoryEntity>(jsonstr);
            return result;
        }

        /// <summary>
        /// 获取菜谱列表
        /// </summary>
        /// <returns></returns>
        public async Task<RecipeListEntity> GetRecipeListEntity( int page,int size = pageSize)
        {
            var result = new RecipeListEntity();
            string url = StaticURLHelper.RecipeList;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("key", key);
            dic.Add("page", page.ToString());
            dic.Add("size", size.ToString());
            var jsonstr = await GetJSON(url, dic);
            result = JsonConvert.DeserializeObject<RecipeListEntity>(jsonstr);
            GetValue(result);
            return result;
        }


        /// <summary>
        /// 获取菜谱列表（根据类别Id）
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<RecipeListEntity> GetRecipeListEntityByCid(string cid, int page, int size = pageSize)
        {
            var result = new RecipeListEntity();
            string url = StaticURLHelper.RecipeList;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("key", key);
            dic.Add("cid", cid);
            dic.Add("page", page.ToString());
            dic.Add("size", size.ToString());
            var jsonstr = await GetJSON(url, dic);
            result = JsonConvert.DeserializeObject<RecipeListEntity>(jsonstr);
            GetValue(result);

            return result;
        }
        /// <summary>
        /// 获取菜谱列表（根据菜谱名称）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        public async Task<RecipeListEntity> GetRecipeListEntityByName(string name, int page, int size = pageSize)
        {
            var result = new RecipeListEntity();
            string url = StaticURLHelper.RecipeList;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("key", key);
            dic.Add("name", name);
            dic.Add("page", page.ToString());
            dic.Add("size", size.ToString());
            var jsonstr = await GetJSON(url, dic);

            result = JsonConvert.DeserializeObject<RecipeListEntity>(jsonstr);
            GetValue(result);

            return result;
        }
        /// <summary>
        /// 获取菜谱详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RecipeDetailEntity> GetRecipeDetailEntity(string id)
        {

            var result = new RecipeDetailEntity();
            string url = StaticURLHelper.RecipeDetail;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("key", key);
            dic.Add("id", id);
            var jsonstr = await GetJSON(url, dic);
            result = JsonConvert.DeserializeObject<RecipeDetailEntity>(jsonstr);
            var recipeDetailInfo = result.Result;
            if (!string.IsNullOrEmpty(recipeDetailInfo.Recipe.Ingredients))
            {
                recipeDetailInfo.Recipe.IngredientsInfo =
                    JsonConvert.DeserializeObject<string[]>(recipeDetailInfo.Recipe.Ingredients);

            }
            if (!string.IsNullOrEmpty(recipeDetailInfo.Recipe.Method))
            {
                recipeDetailInfo.Recipe.MethodInfo =
                    JsonConvert.DeserializeObject<Method[]>(recipeDetailInfo.Recipe.Method);

            }
            return result;

        }

        private bool GetValue(RecipeListEntity result)
        {
            var isSuc = true;
            if (result != null)
            {
                if (result.RetCode == "200" && result.Result != null)
                {


                    foreach (var recipeDetailInfo in result.Result.List)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(recipeDetailInfo.Recipe.Ingredients))
                            {
                                recipeDetailInfo.Recipe.IngredientsInfo =
                                    JsonConvert.DeserializeObject<string[]>(recipeDetailInfo.Recipe.Ingredients);

                            }
                            if (!string.IsNullOrEmpty(recipeDetailInfo.Recipe.Method))
                            {
                                recipeDetailInfo.Recipe.MethodInfo =
                                    JsonConvert.DeserializeObject<Method[]>(recipeDetailInfo.Recipe.Method);

                            }

                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            continue;
                        }
                    }
                }
                else
                {
                    isSuc = false;
                }
            }
            else
            {
                isSuc = false;
            }
            return isSuc;

        }


        private async Task<string> GetJSON(string url, Dictionary<string, string> parameters)
        {
            var resposeString = string.Empty;
            string postString = url;
            if (parameters != null && parameters.Count > 0)
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        i++;
                    }
                }
                postString = postString + "?" + buffer;

            }
            try
            {
                resposeString = await HttpHelper.GetUrlResposeAsnyc(postString).ConfigureAwait(false);

            }
            catch (Exception e)
            {

            }

            return resposeString;
        }
    }
}