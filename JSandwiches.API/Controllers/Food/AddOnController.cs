using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.Food;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.Food
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddOnController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddOnController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
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



        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAddOn([FromBody] CreateAddOnDTO addOn)
        {
            if (addOn == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<AddOn>(addOn);
                if (_unitOfWork.AddOn.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditAddOn([FromBody] AddOnDTO addOn)
        {
            if (addOn == null || addOn.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<AddOn>(addOn);
                _unitOfWork.AddOn.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAddOn(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.AddOn.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
