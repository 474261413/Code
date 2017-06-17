using AiErLan.Config.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AiErLan.Config
{
    public class MessageQueueConfig
    {
        public static void RegisterExceptionLogQueue()
        {
            //通过线程池开启线程，不停地从队列中获取异常信息并将其写入日志文件
            Task.Run(()=> {
                while (true)
                {
                    try
                    {
                        if (AelExceptionFilterAttribute.ExceptionQueue.Count > 0)
                        {
                            Exception ex = AelExceptionFilterAttribute.ExceptionQueue.Dequeue();
                            if (ex != null)
                            {
                                if(!ex.Message.Contains("SSL/TLS"))
                                    AiErLan.Log.LogFactory.CreateLoger("EbdExceptionFilter").Error("系统出现异常", ex);
                            }
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }
                    }
                    catch (Exception ex)
                    {
                        Filter.AelExceptionFilterAttribute.ExceptionQueue.Enqueue(ex);
                    }
                }
            });
        }
    }
}
