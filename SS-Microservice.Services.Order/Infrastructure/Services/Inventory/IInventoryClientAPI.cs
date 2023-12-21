using RestEase;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Services.Order.Infrastructure.Services.Inventory.Model.Request;
using SS_Microservice.Services.Order.Infrastructure.Services.Inventory.Model.Response;

namespace SS_Microservice.Services.Order.Infrastructure.Services.Inventory
{
    public interface IInventoryClientAPI
    {
        [Get("api/inventories/internal/{type}")]
        Task<CustomAPIResponse<List<DocketDto>>> GetDocketByType([Path] string type);

        [Get("api/inventories/internal/date")]
        [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
        Task<CustomAPIResponse<List<List<DocketDto>>>> GetDocketByDate([Query] GetDocketByDateRequest request);
    }
}
