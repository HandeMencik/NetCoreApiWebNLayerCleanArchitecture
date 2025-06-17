namespace Repositories.Products;
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }=default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }  
}
