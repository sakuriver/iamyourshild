using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IamYourShild
{
    public class LoadData
    {
        private List<Dictionary<String, String>> data;
        public List<Dictionary<string, string>> Data { get { return data; } }
        public LoadData() 
        {
            data = new List<Dictionary<string,string>>();
        }
        public LoadData(List<Dictionary<string, string>> list) 
        {
            data = list;
        }
        
    }
}
