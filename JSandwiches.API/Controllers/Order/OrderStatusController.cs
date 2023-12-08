using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.DTO.OrderDTO;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.Food
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderStatusController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderStatus()
        {
            var orderStatus = await _unitOfWork.OrderStatus.GetAll(null, null, null);

            if (orderStatus == null)
                return NotFound();

            var result = _mapper.Map<IList<OrderStatusDTO>>(orderStatus);
            return Ok(result);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderStatus(int id)
        {
            if (id < 1)
                return BadRequest();

            var OrderStatus = await _unitOfWork.OrderStatus.Get(q => q.Id == id, null);

            if (OrderStatus == null)
                return NotFound();

            var result = _mapper.Map<OrderStatusDTO>(OrderStatus);
            return Ok(result);
        }

    }
}
