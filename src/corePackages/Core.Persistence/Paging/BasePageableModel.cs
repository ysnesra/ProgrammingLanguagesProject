namespace Core.Persistence.Paging;

public class BasePageableModel
{
    public int Index { get; set; }    //Kaçıncı sayfadayım
    public int Size { get; set; }     //bir sayfada kaç tane data var
    public int Count { get; set; }     //toplamda kaç tane data var
    public int Pages { get; set; }      //toplamda kaç sayfa var
    public bool HasPrevious { get; set; }   //önceki sayfa var mı
    public bool HasNext { get; set; }       //sonraki sayfa var mı
}