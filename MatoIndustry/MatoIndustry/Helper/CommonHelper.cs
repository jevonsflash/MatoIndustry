using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using MatoIndustry.Common;
using MatoIndustry.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MatoIndustry.Helper
{
    public class CommonHelper
    {
        public static Color GetColorFromHEX(object value)
        {
            uint color = System.Convert.ToUInt32(value.ToString(), fromBase: 16);
            byte A = (byte)((color & 0xFF000000) >> 24);
            byte R = (byte)((color & 0x00FF0000) >> 16);
            byte G = (byte)((color & 0x0000FF00) >> 8);
            byte B = (byte)((color & 0x000000FF) >> 0);
            Color col = Color.FromRgba(R, G, B, A);


            return col;
        }


        public static List<RecipeDetailInfo> ReSeletionValue(RecipeDetailInfo[] source)
        {
            var filterSource = source.Where(c => !string.IsNullOrEmpty(c.Recipe.Method));

            foreach (var recipeDetailInfo in filterSource)
            {
                if (string.IsNullOrEmpty(recipeDetailInfo.Recipe.Img))
                {
                    var methods =
                        JsonConvert.DeserializeObject<Method[]>(recipeDetailInfo.Recipe.Method);
                    var firstMethodHasImg = methods.LastOrDefault(c => !string.IsNullOrEmpty(c.Img));
                    if (firstMethodHasImg != null)
                    {
                        recipeDetailInfo.Recipe.Img = firstMethodHasImg.Img;
                    }
                }
            }

            return filterSource.ToList();
        }

        public static void GoNavigate(string pageName, NavigationType type = NavigationType.NavigateTo, object[] args = null)
        {

            Messenger.Default.Send<WindowArg>(new WindowArg(pageName, args, type), TokenHelper.WindowToken);

        }
        public static int GetRamdonNum()
        {
            var r = new Random();
            return r.Next(100000, 999999);
        }

        public static int[] GetRandomArry(int minval, int maxval)
        {

            int[] arr = new int[maxval - minval + 1];
            int i;
            //初始化数组
            for (i = 0; i <= maxval - minval; i++)
            {
                arr[i] = i + minval;
            }
            //随机数
            Random r = new Random();
            for (int j = maxval - minval; j >= 1; j--)
            {
                int address = r.Next(0, j);
                int tmp = arr[address];
                arr[address] = arr[j];
                arr[j] = tmp;
            }
            //输出
            foreach (int k in arr)
            {
                Debug.WriteLine(k + " ");
            }
            return arr;
        }

        public static string[] ImagesHandler(string src)
        {
            string[] temp;

            if (string.IsNullOrEmpty(src))
            {
                return new string[] { "x" };
            }

            else
            {
                temp = src.Split(',');
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = string.Format("http://tnfs.tngou.net/image{0}", temp[i]);
                }
            }
            return temp;
        }

        //public static string FormatHtmlStr(string message)
        //{
        //    var detailContent = "";
        //    if (string.IsNullOrEmpty(message))
        //    {
        //        return detailContent;
        //    }
        //    var doc = new HtmlDocument();
        //    doc.LoadHtml(message);
        //    var first = doc.DocumentNode.ChildNodes.FirstOrDefault();
        //    detailContent = SetNextNode(first, "");
        //    return detailContent;

        //}

        //public static List<MessageDetailContent> FormatHtmlStrWithTitle(string message)
        //{
        //    var detailContent = new List<MessageDetailContent>();
        //    if (string.IsNullOrEmpty(message))
        //    {
        //        return detailContent;
        //    }
        //    var doc = new HtmlDocument();
        //    doc.LoadHtml(message);
        //    var nodes = doc.DocumentNode.ChildNodes;
        //    foreach (var node in nodes)
        //    {
        //        if (node.Name == "h2")
        //        {
        //            detailContent.Add(new MessageDetailContent()
        //            {
        //                Title = node.InnerText,
        //                Contents = SetNextNode(node.NextSibling, "")
        //            });
        //        }

        //    }
        //    return detailContent;
        //}

        //public static string SetNextNode(HtmlNode n, string preStr)
        //{
        //    var result = "";
        //    if (n != null)
        //    {
        //        if (n.Name != "h2")
        //        {
        //            var currentStr = SetNextNode(n.NextSibling, n.InnerText);
        //            if (string.IsNullOrEmpty(currentStr.Trim()) || currentStr.Trim() == "\n")
        //            {
        //                if (!preStr.EndsWith("\n"))
        //                {
        //                    currentStr = "\n";
        //                }
        //            }

        //            result = preStr + currentStr;
        //        }
        //        else
        //        {
        //            result = preStr;
        //        }

        //    }
        //    else
        //    {
        //        result = preStr;
        //    }
        //    return result;
        //}



    }


}