﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AiErLan.Utils
{
    public class EbdFileStream
    {
        /// <summary>
        /// 建立文件夹
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CreateDir(string name)
        {
            return MakeSureDirectoryPathExists(name);
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>创建是否成功</returns>
        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);

        /// <summary> 
        /// 获取指定mimeType的ImageCodecInfo 
        /// </summary> 
        private static ImageCodecInfo GetImageCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            return CodecInfo.FirstOrDefault(ici => ici.MimeType == mimeType);
        }
        /// <summary>
        /// 获取inputStream中的Bitmap对象
        /// </summary>  
        public static Bitmap GetBitmapFromStream(Stream inputStream)
        {
            Bitmap bitmap = new Bitmap(inputStream);
            inputStream.Dispose();
            return bitmap;
        }

        /// <summary>
        /// 将inputStream中的对象压缩为JPG图片类型 
        /// </summary> 
        /// <param name="inputStream">源Stream对象</param> 
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="quality">压缩质量，越大照片越清晰，推荐80</param> 
        public static void CompressAsJPG(Stream inputStream, string saveFilePath, int quality)
        {
            using (var img = Image.FromStream(inputStream))
            {
                EncoderParameter p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);
                EncoderParameters ps = new EncoderParameters(1);
                ps.Param[0] = p;

                try
                {
                    img.Save(saveFilePath, GetImageCodecInfo("image/jpeg"), ps);
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// 保存原图像，不压缩(推荐用于保存PNG、GIF等带透明效果的图片)
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="saveFilePath"></param>
        public static void SaveAs(Stream inputStream, string saveFilePath)
        {
            using (var img = Image.FromStream(inputStream))
            {
                img.Save(saveFilePath);
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }

        }

        #region Stream 和 byte[]、 文件之间的转换
        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);

            return stream;
        }

        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 从文件读取 Stream
        /// </summary>
        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        #endregion
    }
}
