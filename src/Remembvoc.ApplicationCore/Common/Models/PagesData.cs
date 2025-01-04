namespace Remembvoc.ApplicationCore.Common.Models;

public class PagesData
{
    public Page MainPage { get; set; } = new();
    public Page TranslationPage { get; set; } = new();
    public Page CurrentPage { get; set; }
}