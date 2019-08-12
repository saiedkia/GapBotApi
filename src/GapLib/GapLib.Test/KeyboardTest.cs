using FluentAssertions;
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
        public void Inline_keyboard_serialization_must_be_equal_to_expected()
        {
            InlineKeyboard inlineKeyboard = new InlineKeyboard();
            inlineKeyboard.AddRow(new List<InlineKeyboardItem>()
            {
                   InlineKeyboardItem.Simple("simple button", "btnSmpTapped"),
                   InlineKeyboardItem.Payment("payment", 5000, "GUID/refId", "give me that.."),
                   InlineKeyboardItem.OpenUrl("bing", "https://bing.com")
            });

            string result = Utils.Serialize(inlineKeyboard).Replace(" ", "").ToLower();
            string expected = Utils.ReadFile(JsonsDirectory + "text\\inlineKeyboard.txt").Replace("\n", "").Replace("\r", "").Replace(" ", "").ToLower();

            result.Should().Be(expected);
        }


        [Fact]
        public void Inline_keyboard_serialization_must_be_equal_to_expected_with_keyboard_builder()
        {
            InlineKeyboard inlineKeyboard = InlineKeyboard.Builder().AddButton("simple button", "btnSmpTapped").AddPayment("payment", 5000, "GUID/refId", "give me that..").AddOpenUrl("bing", "https://bing.com").Build();

            string result = Utils.Serialize(inlineKeyboard).Replace(" ", "").ToLower();
            string expected = Utils.ReadFile(JsonsDirectory + "text\\inlineKeyboard.txt").Replace("\n", "").Replace("\r", "").Replace(" ", "").ToLower();

            result.Should().Be(expected);
        }


    }
}
