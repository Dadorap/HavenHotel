namespace HavenHotel.Interfaces;

public interface ISharedMenu
{
    void DisplayMenu(string display, List<string> menu, Action option1 = null,
        Action option2 = null, Action option3 = null,
        Action option4 = null, Action option5 = null,
        Action option6 = null, Action option7 = null);
}
