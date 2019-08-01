using GapLib.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GapLib.Model
{
    public class InlineKeyboard : List<List<InlineKeyboardItem>>
    {
        //public List<InlineKeyboardItem> inline_keyboard { get; set; }

        public void AddRow(IEnumerable<InlineKeyboardItem> row)
        {
            Add(row.ToList());
        }
    }

    [JsonConverter(typeof(InlineKeyboardItemConverter))]
    public class InlineKeyboardItem
    {
        public string Text { get; set; }
        public string Cb_data { get; set; }
        public string Url { get; set; }
        public OpenMode? Open_in { get; set; }
        public int? Amount { get; set; }
        public Currency? Currency { get; set; }
        public string ref_id { get; set; }
        public string desc { get; set; }

        public InlineKeyboardItem() { }

        public static InlineKeyboardItem Simple(string text, string callbackData, string description = null)
        {
            InlineKeyboardItem keyboard = new InlineKeyboardItem()
            {
                Text = text,
                Cb_data = callbackData,
                desc = description
            };


            return keyboard;
        }

        public static InlineKeyboardItem Payment(string buttonText, int amount, string refId, string description, Currency currency = Model.Currency.IRR)
        {
            InlineKeyboardItem keyboard = new InlineKeyboardItem
            {
                Amount = amount,
                Currency = currency,
                ref_id = refId,
                desc = description,
                Text = buttonText,
            };

            return keyboard;
        }

        public static InlineKeyboardItem OpenUrl(string buttonText, string url, OpenMode openMode = OpenMode.webview, string description = null, string cb_dataTrigger = null)
        {
            InlineKeyboardItem keyboard = new InlineKeyboardItem
            {
                Url = url,
                Open_in = openMode,
                desc = description,
                Text = buttonText,
                Cb_data = cb_dataTrigger
            };

            return keyboard;
        }
    }

    public enum OpenMode
    {
        webview_full,
        inline_browser,
        webview_with_heade,
        webview,
        browser
    }


    public enum Currency
    {
        IRR,
        Coin,
        USD
    }
}
