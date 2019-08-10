namespace GapLib.Builders
{
    public static class KeyboardBuilder
    {
        public static InlineKeyboardBuilder InlineKeyboard()
        {
            return new InlineKeyboardBuilder();
        }

        public static ReplyKeyboardBuilder ReplyKeyboardBuilder()
        {
            return new ReplyKeyboardBuilder();
        }
    }
}

