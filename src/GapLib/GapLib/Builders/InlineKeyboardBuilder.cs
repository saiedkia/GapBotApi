using GapLib.Model;
using System.Collections.Generic;

namespace GapLib.Builders
{
    public class InlineKeyboardBuilder
    {
        readonly InlineKeyboard keyboard;
        List<InlineKeyboardItem> rowItems;

        //public static InlineKeyboardBuilder Create()
        //{
        //    return new InlineKeyboardBuilder();
        //}

        public InlineKeyboardBuilder()
        {
            keyboard = new InlineKeyboard();
            rowItems = new List<InlineKeyboardItem>();
        }

        public InlineKeyboardBuilder AddRow()
        {
            if (rowItems?.Count > 0)
                keyboard.Add(rowItems);

            rowItems = new List<InlineKeyboardItem>();

            return this;
        }

        public InlineKeyboardBuilder AddPayment(InlineKeyboardItem inlineKeyboardItem)
        {
            AddPayment(inlineKeyboardItem.Text, inlineKeyboardItem.Amount.Value, inlineKeyboardItem.RefId, inlineKeyboardItem.Desc, inlineKeyboardItem.Currency ?? Currency.IRR);
            return this;
        }

        public InlineKeyboardBuilder AddPayment(string buttonText, int amount, string refId, string description, Currency currency = Currency.IRR)
        {
            InlineKeyboardItem item = InlineKeyboardItem.Payment(buttonText, amount, refId, description, currency);
            rowItems.Add(item);

            return this;
        }

        public InlineKeyboardBuilder AddOpenUrl(string buttonText, string url, OpenMode openMode = OpenMode.Webview, string description = null, string callbackDataTrigger = null)
        {

            InlineKeyboardItem item = InlineKeyboardItem.OpenUrl(buttonText, url, openMode, description, callbackDataTrigger);
            rowItems.Add(item);

            return this;
        }

        public InlineKeyboardBuilder AddOpenUrl(InlineKeyboardItem inlineKeyboardItem)
        {
            AddOpenUrl(inlineKeyboardItem.Text, inlineKeyboardItem.Url, inlineKeyboardItem.OpenIn ?? OpenMode.Browser, inlineKeyboardItem.Desc, inlineKeyboardItem.CbData);
            return this;
        }


        public InlineKeyboardBuilder AddButton(string text, string callbackData, string description = null)
        {
            InlineKeyboardItem item = InlineKeyboardItem.Simple(text, callbackData, description);
            rowItems.Add(item);

            return this;
        }

        public InlineKeyboardBuilder AddButton(string text)
        {
            InlineKeyboardItem item = InlineKeyboardItem.Simple(text, text, null);
            rowItems.Add(item);

            return this;
        }


        public InlineKeyboard Build()
        {
            AddRow();
            return keyboard;
        }

    }
}
