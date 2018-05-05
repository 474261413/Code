using AiErLan.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AiErLan.Handle
{
    public class News
    {
        DataHandle.DataOperate dataOperate = new DataHandle.DataOperate();
        public bool Edit(Data.News news, out long id)
        {
            id = 0;
            if (news.Id > 0)
            {
                Data.News model = dataOperate.Find<Data.News>(s => s.Id == news.Id);
                if (model != null)
                {
                    model.Contents = news.Contents;
                    model.Images = news.Images;
                    model.Intro = news.Intro;
                    model.Keys = news.Keys;
                    model.Remark = news.Remark;
                    model.Status = news.Status;
                    model.Title = news.Title;
                    model.Type = news.Type;
                    model.UserID = news.UserID;
                    model.ShowTime = news.ShowTime;
                    id = model.Id;
                    return dataOperate.Update(model);
                }
                return false;
            }
            else
            {
                news.CreateDateTime = DateTime.Now;
                bool b = dataOperate.Add(news);
                id = news.Id;
                return b;
            }
        }

        public IQueryable<Data.News> GetNewsList()
        {

            return dataOperate.FindAll<Data.News>();
        }

        /// <summary>
        /// 根据条件过去数据
        /// </summary>
        /// <param name="rep"></param>
        /// <returns></returns>
        public IQueryable<Data.News> GetNewsList(NewsReq rep, out int totalCount)
        {
            var list = dataOperate.FindAll<Data.News>();
            if (!string.IsNullOrWhiteSpace(rep.Name))
            {
                list = list.Where(s => s.Title.Contains(rep.Name));
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
        public IQueryable<Data.News> GetNewsList(Expression<Func<Data.News, bool>> fun)
        {

            return dataOperate.FindAll<Data.News>().Where(fun);
        }
        public Data.News GetNewsById(long? id)
        {
            return dataOperate.Find<Data.News>(s => s.Id == id);
        }
        public Data.News GetNewsById(long? id, int type)
        {
            return dataOperate.Find<Data.News>(s => s.Id == id && s.Type == type);
        }


        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelNews(long id)
        {
            var news = dataOperate.Find<Data.News>(s => s.Id == id);
            if (news != null)
            {
                return dataOperate.Remove(news);
            }
            else
            {
                return false;
            }
        }
    }
}
