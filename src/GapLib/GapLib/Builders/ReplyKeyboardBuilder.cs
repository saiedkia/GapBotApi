using GapLib.Model;
using System.Collections.Generic;

namespace GapLib.Builders
{
    public class ReplyKeyboardBuilder
    {
        ReplyKeyboard keyboard;
        List<ReplyKeyboardItem> rowItems;

        public ReplyKeyboardBuilder()
        {
            keyboard = new ReplyKeyboard();
            rowItems = new List<ReplyKeyboardItem>();
        }


        //public ReplyKeyboardBuilder Create()
        //{
        //    return new ReplyKeyboardBuilder();
        //}

        public ReplyKeyboardBuilder AddRow()
        {
            if (rowItems?.Count > 0)
                keyboard.AddRow(rowItems);

            rowItems = new List<ReplyKeyboardItem>();

            return this;
        }

        public ReplyKeyboardBuilder AddGetContact(string label)
        {
            ReplyKeyboardContactItem item = new ReplyKeyboardContactItem(label);
            rowItems.Add(item);

            return this;
        }

        public ReplyKeyboardBuilder AddGetLocation(string label)
        {
            ReplyKeyboardLocationItem item = new ReplyKeyboardLocationItem(label);
            rowItems.Add(item);

            return this;
        }

        public ReplyKeyboardBuilder Add(string key, string value)
        {
            ReplyKeyboardItem item = new ReplyKeyboardItem(key, value);
            rowItems.Add(item);

            return this;
        }

        public ReplyKeyboardBuilder Add(string keyOrValue)
        {
            ReplyKeyboardItem item = new ReplyKeyboardItem(keyOrValue);
            rowItems.Add(item);

            return this;
        }


        public ReplyKeyboard Build()
        {
            AddRow();
            return keyboard;
        }
    }
}