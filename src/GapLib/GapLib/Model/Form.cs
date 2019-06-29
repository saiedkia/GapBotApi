using System.Collections.Generic;

namespace GapLib.Model
{
    public class Form
    {
        public string Data { get; set; }
        public long Message_id { get; set; }
        public string Callback_id { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ParseData()
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();

            if (Data == null)
                return values;

            string[] dataValues = Data.Split('&');
            foreach(string item in dataValues)
            {
                string[] kv = item.Split('=');
                if (kv.Length == 2)
                    values.Add(new KeyValuePair<string, string>(kv[0], kv[1]));
            }


            return values;
        }
    }
}
