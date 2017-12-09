using MatoIndustry.Model;

namespace MatoIndustry.Common
{
  public class CommonFunctionEventArgs
    {
        public CommonFunctionEventArgs(IBasicInfo info, string code)
        {
            this.Info = info;
            this.Code = code;
        }
        public IBasicInfo Info { get; set; }
        public string Code { get; set; }

    }
}
