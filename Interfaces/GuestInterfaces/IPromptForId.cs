namespace HavenHotel.Interfaces.GuestInterfaces;

public interface IPromptForId
{
    int GetValidId(string headerText, string identifier);
}