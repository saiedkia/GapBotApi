using GapLib.Builders;
using GapLib.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GapLib.Model
{
    public class ReplyKeyboard
    {
        public List<List<ReplyKeyboardItem>> Keyboard { get; set; }

        public ReplyKeyboard()
        {
            Keyboard = new List<List<ReplyKeyboardItem>>();
        }

        public void AddRow(IEnumerable<ReplyKeyboardItem> rowItems)
        {
            Keyboard.Add(rowItems.ToList());
        }

        public static ReplyKeyboardBuilder Builder()
        {
            return new ReplyKeyboardBuilder();
        }
    }

    [JsonConverter(typeof(ReplyKeyboardConverter))]
    public class ReplyKeyboardItem
    {
        public string Key { get; set; }
        public string Value { get; set; }


        public ReplyKeyboardItem(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public ReplyKeyboardItem(string value)
        {
            Key = value;
            Value = value;
        }


        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class ReplyKeyboardContactItem : ReplyKeyboardItem
    {
        public ReplyKeyboardContactItem(string value) : base("$contact", value) { }
    }

    public class ReplyKeyboardLocationItem : ReplyKeyboardItem
    {
        public ReplyKeyboardLocationItem(string value) : base("$location", value) { }
    }
}
