using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public record ProductDTO(int Id, string Name, decimal Price, int Stock);

    //public record ProductDTO
    //{
    //    public int Id { get; init; }
    //    public string Name { get; init; }
    //    public string Description { get; init; }
    //    public decimal Price { get; init; }
    //    public int Stock { get; init; }
    //}
}
