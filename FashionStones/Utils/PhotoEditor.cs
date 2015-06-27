using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using FashionStones.Models.Domain.Entities;

namespace FashionStones.Utils
{
    public class PhotoEditor
    {
        private string TempFolder { get; set; }
        private string JewelPHotoFolder { get; set; }

        public PhotoEditor(string tempFolder, string jewelPHotoFolder)
        {
            TempFolder = tempFolder;
            JewelPHotoFolder = jewelPHotoFolder;
        }


        private static readonly ImageSize PhotoGallery = new ImageSize { Width = 640, Height = 800 };
        private static readonly ImageSize PhotoGalleryPreView = new ImageSize { Width = 200, Height = 250 };
       
        public static bool IsImage(string ext)
        {
            var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".JPG", ".JPEG" }; // допустимые форматы.
            return extensions.Any(p => p == ext);
        }
        public static bool CheckSize(WebImage img)
        {
            return img.Width >= PhotoGallery.Width && img.Height >= PhotoGallery.Height;
        }
        public string  SaveTempFile(HttpPostedFileBase file)
        {

            if (Directory.Exists(TempFolder) == false)
            {
                Directory.CreateDirectory(TempFolder);
            }
            string ext = Path.GetExtension(file.FileName);   
            if (IsImage(ext))
            {
                string newName = Guid.NewGuid().ToString("D") + ext;
              var img = new WebImage(file.InputStream);
                if (CheckSize(img))
                {
                    try
                    {
                        img = ReSizePreview(img);
                        img.Save(TempFolder + "\\" + newName);
                        CleanUpTempFolder(1);
                        return newName;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Произошла ошибка при попытке загрузки изображени.");
                    }
                 }
                throw  new ArgumentException("Малый размер изображения.");
            }
            throw new ArgumentException("Файл недопустимого формата.");
        }

        public string  SaveJevelPhot(int top, int left, int height, int width, string path)
        {
            
            FileInfo f = new FileInfo(path);
            var img = new WebImage(path); // наше изображение
            img.Resize(width, height); // уменьшим его 
            /********************************************************************************/
            int nHeight = img.Height - top - PhotoGallery.Height; // подготовим ширину и высоту
            int nWidth = img.Width - left - PhotoGallery.Width;
            if (nHeight < 0) nHeight = 0;
            /****************** Обрежим изображение, по выбранным кординатам ***************************************************/
            img.Crop(top, left, nHeight, nWidth);
            try
            {
                img.Save(JewelPHotoFolder + @"\big\" + f.Name);
                img.Resize(PhotoGalleryPreView.Width, PhotoGalleryPreView.Height);
                img.Save(JewelPHotoFolder + @"\small\" + f.Name);
                return f.Name;
            }
            catch (Exception)
            {
                throw new ArgumentException("Произошла ошибка при попытке сохранения изображени.");
            }
            
        }

        public WebImage ReSizePreview(WebImage img)
        {
            int ImageHeight = 800;
            int ImageWidth = 1200;

            double ratio;


            if (img.Width > img.Height && img.Width > ImageWidth)
            {
                ratio = (double)img.Height / (double)img.Width;

                int  nWidth = (int) (ImageHeight/ratio);

              //  img.Resize(ImageWidth, (int)(img.Height * ratio));

                img.Resize(nWidth, ImageHeight);
            }

            return img;
        }



        private void CleanUpTempFolder(int hoursOld)
        {
            DateTime fileCreationTime;
            DateTime currentUtcNow = DateTime.UtcNow;
            try
            {
             var serverPath = TempFolder;
                if (Directory.Exists(serverPath))
                {
                    string[] fileEntries = Directory.GetFiles(serverPath);
                    foreach (var fileEntry in fileEntries)
                    {
                        fileCreationTime = System.IO.File.GetCreationTimeUtc(fileEntry);
                        var res = currentUtcNow - fileCreationTime;
                        if (res.TotalHours > hoursOld)
                        {
                            System.IO.File.Delete(fileEntry);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }

    public class ImageSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

    }

}