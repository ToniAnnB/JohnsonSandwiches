using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.DTO.FoodDTO;
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
        [ResponseCache(CacheProfileName = "5minsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddOns()
        {
            var addOns = await _unitOfWork.AddOn.GetAll(null, null, null);

            if (addOns == null)
                return NotFound();

            var result = _mapper.Map<IList<AddOnDTO>>(addOns);
            return Ok(result);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddOn(int id)
        {
            if (id < 1)
                return BadRequest();

            var addOn = await _unitOfWork.AddOn.Get(q => q.Id == id, null);

            if (addOn == null)
                return NotFound();

            var result = _mapper.Map<AddOnDTO>(addOn);
            return Ok(result);
        }

    }
}
