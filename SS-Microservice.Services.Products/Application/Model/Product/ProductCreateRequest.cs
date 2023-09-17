﻿namespace SS_Microservice.Services.Products.Application.Model.Product
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public int Status { get; set; }
        public string CategoryId { get; set; }
        public IFormFile Image { get; set; }

        public List<IFormFile> SubImages { get; set; }
    }
}