﻿using System;

namespace MatoIndustry.Server
{
    public class TpiServer
    {
        private const string _tips = @"合理饮食可以使人类平均寿命延长20年
如果你是富翁，那么应该在高兴的时候多吃；反之，你若贫穷，那么就在能吃的时候多吃。——（德国哲学家）第欧根尼
如果需要改变一种饮食习惯，那么最好对饮食全面重新调整一下。——（英国哲学家）培根
我为生存，为服务于人而食，有时也为快乐而食，但并不为享受才进食。——（印度政治家）甘地
你应该为生存而食，不应为食而生存。——（古罗马哲学家）西塞罗
一生没有宴饮，就像一条长路没有旅店一样。——（古希腊哲学家）德漠克利特
人的饮食要从五谷杂粮中吸收多方面的营养，也要从多种蔬菜中吸收营养，不能偏食。——（中国教育家）徐特立
口腹之欲，何穷之有。每加节俭，亦是惜福延寿之道。——（中国古代诗人）苏轼
已饥方食，未饱先止；散步逍遥，务令腹空。——（中国古代诗人）苏轼
不要因为你自己没有胃口而去责备你的食物。——（印度作家）泰戈尔
美食珍锺可以充实肌肤，却会闭塞心窍。——（英国戏剧家、诗人）莎士比亚
整天赴宴的人没有一顿饭能吃得香。——（英国词典编辑家）福勒
从来没有一个例子能证明好话能安慰饥饿的胃。——（奥地利作家）茨威格
一生没有宴饮，就像一条长路没有旅店一样。——（古希腊哲学家）德溪克利特
欢乐的气氛能使一盘菜变得像一个宴会。——（英国哲学家）赫伯特
用艰苦的劳作换取旺盛的食欲。——（古罗马诗人）贺拉斯
为了能够保持良好的健康，养料不仅分量要有节制，而且质料也要清淡。——（捷克教育家）夸美纽斯
饮食习惯的改良比其他任何改良，其优点显然要大得多。——（英国诗人）雪莱
愈是能够欣赏食物的人就愈不需要调味品，愈是能够欣赏饮料的人就愈不忙于寻求他所没有的饮料。——（古希腊历史学家）色诺芬
蔬果乃是无上的美味。他不再需要连续不断地去操作和毁坏各种器官，以求从它们那里获得满足。（英国诗人）雪莱
闲散、安逸和过去才会产生食欲；而时间和艰苦的劳动只能造成饥饿。——（俄国作家）冈察洛夫
要少吃，要常吃。——（法国作家）雨果";

        private const string _warnsettime = @"请设置个时间！
时间！时间！
设置时间啊！想急死我呀
先设置个时间好么？
你想让时光静止吗？
";

        public static string GetTip()
        {
            return Tip(_tips);
        }

        private static string Tip(string source)
        {
            var tiplist = source.Split('\n');
            Random r = new Random();
            var index = r.Next(0, tiplist.Length - 1);
            return tiplist[index];
        }

        public static string GetWarnning()
        {
            return Tip(_warnsettime);
        }
    }
}
