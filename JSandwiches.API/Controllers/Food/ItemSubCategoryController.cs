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
    public class ItemSubCategoryController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ItemSubCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubCategories()
        {
            var subCategories = await _unitOfWork.ItemSubCategory.GetAll(null, null, null);

            if (subCategories == null)
                return NotFound();

            var result = _mapper.Map<IList<ItemSubCategoryDTO>>(subCategories);
            return Ok(result);
        }



        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubCategory(int id)
        {
            if (id < 1)
                return BadRequest();

            var subCategory = await _unitOfWork.ItemSubCategory.Get(q => q.Id == id, null);

            if (subCategory == null)
                return NotFound();

            var result = _mapper.Map<ItemSubCategoryDTO>(subCategory);
            return Ok(result);
        }



        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSubCategory([FromBody] CreateItemSubCategoryDTO subCategory)
        {
            if (subCategory == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<ItemSubCategory>(subCategory);
                if (_unitOfWork.ItemSubCategory.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditSubCategory([FromBody] ItemSubCategoryDTO subCategory)
        {
            if (subCategory == null || subCategory.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<ItemSubCategory>(subCategory);
                _unitOfWork.ItemSubCategory.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.ItemSubCategory.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }
    }


}
