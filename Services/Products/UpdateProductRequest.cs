namespace Services.Products;

public record UpdateProductRequest(int ProductId, string Name, decimal Price, int Stock);
