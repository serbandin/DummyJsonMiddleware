public record Product(int Id,
string Title,
string Description,
int Price,
decimal DiscountPercentage,
decimal Rating,
int Stock,
string Brand,
string Category,
 string Thumbnail,
 List<string> Images);

public record AllProducts(Product[] Products);