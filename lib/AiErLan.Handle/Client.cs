using AiErLan.Data.Enums;
using AiErLan.Data.Model;
using AiErLan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Handle
{
    /// <summary>
    /// 客户管理
    /// </summary>
    public class Client
    {
        DataHandle.DataOperate dataOperate = new DataHandle.DataOperate();
        public bool Edit(Data.Client client, out long id)
        {
            string emailcofig = string.Format("{{ \"Address\":\"{0}\",\"Account\":\"{1}\",\"SmtpServer\":\"{2}\",\"EmailPwd\":\"{3}\"}}", "474261413@qq.com",
                "474261413@qq.com",
                "smtp.qq.com",
                "a123123123??");
           
            string content = string.Format("姓名:{0}\n 电话:{1} ,\n  类型:{2},\n 面积:{3}, \n地址:{4},\n 其他说明:{5},\n 来源:{6} ", client.Name,client.Phone, EnumUtil.GetDescription(typeof(ClientType),client.Type),client.Serial,client.Address,client.Remark, EnumUtil.GetDescription(typeof(Source), client.Source));
            try
            {
                AiErLan.Utils.SendMail sendmail = new Utils.SendMail(emailcofig);
                sendmail.SendEmail("184462359@qq.com", "甲醛检测预约", content);
            }
            catch (Exception e)
            {

                Log.LogFactory.CreateLoger(this).Error("邮件发送失败 内容" + content, e);
            }

            id = 0;
            if (client.Id > 0)
            {
                Data.Client model = dataOperate.Find<Data.Client>(s => s.Id == client.Id);
                if (model != null)
                {
                    model.Remark = client.Remark;
                    model.Status = client.Status;
                    model.Type = client.Type;
                    model.Name = client.Name;
                    model.Phone = client.Phone;
                    model.Serial = client.Serial;
                    model.Sex = client.Sex;
                    model.Address = client.Address;
                    model.Source = client.Source;
                    id = model.Id;
                    return dataOperate.Update(model);
                }
                return false;
            }
            else
            {
                client.CreateDateTime = DateTime.Now;
                bool b = dataOperate.Add(client);
                id = client.Id;
                return b;
            }
        }

        public IQueryable<Data.Client> GetClientList()
        {

            return dataOperate.FindAll<Data.Client>();
        }

        /// <summary>
        /// 根据条件过去数据
        /// </summary>
        /// <param name="rep"></param>
        /// <returns></returns>
        public IQueryable<Data.Client> GetClientList(ClientReq rep, out int totalCount)
        {
            var list = dataOperate.FindAll<Data.Client>();
            if (!string.IsNullOrWhiteSpace(rep.Name))
            {
                list = list.Where(s => s.Name.Contains(rep.Name));
            }
            if (rep.Type.HasValue)
            {
                list = list.Where(s => s.Type == rep.Type);
            }
            list = list.OrderByDescending(s => s.CreateDateTime);
            totalCount = list.Count();
            if (rep.PageIndex != null && rep.PageSize != null)
            {
                list = list.Skip((rep.PageIndex.Value - 1) * rep.PageSize.Value).Take(rep.PageSize.Value);
            }
            return list;

        }
        public IQueryable<Data.Client> GetClientList(Expression<Func<Data.Client, bool>> fun)
        {
            return dataOperate.FindAll<Data.Client>().Where(fun);
        }
        public Data.Client GetClientById(long? id)
        {
            return dataOperate.Find<Data.Client>(s => s.Id == id);
        }
        public Data.Client GetClientById(long? id, int type)
        {
            return dataOperate.Find<Data.Client>(s => s.Id == id && s.Type == type);
        }
    }
}
