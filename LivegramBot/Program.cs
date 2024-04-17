using LivegramBot.Entities;
using LivegramBot.Services;
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

        public static void OnMessage(object? sender, MessageEventArgs e)
        {
            UserService service = new UserService();
            
            string text = e.Message.Text;

            long chatId = 2017110018;

            if (text == "/start")
            {
                _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Assalomu aleykum !\r\n\r\nBo't bo'yicha qanday savol taklif yoki shikoyatingiz bo'lsa bizga bemalol yo'llashingiz mumkin");

                User user = new User();
                user.ChatId = e.Message.Chat.Id;
                user.UserName = e.Message.Chat.Username;
                user.FirstName = e.Message.Chat.FirstName;
                user.LastName = e.Message.Chat.LastName;
                int response = service.AddUserAsync(user);

                Console.WriteLine(response);
            }
            else if (e.Message.Chat.Id != chatId)
            {
                _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Sizning habaringiz muvaffaqiyatli yuborildi");
                _botClient.ForwardMessageAsync(chatId, e.Message.Chat.Id, e.Message.MessageId);
                _botClient.ForwardMessageAsync(chatId, e.Message.Chat.Id, e.Message.MessageId);
            }
            else
            {
                var user =  service.FindUser(e.Message.ReplyToMessage.ForwardFrom.Id);
                //_botClient.SendTextMessageAsync(Id.ChatId, e.Message.Text);
                _botClient.SendTextMessageAsync(user.ChatId, e.Message.Text);
            }
            //else if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.ForwardFrom != null)
            //{
            //}
        }
    }
}
