/**
* Title: 文件操作的方法工具
* Author: other
* Date: long long ago
* Desp:
*/

using System;
using System.IO;
using System.Text;

namespace dataAnalize
{
    public class FileHelper
    {
        #region 目录检测相关操作

        /// <summary>
        /// 检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        /// <returns></returns>
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 检测指定目录是否为空
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static bool IsEmptyDirectory(string directoryPath)
        {
            try
            {
                string[] fileNames = GetDirectoryFileNames(directoryPath, "*", false);   //判断是否存在文件
                if (fileNames.Length > 0)
                {
                    return false;
                }
                string[] directoryNames = GetDirectories(directoryPath, "*", false);   //判断是否存在文件夹
                if (directoryNames.Length > 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        public static bool DirectoryContains(string directoryPath, string searchPattern)
        {
            try
            {
                string[] fileNames = GetDirectoryFileNames(directoryPath, searchPattern, false);   //获取指定的文件列表

                if (fileNames.Length == 0)   //判断指定文件是否存在
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static bool DirectoryContains(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                //获取指定的文件列表
                string[] fileNames = GetDirectoryFileNames(directoryPath, searchPattern, true);

                //判断指定文件是否存在
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }

        /// <summary>
        /// 获取指定目录及子目录中所有子目录列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定目录及子目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetDirectoryFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            try
            {
                if (isSearchChild)
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 目录相关操作的内容

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dir">要创建的目录路径包括目录名</param>
        public static void CreateDirectory(string dir)
        {
            if (dir.Length == 0) return;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dir">要删除的目录路径和名称</param>
        public static void DeleteDirectory(string dir)
        {
            if (dir.Length == 0) return;
            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
        }

        /// <summary>
        /// 清空指定目录下所有文件及子目录,但该目录依然保存.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void ClearDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                //删除目录中所有的文件
                string[] fileNames = GetDirectoryFileNames(directoryPath, "*", false);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    DeleteFile(fileNames[i]);
                }
                //删除目录中所有的子目录
                string[] directoryNames = GetDirectories(directoryPath, "*", false);
                for (int i = 0; i < directoryNames.Length; i++)
                {
                    DeleteDirectory(directoryNames[i]);
                }
            }
        }

        /// <summary>
        /// 复制文件夹(递归)
        /// </summary>
        /// <param name="varFromDirectory">源文件夹路径</param>
        /// <param name="varToDirectory">目标文件夹路径</param>
        public static void CopyDirectory(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);

            if (!Directory.Exists(varFromDirectory)) return;

            string[] directories = Directory.GetDirectories(varFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyDirectory(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }
            string[] files = Directory.GetFiles(varFromDirectory);
            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, varToDirectory + s.Substring(s.LastIndexOf("\\")), true);
                }
            }
        }

        #endregion

        #region 文件本身操作

        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 创建一个文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);

                    //创建文件
                    FileStream fs = file.Create();

                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        public static void CreateFile(string path, string content)
        {
            FileInfo fi = new FileInfo(path);
            var di = fi.Directory;
            if (!di.Exists)
            {
                di.Create();
            }
            StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("GB2312"));
            sw.Write(content);
            sw.Close();

