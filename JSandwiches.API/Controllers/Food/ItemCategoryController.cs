using JSandwiches.API.IRespository;
using JSandwiches.Models.DTO.FoodDTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using JSandwiches.Models.Food;

namespace JSandwiches.API.Controllers.Food
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ItemCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _unitOfWork.ItemCategory.GetAll(null, null, null);

            if (categories == null)
                return NotFound();

            var result = _mapper.Map<IList<ItemCategoryDTO>>(categories);
            return Ok(result);
        }



        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategory(int id)
        {
            if (id < 1)
                return BadRequest();

            var category = await _unitOfWork.ItemCategory.Get(q => q.Id == id, null);

            if (category == null)
                return NotFound();

            var result = _mapper.Map<ItemCategoryDTO>(category);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateItemCategoryDTO category)
        {
            if (category == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<ItemCategory>(category);
                if (_unitOfWork.ItemCategory.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCategory([FromBody] ItemCategoryDTO category)
        {
            if (category == null || category.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<ItemCategory>(category);
                _unitOfWork.ItemCategory.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.ItemCategory.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }
    }
}
