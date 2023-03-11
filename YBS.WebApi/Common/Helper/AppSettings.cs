using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class AppSettings
    {
        public string RedisConnectionString { get; set; }
        public bool TranAOP { get; set; }
        public bool RedisCacheAOP { get; set; }
    }
}
