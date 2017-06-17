using FtpSupport;
using System;
using System.Diagnostics;
using System.IO;
using AiErLan.Log;

namespace AiErLan.Utils
{
    /// <summary>
    /// ftp帮助类
    /// </summary>
    public class Ftp
    {

        private string _ftpUri;

        private string _userName;

        private string _passWord;

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="ftpUri">ftp地址：例如："ftp.cqebd.cn"</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        public Ftp(string ftpUri, string userName, string passWord)
        {
            _ftpUri = ftpUri;
            _userName = userName;
            _passWord = passWord;
        }

        /// <summary>
        /// ftp上传(0,上传失败 、1,上传成功)
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="dr">ftp服务器端目录名称，形如/vagerent/image/</param>
        /// <param name="newRemoteFile">传到服务器后的文件名称，比如log.txt</param>
        /// <returns></returns>
        public bool UploadFile(Stream fileStream, string dr, string newRemoteFile)
        {
            string remoteDr = "/";
            FtpConnection ftp = new FtpConnection();
            try
            {
                string ftpIP = _ftpUri;
                string ftpUsrName = _userName;
                string ftpUsrPsw = _passWord;
                ftp.Connect(ftpIP, ftpUsrName, ftpUsrPsw);
                remoteDr = dr;
                if (!ftp.DirectoryExist(remoteDr))
                {
                    ftp.CreateDirectory(remoteDr);
                }
                ftp.SetCurrentDirectory(remoteDr);
                ftp.PutStream(fileStream, remoteDr + newRemoteFile);
                return true;//上传成功

            }
            catch (Exception e)
            {
                AiErLan.Log.LogFactory.CreateLoger(this).Error(string.Format("上传图片失败 UploadFile {0}", e.Message), e);
                return false;//上传失败
            }
            finally
            {
                ftp.Close();
            }
        }

        /// <summary>
        /// 异步上传
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="dr">ftp服务器端目录名称，形如/vagerent/image/</param>
        /// <param name="newRemoteFile">传到服务器后的文件名称，比如log.txt</param>
        /// <param name="CallBack">异步回调函数</param>

        public void AsyncUploadFile(Stream fileStream, string dr, string newRemoteFile, AsyncCallback CallBack)
        {
            try
            {
                UploadFileDelegate dl = UploadFile;
                IAsyncResult ia = dl.BeginInvoke(fileStream, dr, newRemoteFile, CallBack, null);
            }
            catch (Exception ex)
            {
                LogFactory.CreateLoger(this).Error(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 文件夹上传
        /// </summary>
        /// <param name="direction">本地文件夹路径</param>
        /// <param name="uploadDirection">Ftp文件路径</param>
        public void FolderUploadFile(string direction, string uploadDirection)
        {
            FtpConnection ftp = new FtpConnection();
            string ftpIP = _ftpUri;
            string ftpUsrName = _userName;
            string ftpUsrPsw = _passWord;
            ftp.Connect(ftpIP, ftpUsrName, ftpUsrPsw);
            DirectoryInfo TheFolder = new DirectoryInfo(direction);
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                try
                {
                    if (NextFile.Name.Contains(".htm") == false)
                    {
                        FileStream files = new FileStream(NextFile.FullName, FileMode.Open);
                        byte[] imgByte = new byte[files.Length];
                        files.Read(imgByte, 0, imgByte.Length);
                        files.Close();
                        Stream stream = new MemoryStream(imgByte);
                        AsyncUploadFile(stream, uploadDirection, NextFile.Name, null);
                    }
                }
                catch (Exception ex)
                {
                    AiErLan.Log.LogFactory.CreateLoger(this).Error(ex.Message);
                    throw ex;
                }
            }
        }

        private delegate bool UploadFileDelegate(Stream fileStream, string dr, string newRemoteFile);
    }
}
