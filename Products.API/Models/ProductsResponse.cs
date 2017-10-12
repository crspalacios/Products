
namespace Products.API.Models
{
    using System;

    public class ProductsResponse
    {
        public int ProductId{ get; set; }


        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActivte { get; set; }

        public double Stock { get; set; }


        public DateTime LastEvent { get; set; }


        public string Remarks { get; set; }


        public string Image { get; set; }
    }
}