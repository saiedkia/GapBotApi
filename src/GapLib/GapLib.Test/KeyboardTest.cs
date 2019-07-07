using GapLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GapLib.Test
{
    public class KeyboardTest : BaseTest
    {
        [Fact]
        public void Shold_client_receive_a_message_with_two_inline_button()
        {
            InlineKeyboard inlineKeyboard = new InlineKeyboard();
            inlineKeyboard.AddRow(new List<InlineKeyboardItem>()
            {
                   InlineKeyboardItem.Simple("simple button", "btnSmpTapped"),
                   InlineKeyboardItem.Payment("payment", 5000, "GUID/refId", "give me that..", cb_dataTrigger: "btnPayTapped"),
                   InlineKeyboardItem.OpenUrl("bing", "https://bing.com")
            });

            Message msg = new Message()
            {
                Chat_Id = ChatId,
                Data = "HI!!!",
                InlineKeyboard = inlineKeyboard
            };
            
            GapClient gapClient = new GapClient(Token);
            gapClient.Send(msg).Wait();
        }
    }
}
