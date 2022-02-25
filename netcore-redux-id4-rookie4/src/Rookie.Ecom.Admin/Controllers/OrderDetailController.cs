using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Contracts;
using Rookie.Ecom.Contracts.Constants;
using Rookie.Ecom.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rookie.Ecom.Admin.Controllers
{
    [Route("api/[controller]")]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetailDto>> CreateAsync([FromBody] OrderDetailDto orderDetailDto)
        {
            Ensure.Any.IsNotNull(orderDetailDto, nameof(OrderDetailDto));
            var asset = await _orderDetailService.AddAsync(orderDetailDto);
            return Created(Endpoints.OrderDetail, asset);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] OrderDetailDto orderDetailDto)
        {
            Ensure.Any.IsNotNull(orderDetailDto, nameof(OrderDetailDto));
            await _orderDetailService.UpdateAsync(orderDetailDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssetAsync([FromRoute] Guid id)
        {
            var orderDetailDto = await _orderDetailService.GetByIdAsync(id);
            Ensure.Any.IsNotNull(orderDetailDto, nameof(OrderDetailDto));
            await _orderDetailService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<OrderDetailDto> GetByIdAsync(Guid id)
            => await _orderDetailService.GetByIdAsync(id);

        [HttpGet]
        public async Task<IEnumerable<OrderDetailDto>> GetAsync()
            => await _orderDetailService.GetAllAsync();

        /*[HttpGet("find")]
        public async Task<PagedResponseModel<OrderDetailDto>>
            FindAsync(string name, int page = 1, int limit = 10)
            => await _orderDetailService.PagedQueryAsync(name, page, limit);*/
    }
}
