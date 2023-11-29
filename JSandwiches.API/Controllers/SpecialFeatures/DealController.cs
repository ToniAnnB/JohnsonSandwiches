using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.SpecialFeaturesDTO;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.SpecialFeatures
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DealController(IUnitOfWork unitOfWork, IMapper mapper)
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
        public async Task<IActionResult> GetDeals()
        {
            var deals = await _unitOfWork.Deal.GetAll(null, null, null);

            if (deals == null)
                return NotFound();

            var result = _mapper.Map<IList<DealDTO>>(deals);
            return Ok(result);
        }



        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "5minsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDeal(int id)
        {
            if (id < 1)
                return BadRequest();

            var deal = await _unitOfWork.Deal.Get(q => q.Id == id, null);

            if (deal == null)
                return NotFound();

            var result = _mapper.Map<DealDTO>(deal);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDeal([FromBody] CreateDealDTO deal)
        {
            if (deal == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Deal>(deal);
                if (_unitOfWork.Deal.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditDeal([FromBody] DealDTO deal)
        {
            if (deal == null || deal.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Deal>(deal);
                _unitOfWork.Deal.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDeal(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.Deal.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
