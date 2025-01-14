﻿using Jing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Zero
{
    /// <summary>
    /// 下载的文件保存为临时文件，后缀.temp。下载完成后再改名，这样断点续传好处理
    /// </summary>    
    public class HttpDownloadHandler : DownloadHandlerScript
    {
        public delegate void ReceivedData(int contentLength);

        /// <summary>
        /// 保存路径
        /// </summary>
        public string savePath { get; private set; }

        /// <summary>
        /// 是否断点续传
        /// </summary>
        public bool isResumeable { get; private set; }

        /// <summary>
        /// 已下载文件大小
        /// </summary>
        public long downloadedSize { get; private set; } = 0;

        /// <summary>
        /// 文件大小
        /// </summary>
        public long totalSize { get; private set; } = 0;

        /// <summary>
        /// 下载进度
        /// </summary>
        public float progress { get; private set; } = 0;

        /// <summary>
        /// 收到数据的事件，参数是这次收到的数据长度
        /// </summary>
        //public event Action<int> onReceivedData;

        FileStream _fileStream;

        string _tempSavePath;

        /// <summary>
        /// 下载到了数据的更新
        /// </summary>
        public event ReceivedData onReceivedData;

        /// <summary>
        /// 收到了ResponseHeader数据
        /// </summary>
        public event Action onReceivedHeaders;

        public HttpDownloadHandler(string savePath, bool isResumeable):base(new byte[200 << 10])
        {            
            this.savePath = savePath;
            this.isResumeable = isResumeable;
            _tempSavePath = savePath + ".temp";

            string saveDir = Path.GetDirectoryName(savePath);
            if (Directory.Exists(saveDir) == false)
            {
                Directory.CreateDirectory(saveDir);
            }

            FileMode mode = isResumeable ? FileMode.Append : FileMode.OpenOrCreate;

            try
            {
                _fileStream = new FileStream(_tempSavePath, mode, FileAccess.Write, FileShare.None);
                if (isResumeable)
                {
                    //断点续传的话，则先记录已下载的文件尺寸
                    downloadedSize = _fileStream.Length;           
                }
            }
            catch(Exception e)
            {
                Debug.Log(e);
                _fileStream = null;
            }
        }

        protected override void ReceiveContentLengthHeader(ulong contentLength)
        {            
            //已收到过长度(iOS下可能重复收到)，或者长度为0，则直接返回
            if(0 == contentLength || totalSize > 0)
            {
                //Debug.Log($"重复收到ReceiveContentLengthHeader:{totalSize}");
                return;
            }            
            totalSize = (long)contentLength + downloadedSize;                        
            onReceivedHeaders?.Invoke();
        }

        protected override bool ReceiveData(byte[] data, int dataLength)
        {            
            if(data == null || data.Length == 0)
            {
                return false;
            }

            if(_fileStream == null || _fileStream.CanWrite == false)
            {
                return false;
            }

            try
            {
                _fileStream.Write(data, 0, dataLength);                
            }
            catch
            {
                return false;
            }
            downloadedSize += dataLength;
            progress = (float)downloadedSize / totalSize;

            //Debug.Log($"下载到数据大小:{dataLength} 完成度:{GetProgress()} 已下载内容大小:{downloadedSize}/{totalSize}");

            onReceivedData?.Invoke(dataLength);

            return true;
        }

        protected override void CompleteContent()
        {                      
            CloseFileStream();
            FileUtility.MoveFile(_tempSavePath, savePath, true);
        }

        protected override float GetProgress()
        {
            return progress;
        }

        protected override byte[] GetData()
        {
            if(_fileStream != null)
            {
                return null;
            }
            return File.ReadAllBytes(savePath);            
        }

        protected override string GetText()
        {
            if (_fileStream != null)
            {
                return null;
            }
            return File.ReadAllText(savePath);
        }

        /// <summary>
        /// 停止下载，并销毁
        /// </summary>
        /// <param name="isCleanTmepFile">是否清理已下载的临时文件</param>
        public void DisposeSafely(bool isCleanTmepFile)
        {
            CloseFileStream();

            if (isCleanTmepFile && File.Exists(_tempSavePath))
            {
                File.Delete(_tempSavePath);
            }
            Dispose();
        }

        void CloseFileStream()
        {
            if (null != _fileStream)
            {
                _fileStream.Flush();
                _fileStream.Close();
                _fileStream.Dispose();
                _fileStream = null;
            }
        }
    }
}
