using JSandwiches.API.IRespository;
using JSandwiches.Models.DTO.FoodDTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using JSandwiches.Models.Food;

namespace JSandwiches.API.Controllers.Food
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemAddOnController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MenuItemAddOnController(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<IActionResult> GetMenuItemAddOns()
        {
            var menuItemAddOns = await _unitOfWork.MenuItemAddOn.GetAll(null, null, null);

            if (menuItemAddOns == null)
                return NotFound();

            var result = _mapper.Map<IList<MenuItemAddOnDTO>>(menuItemAddOns);
            return Ok(result);
        }



        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "5minsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMenuItemAddOn(int id)
        {
            if (id < 1)
                return BadRequest();

            var menuItemAddOn = await _unitOfWork.MenuItemAddOn.Get(q => q.Id == id, null);

            if (menuItemAddOn == null)
                return NotFound();

            var result = _mapper.Map<MenuItemAddOnDTO>(menuItemAddOn);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMenuItemAddOn([FromBody] CreateMenuItemAddOnDTO menuItemAddOn)
        {
            if (menuItemAddOn == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<MenuItemAddOn>(menuItemAddOn);
                if (_unitOfWork.MenuItemAddOn.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMenuItemAddOn(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.MenuItemAddOn.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }
    }
}
