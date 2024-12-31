namespace Remembvoc.ApplicationCore.Common.Models;

public class Page
{
    public int CurrentPageNumber { get; set; } = 1;
    public int ElementsPerPage { get; set; } = 11;
    public int TotalWordsAmount { get; set; }
    public int LastPage => (int)Math.Ceiling(TotalWordsAmount / (double)ElementsPerPage);
    public bool IsPlusPageButtonEnabled { get; set; }
    public bool IsMinusPageButtonEnabled { get; set; }
    public bool IsVisible { get; set; }
}