using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Products;

public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{  

    public Task<List<Product>> GetTopPriceProductAsnyc(int count)
    {
        return Context.Products
            .OrderByDescending(p => p.Price)
            .Take(count)
            .ToListAsync();
    }
}
