using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PCLStorage;

namespace MatoIndustry.Helper
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<bool> IsFileExistAsync(this string fileName)
        {
            // get hold of the file system
            IFolder folder = FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(fileName);
            // already run at least once, don't overwrite what's there
            if (folderexist == ExistenceCheckResult.FileExists)
            {
                return true;

            }
            return false;
        }

        /// <summary>
        /// 文件夾是否存在
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<bool> IsFolderExistAsync(this string folderName, IFolder rootFolder = null)
        {
            // get hold of the file system
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(folderName);
            // already run at least once, don't overwrite what's there
            if (folderexist == ExistenceCheckResult.FolderExists)
            {
                return true;

            }
            return false;
        }

        /// <summary>
        /// 創建文件夾
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<IFolder> CreateFolder(this string folderName, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
            return folder;
        }

        /// <summary>
        /// 創建文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<IFile> CreateFile(this string filename, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            return file;
        }

        /// <summary>
        /// 創建文本文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="content"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<bool> WriteTextAllAsync(this string filename, string content = "", IFolder rootFolder = null)
        {
            try
            {
                IFile file = await filename.CreateFile(rootFolder);
                await file.WriteAllTextAsync(content);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// 讀取文本文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<string> ReadAllTextAsync(this string fileName)
        {
            string content = "";
            IFolder folder = FileSystem.Current.LocalStorage;
            bool exist = await fileName.IsFileExistAsync();
            if (exist == true)
            {
                IFile file = await folder.GetFileAsync(fileName);
                content = await file.ReadAllTextAsync();
            }
            return content;
        }

        /// <summary>
        /// 刪除文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<bool> DeleteFile(this string fileName)
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            bool exist = await fileName.IsFileExistAsync();
            if (exist == true)
            {
                IFile file = await folder.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保存圖片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fileName"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task SaveImage(this byte[] image, String fileName, IFolder rootFolder = null)
        {
            // get hold of the file system
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            // create a file, overwriting any existing file
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            // populate the file with image data
            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                stream.Write(image, 0, image.Length);
            }
        }

        /// <summary>
        /// 讀取圖片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fileName"></param>
        /// <param name="rootFolder"></param>
        /// <returns></returns>
        public async static Task<byte[]> LoadImage(this byte[] image, String fileName, IFolder rootFolder = null)
        {
            // get hold of the file system
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            //open file if exists
            IFile file = await folder.GetFileAsync(fileName);
            //load stream to buffer
            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                long length = stream.Length;
                byte[] streamBuffer = new byte[length];
                stream.Read(streamBuffer, 0, (int)length);
                return streamBuffer;
            }

        }
    }


}
