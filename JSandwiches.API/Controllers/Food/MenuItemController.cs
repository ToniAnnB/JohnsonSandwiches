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
    public class MenuItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private List<string> includes = new List<string>()
        {
            "SubCategory",
            "SubCategory.Category"
        };
        public MenuItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems = await _unitOfWork.MenuItem.GetAll(null, null, includes);

            if (menuItems == null)
                return NotFound();

            var result = _mapper.Map<IList<MenuItemDTO>>(menuItems);
            return Ok(result);
        }



        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMenuItem(int id)
        {
            if (id < 1)
                return BadRequest();

            var menuItem = await _unitOfWork.MenuItem.Get(q => q.Id == id, includes);

            if (menuItem == null)
                return NotFound();

            var result = _mapper.Map<MenuItemDTO>(menuItem);
            return Ok(result);
        }



        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMenuItem([FromBody] CreateMenuItemDTO menuItem)
        {
            if (menuItem == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<MenuItem>(menuItem);
                if (_unitOfWork.MenuItem.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditMenuItem([FromBody] MenuItemDTO menuItem)
        {
            if (menuItem == null || menuItem.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<MenuItem>(menuItem);
                _unitOfWork.MenuItem.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.MenuItem.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
