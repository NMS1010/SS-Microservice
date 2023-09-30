using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Interfaces;

namespace SS_Microservice.Services.Products.Application.Features.Variant.Queries
{
    public class GetVariantByIdQuery : IRequest<VariantDto>
    {
        public string ProductId { get; set; }
        public string VariantId { get; set; }
    }

    public class GetVariantByIdHandler : IRequestHandler<GetVariantByIdQuery, VariantDto>
    {
        private readonly IProductVariantService _service;

        public GetVariantByIdHandler(IProductVariantService service)
        {
            _service = service;
        }

        public async Task<VariantDto> Handle(GetVariantByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetVariantById(request);
        }
    }
}