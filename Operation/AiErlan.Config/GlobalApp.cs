using AiErLan.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Config
{
    /// <summary>
    /// 全局设置
    /// </summary>
    public class GlobalApp
    {
        private static GlobalApp _app;
        public static GlobalApp Instance
        {
            get
            {

                if (_app == null)
                    _app = LockExecution.ExecutionFun(() => new GlobalApp());
                return _app;
            }
        }

        /// <summary>
        /// 全局初始化
        /// </summary>
        /// <param name="action">自助动作</param>
        public void Initialize(Action action = null)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.None,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
                ContractResolver = new AiErLan.Utils.Serializer.NullToEmptyListResolver()
            };
            if (action != null)
                action();
        }
    }
}
