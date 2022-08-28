namespace Cache.Models;

public class Product
{
    public Guid Identifier { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }

    public Product(Guid identifier, string name, string price)
    {
        Identifier = identifier;
        Name = name;
        Price = price;
    }
}
