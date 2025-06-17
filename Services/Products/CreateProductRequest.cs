namespace Services.Products;

public record CreateProductRequest(string Name, Decimal Price,int Stock);
