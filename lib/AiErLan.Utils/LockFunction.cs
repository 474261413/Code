using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Utils
{
    /// <summary>
    /// 加锁执行
    /// </summary>
    public class LockExecution
    {
        private static object _object = new object();
        /// <summary>
        /// 无参数带返回值的回销执行
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="fun">方法过程</param>
        /// <returns></returns>
        public static T ExecutionFun<T>(Func<T> fun)
        {
            T t;
            //_object = new object();
            lock (_object)
            {
                t = fun.Invoke();
            }
            return t;
        }

        /// <summary>
        /// 有参数的带返回值加锁执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fun"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TResult ExecutionFun<T, TResult>(Func<T, TResult> fun, T t)
        {
            TResult result;
            //_object = new object();
            lock (_object)
            {
                result = fun.Invoke(t);
            }
            return result;
        }

        /// <summary>
        /// 无参无返回值的回销执行动作
        /// </summary>
        /// <param name="action">执行的动作</param>
        public static void ExecutionAction(Action action)
        {
            //_object = new object();
            lock (_object)
            {
                action.Invoke();
            }
        }
    }
}
