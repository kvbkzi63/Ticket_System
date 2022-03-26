using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ForDemo.Utility
{
    public class DataUtility
    {
        /// <summary>
        /// 拿出資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<T> GetData<T>(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.UTF8))
            {
              return JsonConvert.DeserializeObject<List<T>>(sr.ReadToEnd());
            }
        }
        /// <summary>
        /// 寫入資料
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileContent"></param>
        public void WriteToJsonFile(string filePath,string fileContent)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
                var W = new StreamWriter(filePath);
                W.WriteLine(fileContent);
                W.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}