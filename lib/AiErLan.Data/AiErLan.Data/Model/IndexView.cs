using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Data.Model
{
    public class IndexView
    {
       /// <summary>
       ///产品
       /// </summary>
        public IQueryable<Data.News> Product { get; set; }
        /// <summary>
        /// 新闻
        /// </summary>
        public IQueryable<Data.News> News { get; set; }
        /// <summary>
        /// 客户案例
        /// </summary>
        public IQueryable<Data.News> Customer { get; set; }
    }
}
