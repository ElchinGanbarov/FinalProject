﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Messenger.Lib
{
    public interface IFileManager
    {
        string Upload(IFormFile file, string savePath = "uploads", string newName = null);
        void Delete(string filename, string deletedPath = "uploads");
    }

    public class FileManager : IFileManager
    {
        public void Delete(string filename, string deletedPath = "uploads")
        {
            string path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot", deletedPath,
                                filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public string Upload(IFormFile file, string savePath = "uploads", string newName = null)
        {
            string filename;

            if (newName == null)
                filename = Guid.NewGuid()  + Path.GetExtension(file.FileName);
            else
                filename = newName  + Path.GetExtension(file.FileName);

            var writePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", savePath);
            if (!Directory.Exists(writePath))
                Directory.CreateDirectory(writePath);

            var path = Path.Combine(writePath, filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return filename;
        }
    }
}
