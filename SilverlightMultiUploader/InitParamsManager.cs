using System.Collections.Generic;

namespace SilverlightMultiUploader
{
    public class InitParamsManager
    {
        private static IDictionary<string, string> _initParams;
        public static IDictionary<string, string> InitParams
        {
            get { return _initParams ?? (_initParams = new Dictionary<string, string>()); }
            set { _initParams = value; }
        }
    }
}