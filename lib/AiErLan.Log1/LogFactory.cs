using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using System.Web;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace AiErLan.Log
{
    /// <summary>
    /// 日志系统
    /// </summary>
    public class LogFactory
    {
        public static ConcurrentDictionary<string, ILog> logers;

        /// <summary>
        /// 日志初始化
        /// </summary>
        /// <param name="configFile"></param>
        public static void Initialize(string configFile = "log.config", bool isWeb = false)
        {
            logers = new ConcurrentDictionary<string, ILog>();
            logers.TryAdd("global", log4net.LogManager.GetLogger("global"));
            /**/
            string filepath;
            if (!isWeb)
                filepath = string.Format(@"{0}\{1}", Environment.CurrentDirectory, configFile);
            else
                filepath = string.Format(@"{0}\{1}", HttpContext.Current.Server.MapPath("."), configFile);
            FileInfo fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                log4net.Config.XmlConfigurator.Configure(fileInfo);
            }
        }

        /// <summary>
        /// 获取日志对象
        /// </summary>
        /// <returns></returns>
        public static ILog CreateLoger<T>(T t) where T : class
        {
            string logClassName = t.GetType().Name;
            if (!logers.ContainsKey(logClassName))
            {
                logers.TryAdd(logClassName, log4net.LogManager.GetLogger(logClassName));
            }
            return logers[logClassName];
            //StackTrace st = new StackTrace(1, true);
            //StackFrame[] stFrames = st.GetFrames();
            //Type t = stFrames[0].GetMethod().DeclaringType;
            //return new Loger(log4net.LogManager.GetLogger(t));
        }
        public static ILog CreateLoger(string name)
        {
            if (!logers.Keys.Any(c => c == name))
            {
                logers.TryAdd(name, log4net.LogManager.GetLogger(name));
            }
            return logers[name];
        }
    }
}
