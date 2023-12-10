using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.DTO.SpecialFeaturesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JSandwiches.API.Controllers.SpecialFeatures
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyPointController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoyaltyPointController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoyaltyPoints()
        {
            var loyaltyPoints = await _unitOfWork.LoyaltyPoint.GetAll(null, null, null);

            if (loyaltyPoints == null)
                return NotFound();

            var result = _mapper.Map<IList<LoyaltyPointDTO>>(loyaltyPoints);
            return Ok(result);
        }



        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoyaltyPoint(int id)
        {
            if (id < 1)
                return BadRequest();

            var loyaltyPoint = await _unitOfWork.LoyaltyPoint.Get(q => q.Id == id, null);

            if (loyaltyPoint == null)
                return NotFound();

            var result = _mapper.Map<LoyaltyPointDTO>(loyaltyPoint);
            return Ok(result);
        }



        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLoyaltyPoint([FromBody] CreateLoyaltyPointDTO loyaltyPoint)
        {
            if (loyaltyPoint == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<LoyaltyPoint>(loyaltyPoint);
                if (_unitOfWork.LoyaltyPoint.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditLoyaltyPoint([FromBody] LoyaltyPointDTO loyaltyPoint)
        {
            if (loyaltyPoint == null || loyaltyPoint.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<LoyaltyPoint>(loyaltyPoint);
                _unitOfWork.LoyaltyPoint.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLoyaltyPoint(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.LoyaltyPoint.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
