using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products;
public record ProductDto(int ProductId, string Name, Decimal Price, int Stock);

