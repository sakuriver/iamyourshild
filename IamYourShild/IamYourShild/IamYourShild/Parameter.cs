using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IamYourShild
{
    class Parameter
    {
        Dictionary<string, string> parameter_list = new Dictionary<string, string>();

        public void SetParameter(Dictionary<string, string> set_list) 
        {
            // パラメータをセットする時は一度リセットすることが前提
            parameter_list = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> kvp in set_list) 
            {
                parameter_list.Add(kvp.Key, kvp.Value);
            }
        
        }

        public string this[string Name] 
        {
            get { return parameter_list[Name]; }
            set { parameter_list[Name] = value; }
        }

    }
}
