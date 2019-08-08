using GapLib.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GapLib.Model
{
    public class FormResult
    {
        public string Data { get; set; }
        public long MessageId { get; set; }
        public string CallbackId { get; set; }

        public List<KeyValuePair<string, string>> ParseData()
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();

            if (Data == null)
                return values;

            string[] dataValues = Data.Split('&');
            foreach (string item in dataValues)
            {
                string[] kv = item.Split('=');
                if (kv.Length == 2)
                    values.Add(new KeyValuePair<string, string>(kv[0], kv[1]));
            }


            return values;
        }
    }

    public class Form : List<FormItem>
    {
        public Form() { }
        public void AddItem(FormItem item)
        {
            Add(item);
        }
    }


    [JsonConverter(typeof(FormConverter))]
    public class FormItem
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public FormType Type { get; set; }
    }

    public class FormItemOptional : FormItem
    {
        public List<KeyValuePair<string, string>> Options { get; set; }

        public FormItemOptional()
        {
            Options = new List<KeyValuePair<string, string>>();
        }

        public void Add(string key, string value)
        {
            Options.Add(new KeyValuePair<string, string>(key, value));
        }
    }


    public enum FormType
    {
        Text,
        Radio,
        Select,
        Textarea,
        Checkbox,
        Inbuilt,
        Submit
    }
}
