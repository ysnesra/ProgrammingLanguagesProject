namespace Core.Application.Requests;

public class PageRequest
{
    public int Page { get; set; }    //Kaçıncı sayfa
    public int PageSize { get; set; }   //Bir sayfada kaç data 
}