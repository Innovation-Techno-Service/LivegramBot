using Telegram.Bot;
using Telegram.Bot.Args;
namespace LivegramBot
{
    public class Program
    {
        public static ITelegramBotClient _botClient;
        public static void Main(string[] args)
        {
            _botClient = new TelegramBotClient("7086530018:AAFBbs_rua6mjiIYTq6pQsoQMKxnnK_XzHQ");

            _botClient.OnMessage += OnMessage;

            _botClient.StartReceiving();

            Console.ReadLine();
        }
        public static async void OnMessage(object? sender, MessageEventArgs e)
        {
            string text = e.Message.Text;

            long chatId = 2017110018;

            if (text == "/start")
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Assalomu aleykum !\r\n\r\nBo't bo'yicha qanday savol taklif yoki shikoyatingiz bo'lsa bizga bemalol yo'llashingiz mumkin");
            }
            else if (e.Message.Chat.Id != chatId && e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Sizning habaringiz muvaffaqiyatli yuborildi");
                await _botClient.SendTextMessageAsync(chatId, $"{e.Message.Text} | {e.Message.Chat.Id}");
            }
            else if (e.Message.Chat.Id != chatId && e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Voice)
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Sizning ovozli habaringiz muvaffaqiyatli yuborildi");
                await _botClient.SendVoiceAsync(chatId, e.Message.Voice.FileId, caption: $"{e.Message.Text} | {e.Message.Chat.Id}");
            }
            else if (e.Message.Chat.Id != chatId && e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Video)
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Sizning video habaringiz muvaffaqiyatli yuborildi");

                await _botClient.SendVideoAsync(chatId, e.Message.Video.FileId, caption: $"{e.Message.Text} | {e.Message.Chat.Id}");
            }
            else if (e.Message.Chat.Id != chatId && e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Sizning rasmingiz muvaffaqiyatli yuborildi");
                
                var photo = e.Message.Photo[^1];
                var file = await _botClient.GetFileAsync(photo.FileId);

                await _botClient.SendPhotoAsync(chatId, file.FileId, caption: $"{e.Message.Text} | {e.Message.Chat.Id}");
            }
            else if (e.Message.Chat.Id == chatId && e.Message.ReplyToMessage.Text != null)
            {
                string[] data = e.Message.ReplyToMessage.Text.Split("|");

                await _botClient.SendTextMessageAsync(data[1], e.Message.Text);
            }
            else if (e.Message.Chat.Id == chatId && e.Message.ReplyToMessage != null)
            {
                if (e.Message.ReplyToMessage.Video != null || e.Message.ReplyToMessage.Voice != null || e.Message.ReplyToMessage.Photo != null)
                {
                    string[] data = e.Message.ReplyToMessage.Caption.Split("|");

                    await _botClient.SendTextMessageAsync(data[1], e.Message.Text);
                }
            }
        }
    }
}