            //path = path.Replace("/", "\\");
            //if (path.IndexOf("\\") > -1)
            //    CreateDirectory(dir.Substring(0, path.LastIndexOf("\\")));
            //StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("GB2312"));
            //sw.Write(pagestr);
            //sw.Close();
        }
        /// <summary>
        /// 创建一个文件,并将字节流写入文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="buffer">二进制流数据</param>
        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);

                    //创建文件
                    FileStream fs = file.Create();

                    //写入二进制流
                    fs.Write(buffer, 0, buffer.Length);

                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file">要删除的文件路径和名称</param>
        public static void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        /// <summary>
        /// 将文件移动到指定目录
        /// </summary>
        /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param>
        /// <param name="descDirectoryPath">移动到的目录的绝对路径</param>
        public static void MoveFile(string sourceFilePath, string descDirectoryPath)
        {
            //获取源文件的名称
            string sourceFileName = GetFileName(sourceFilePath);

            if (IsExistDirectory(descDirectoryPath))
            {
                //如果目标中存在同名文件,则删除
                if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
                {
                    DeleteFile(descDirectoryPath + "\\" + sourceFileName);
                }
                //将文件移动到指定目录
                File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
            }
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="dir1">要复制的文件的路径已经全名(包括后缀)</param>
        /// <param name="dir2">目标位置,并指定新的文件名</param>
        public static void CopyFile(string dir1, string dir2)
        {
            if (File.Exists(dir1))
            {
                File.Copy(dir1, dir2, true);
            }
        }

        #endregion

        #region 文件内容检测
        /// <summary>
        /// 获取文本文件的行数
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static int GetLineCount(string filePath)
        {
            //将文本文件的各行读到一个字符串数组中
            string[] rows = File.ReadAllLines(filePath);

            //返回行数
            return rows.Length;
        }

        /// <summary>
        /// 获取一个文件的长度,单位为Byte
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static long GetFileSize(string filePath)
        {
            //创建一个文件对象
            FileInfo fi = new FileInfo(filePath);

            //获取文件的大小
            return fi.Length;
        }

        /// <summary>
        /// 计算文件大小函数(保留两位小数),Size为字节大小
        /// </summary>
        /// <param name="size">初始文件大小</param>
        /// <returns></returns>
        public static string ToFileSize(long size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " 字节";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " KB";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " MB";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " GB";
            return m_strSize;
        }
        #endregion

        #region 文件内容操作

        /// <summary>
        /// 向文本文件中写入内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="text">写入的内容</param>
        /// <param name="encoding">编码</param>
        public static void WriteText(string filePath, string text, Encoding encoding)
        {
            //向文件写入内容
            File.WriteAllText(filePath, text, encoding);
        }

        /// <summary>
        /// 向文本文件的尾部追加内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="content">写入的内容</param>
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }

        /// <summary>
        /// 获取文件的所有内容
        /// </summary>
        /// <returns></returns>
        public static string ReadAllFileContent(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string strRead = sr.ReadToEnd(); //从开始到末尾读取文件的所有内容
            sr.Close(); //读完文件记得关闭流

            return strRead;
        }

        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="filePath">文件路径文件名称</param>
        /// <returns>byte[]</returns>
        public static byte[] ReadFileByteData(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffur = new byte[fs.Length];
                using (BinaryReader br = new BinaryReader(fs))
                {
                    br.Read(buffur, 0, (int)fs.Length);
                    br.Close();
                }
                return buffur;
            }
        }

        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="filePath">文件路径文件名称</param>
        /// <returns>byte[]</returns>
        public static byte[] ReadFileByteData2(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 清空文件内容,文件流不会关闭
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void ClearFile(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Truncate, FileAccess.ReadWrite);
            fs.Close();
        }

        /// <summary>
        /// 清空文件内容,文件流不会关闭
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static FileStream ClearFileAndGet(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Truncate, FileAccess.ReadWrite);
            return fs;
        }

        #endregion

        #region 文件路径处理

        /// <summary>
        /// 从文件的绝对路径中获取扩展名
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string GetExtension(string filePath)
        {
            filePath = filePath.Trim();
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            string suffix = fi.Extension;
            return suffix.ToLower();
            //string suffix = filePath.Substring(filePath.LastIndexOf(".") + 1);
            //return suffix.ToLower();
        }

        /// <summary>
        /// 从文件的绝对路径中获取最末层文件夹路径
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string GetDirectoryPath(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            return fi.DirectoryName;
        }

        /// <summary>
        /// 从文件的绝对路径中获取文件名( 包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string GetFileName(string filePath)
        {
            filePath = filePath.Trim();
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Name;
            //string fileName = filePath.Substring(filePath.LastIndexOf('/') + 1);
            //return fileName;
        }

        /// <summary>
        /// 从文件的绝对路径中获取文件名( 不包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string GetFileNameNoExtension(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Name.Split('.')[0];
        }

        /// <summary>
        /// 根据相对路径转化为绝对路径
        /// </summary>
        /// <param name="relativePath">相对路径, 直接加文件夹名称即可</param>
        /// <returns></returns>
        public static string GetFullPath(string relativePath)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            return basePath + relativePath;
        }

        #endregion
    }
}
