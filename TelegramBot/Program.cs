
using TelegramBot;
public class Program
{
    private static void Main(string[] args)
    {
        bool running = true;
        var bot = new TelegramBot.TelegramBot();
        Task.Run(() => bot.CommandListenerAsync());
        while (running)
        {

            PrintMenu();
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value))
            {
                switch (value)
                {
                    case 0:
                        Console.WriteLine("Lukker programmet...");
                        running = false;
                        break;
                    case 1:
                        string inputMessage  = Console.ReadLine();
                        bot.SendMessageAsync( "7091701318",inputMessage);
                        break;

                    default:
                        Console.WriteLine("Ugyldigt valg prøv igen!");
                        break;
                }
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("Vælg en funktion fra menuen");
            Console.WriteLine("0 - Afslut Program");
        }
    }
}