namespace HavenHotel.Services.Guests
{
    public interface IPromptForGuestId
    {
        int GetValidGuestId(string headerText);
    }
}