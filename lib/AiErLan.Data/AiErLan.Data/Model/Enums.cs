using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Data.Enums
{
    /// <summary>
    /// 新闻状态
    /// </summary>
    public enum NewsStatus : int
    {
        /// <summary>
        /// 显示
        /// </summary>
        [Description("显示")]
        XS = 1,
        /// <summary>
        /// 隐藏
        /// </summary>
        [Description("隐藏")]
        YC = 0,
    }

    /// <summary>
    /// 新闻类型
    /// </summary>
    public enum NewsType : int
    {

        /// <summary>
        /// 行业新闻
        /// </summary>
        [Description("行业新闻")]
        XW = 0,
        /// <summary>
        /// 产品介绍
        /// </summary>
        [Description("产品介绍")]
        CP = 1,
        /// <summary>
        /// 经典案例
        /// </summary>
        [Description("经典案例")]
       JDAL = 2,
        /// <summary>
        /// 资质证书
        /// </summary>
        [Description("资质荣誉")]
        ZZRY = 3,
    }
}
