namespace TelegramBotWeb.Models
{
    public class Person
    {
        private long _chatId;
        private string _firstName;
        private string _lastName;
        private string _userName;

        public Person(long chatId, string firstName, string lastName, string userName)
        {
            _chatId = chatId;
            _firstName = firstName;
            _lastName = lastName;
            _userName = userName;

        }
        public Person()
        {
        }
    }
}
