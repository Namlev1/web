namespace Library.Models;

public class Book
{
    // needed for english months names
    public System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Genre { get; set; }
}