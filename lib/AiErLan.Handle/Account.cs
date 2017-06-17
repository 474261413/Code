using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Handle
{
    /// <summary>
    /// 用户中心
    /// </summary>
    public class Account
    {
        DataHandle.DataOperate dataOperate = new DataHandle.DataOperate();

        public Data.Admin Login(string loginName, string pwd)
        {
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(pwd))
            {
                return null;
            }
            // pwd = Utils.Encrypt.MD5Encrypt(pwd);
            Expression<Func<Data.Admin, bool>> filter = t => t.LogName == loginName && t.Pwd == pwd;
            var user = dataOperate.Find(filter);
            return user;
        }

        /// <summary>
        /// 根据条件过去数据
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public IQueryable<Data.Admin> GetAdminByFun(Expression<Func<Data.Admin, bool>> fun)
        {
            return dataOperate.Filter(fun);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="pwd"></param>
        /// <param name="npwd"></param>
        /// <returns></returns>
        public bool EditPwd(string loginName, string pwd, string npwd)
        {

            bool b = false;
            var user = Login(loginName, pwd);
            if (user != null)
            {
                user.Pwd = Utils.Encrypt.MD5Encrypt(npwd);
                return dataOperate.SaveChange();
            }
            return b;

        }
    }
}
