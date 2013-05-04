using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IamYourShild
{
    public class TextBasic
    {
        public static LoadData LoadText(string fileName)
        {
            List<Dictionary<String, String>> data= new List<Dictionary<string,string>>();
            string[] loadStr;
            try {
                loadStr = File.ReadAllLines(fileName);
            }
            catch (FileNotFoundException) {
                System.Console.WriteLine("file not found");
                throw;
            }
            foreach(string str in loadStr) 
            {
                data.Add(LoadDictionary(str));
            }
          
            return new LoadData(data);
        }

        public static void WriteData(string fileName, List<Dictionary<string, string>> writeDataList)
        {
            string writeText = "";
            
            string[] writeDataStrList = new string[writeDataList.Count];
            int writeDataLine = 0;
            foreach (Dictionary<string, string> writeDatarow in writeDataList)
            {
                int writecount = writeDatarow.Count;
                int nowColumn = 1;
                writeText = "";
                foreach (KeyValuePair<string, string> writeDatacolumn in writeDatarow)
                {
                    if (nowColumn < writecount)
                    {
                        writeText += writeDatacolumn.Key + ":" + writeDatacolumn.Value + ",";
                    }
                    else 
                    {
                        writeText += writeDatacolumn.Key + ":" + writeDatacolumn.Value;
                    }
                    nowColumn++;
                }
                writeDataStrList[writeDataLine] = writeText;
                writeDataLine++;
            }
            try
            {
                File.WriteAllLines(fileName, writeDataStrList);
            }
            catch ( IOException)
            {
               
            }
        }

        private static Dictionary<String,String> LoadDictionary(string str)
        {
            Dictionary<String, String> dict = new Dictionary<string, string>();
            string[] lineStr = str.Split(',');
            foreach (string lstr in lineStr) 
            {
                string[] dictStr = lstr.Split(':');
                dict.Add(dictStr[0], dictStr[1]);
            }
            return dict;
        }

    }
}
