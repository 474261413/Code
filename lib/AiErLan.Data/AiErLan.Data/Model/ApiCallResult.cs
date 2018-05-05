using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Data.Model
{
    /// <summary>
    /// 服务调用结果封装
    /// </summary>
    public class ApiCallResult<T>
    {
        /// <summary>
        /// 错误编号
        /// </summary>
        public int errorId { set; get; }
        /// <summary>
        /// 消息
        /// </summary>
        public string message { set; get; }
        /// <summary>
        /// 成功标志
        /// </summary>
        public bool isSuccess { set; get; }
        /// <summary>
        /// 数据
        /// </summary>
        public T data { set; get; }
    }

}
