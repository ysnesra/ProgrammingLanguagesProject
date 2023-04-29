namespace Core.Persistence.Repositories;

public class Entity
{
    //Id ; bütün classlarda ortak olduğu için base olarak burda tanımlanır. Oluşturduğumuz classlara Entity clasından kalıtım verilebilir.
    //parametre alan ve almayan constructor oluşturuldu

    public int Id { get; set; }
  
    public Entity()
    {

    }

    //this() -> ortak şeyler gitsin base de çalışsın
    public Entity(int id) : this()
    {
        Id = id;
    }   
}

