using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Utils
{
    /// <summary>
    /// 产生随机数 by yang
    /// </summary>
    public static class RandomNum
    {
        /// <summary>
        /// 获取4位数的随机数
        /// </summary>
        /// <returns></returns>
        public static int RanNum()
        {
            Random ran = new Random();
            return ran.Next(1000, 9999);
        }
    }
}
