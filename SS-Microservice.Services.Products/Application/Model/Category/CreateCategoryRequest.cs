﻿namespace SS_Microservice.Services.Products.Application.Model.Category
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string ParentId { get; set; }
    }
}