using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace AiErLan.Utils
{
    public static class EnumUtil
    {
        #region 工具函数
        /// <summary>
        /// 获取枚举列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<EnumModel> GetList(System.Type t)
        {
            List<EnumModel> list = new List<EnumModel>();
            EnumModel model = null;
            Array a = System.Enum.GetValues(t);
            for (int i = 0; i < a.Length; i++)
            {
                model = new EnumModel();
                string enumKey = a.GetValue(i).ToString();
                int enumid = (int)System.Enum.Parse(t, enumKey);
                string enumDescription = GetDescription(t, enumid);
                model.EnumKey = enumKey;
                model.EnumID = enumid.ToString();
                model.EnumDescription = enumDescription;
                list.Add(model);
            }
            return list;
        }

        private static string GetName(System.Type t, object v)
        {
            try
            {
                return Enum.GetName(t, v);
            }
            catch
            {
                return "未知";//UNKNOWN
            }
        }


        /// <summary>
        /// 返回指定枚举类型的指定值的描述
        /// </summary>
        /// <param name="t">枚举类型</param>
        /// <param name="v">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(System.Type t, object v)
        {
            try
            {
                FieldInfo fi = t.GetField(GetName(t, v));
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : GetName(t, v);
            }
            catch
            {
                return "未知";
            }
        }
        #endregion
    }

    public class EnumModel
    {
        /// <summary>
        ///  枚举ID
        /// </summary>
        public string EnumID { get; set; }
        /// <summary>
        /// 枚举描述
        /// </summary>
        public string EnumDescription { get; set; }
        /// <summary>
        /// 枚举键值
        /// </summary>
        public string EnumKey { get; set; }

    }
}
