using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.SpecialFeaturesDTO;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.SpecialFeatures
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLoyaltyPointController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerLoyaltyPointController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerLoyaltyPoints()
        {
            var customerLoyaltyPoints = await _unitOfWork.CustomerLoyaltyPoint.GetAll(null, null, null);

            if (customerLoyaltyPoints == null)
                return NotFound();

            var result = _mapper.Map<IList<CustomerLoyaltyPointDTO>>(customerLoyaltyPoints);
            return Ok(result);
        }



        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerLoyaltyPoint(int id)
        {
            if (id < 1)
                return BadRequest();

            var customerLoyaltyPoint = await _unitOfWork.CustomerLoyaltyPoint.Get(q => q.Id == id, null);

            if (customerLoyaltyPoint == null)
                return NotFound();

            var result = _mapper.Map<CustomerLoyaltyPointDTO>(customerLoyaltyPoint);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomerLoyaltyPoint([FromBody] CreateCustomerLoyaltyPointDTO customerLoyaltyPoint)
        {
            if (customerLoyaltyPoint == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<CustomerLoyaltyPoint>(customerLoyaltyPoint);
                if (_unitOfWork.CustomerLoyaltyPoint.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCustomerLoyaltyPoint([FromBody] CustomerLoyaltyPointDTO customerLoyaltyPoint)
        {
            if (customerLoyaltyPoint == null || customerLoyaltyPoint.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<CustomerLoyaltyPoint>(customerLoyaltyPoint);
                _unitOfWork.CustomerLoyaltyPoint.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerLoyaltyPoint(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.CustomerLoyaltyPoint.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
