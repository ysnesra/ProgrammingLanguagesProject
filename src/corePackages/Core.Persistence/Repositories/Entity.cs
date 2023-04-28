namespace Core.Persistence.Repositories;

public class Entity
{
    //Id ; bütün classlarda ortak olduğu için base olarak burda tanımlanır. Oluşturduğumuz classlara Entity clasından kalıtım verilebilir.
    //parametre alan ve almayan constructor oluşturuldu

    public int Id { get; set; }
    public DateTime CreateDate { get; set; }

    public Entity()
    {

    }

    //this() -> ortak şeyler gitsin base de çalışsın
    public Entity(int id) : this()
    {
        Id = id;
    }
    public Entity(int id, DateTime createDate) : this(id:id)
    {
        CreateDate = createDate;
    }
}

// this(id:id) -> sınıfın özellik değerini ayarlar
// this(id) -> aynı sınıf içindeki constructorı çağırır