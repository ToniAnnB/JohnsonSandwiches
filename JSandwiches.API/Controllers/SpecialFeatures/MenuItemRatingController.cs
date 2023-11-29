using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.SpecialFeaturesDTO;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.SpecialFeatures
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemRatingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MenuItemRatingController(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<IActionResult> GetMenuItemRatings()
        {
            var menuItemRatings = await _unitOfWork.MenuItemRating.GetAll(null, null, null);

            if (menuItemRatings == null)
                return NotFound();

            var result = _mapper.Map<IList<MenuItemRatingDTO>>(menuItemRatings);
            return Ok(result);
        }



        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "5minsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMenuItemRating(int id)
        {
            if (id < 1)
                return BadRequest();

            var menuItemRating = await _unitOfWork.MenuItemRating.Get(q => q.Id == id, null);

            if (menuItemRating == null)
                return NotFound();

            var result = _mapper.Map<MenuItemRatingDTO>(menuItemRating);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMenuItemRating([FromBody] CreateMenuItemRatingDTO menuItemRating)
        {
            if (menuItemRating == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<MenuItemRating>(menuItemRating);
                if (_unitOfWork.MenuItemRating.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditMenuItemRating([FromBody] MenuItemRatingDTO menuItemRating)
        {
            if (menuItemRating == null || menuItemRating.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<MenuItemRating>(menuItemRating);
                _unitOfWork.MenuItemRating.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMenuItemRating(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.MenuItemRating.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
