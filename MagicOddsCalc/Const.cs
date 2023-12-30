using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicOddsCalc
{
    public class Const
    {
        private static readonly IConfigurationRoot MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public static readonly string REQUIREMENTS_FILE = MyConfig.GetValue<string>("Settings:REQ_FILE_NAME") ?? "";
        public static readonly bool DEBUG_MODE = MyConfig.GetValue<bool>("Settings:DEBUG_MODE");
    }
}
