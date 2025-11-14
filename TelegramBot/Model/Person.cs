namespace TelegramBot.Model;
public class Person
{
    public string? ChatId { get; set;}
    public string? FirstName { get; set;}
    public string? LastName { get; set;}
   // public string? UserName { get; set;}

    public Person(string? ChatId, string? FirstName, string? LastName) // , string? UserName)
    {
        this.ChatId = ChatId;
        this.FirstName = FirstName;
        this.LastName = LastName;
      //  this.UserName = UserName;
    }

    public override string ToString()
    {
      return $"ChatId: {ChatId}, FirstName: {FirstName}, LastName: {LastName}"; //, UserName: {UserName}";   
    }
}