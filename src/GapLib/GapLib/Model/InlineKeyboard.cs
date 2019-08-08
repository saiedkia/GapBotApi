using GapLib.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GapLib.Model
{
    public class InlineKeyboard : List<List<InlineKeyboardItem>>
    {
        public void AddRow(IEnumerable<InlineKeyboardItem> row)
        {
            Add(row.ToList());
        }
    }

    [JsonConverter(typeof(InlineKeyboardItemConverter))]
    public class InlineKeyboardItem
    {
        public string Text { get; set; }
        public string CbData { get; set; }
        public string Url { get; set; }
        public OpenMode? OpenIn { get; set; }
        public int? Amount { get; set; }
        public Currency? Currency { get; set; }
        public string RefId { get; set; }
        public string Desc { get; set; }

        public InlineKeyboardItem() { }

        public static InlineKeyboardItem Simple(string text, string callbackData, string description = null)
        {
            InlineKeyboardItem keyboard = new InlineKeyboardItem()
            {
                Text = text,
                CbData = callbackData,
                Desc = description
            };


            return keyboard;
        }

        public static InlineKeyboardItem Payment(string buttonText, int amount, string refId, string description, Currency currency = Model.Currency.IRR)
        {
            InlineKeyboardItem keyboard = new InlineKeyboardItem
            {
                Amount = amount,
                Currency = currency,
                RefId = refId,
                Desc = description,
                Text = buttonText,
            };

            return keyboard;
        }

        public static InlineKeyboardItem OpenUrl(string buttonText, string url, OpenMode openMode = OpenMode.Webview, string description = null, string callbackDataTrigger = null)
        {
            InlineKeyboardItem keyboard = new InlineKeyboardItem
            {
                Url = url,
                OpenIn = openMode,
                Desc = description,
                Text = buttonText,
                CbData = callbackDataTrigger
            };

            return keyboard;
        }
    }

    public enum OpenMode
    {
        WebviewFull,
        InlineBrowser,
        WebviewWithHeader,
        Webview,
        Browser
    }



}
