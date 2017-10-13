namespace Products.Models
{
    using System;
    public class Product
    {
        public int ProductId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActivte { get; set; }

        public double Stock { get; set; }

        public DateTime LastEvent { get; set; }

        public string Remarks { get; set; }

        public string Image { get; set; }

        public string ImageFullPath
        {
            get
            {
                return string.Format("http://productsbackendpal.azurewebsites.net/{0}", 
                                      Image.Substring(1));
            }
        }

    }
}
