﻿using System.Text.Json.Serialization;

namespace SS_Microservice.Services.Products.Application.Model.Product
{
    public class UpdateProductRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public long? SaleId { get; set; }
        public long BrandId { get; set; }
        public long UnitId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
    }
}