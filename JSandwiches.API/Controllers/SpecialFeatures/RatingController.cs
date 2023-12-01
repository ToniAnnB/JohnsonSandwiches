using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.SpecialFeaturesDTO;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.SpecialFeatures
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRatings()
        {
            var ratings = await _unitOfWork.Rating.GetAll(null, null, null);

            if (ratings == null)
                return NotFound();

            var result = _mapper.Map<IList<RatingDTO>>(ratings);
            return Ok(result);
        }



        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRating(int id)
        {
            if (id < 1)
                return BadRequest();

            var rating = await _unitOfWork.Rating.Get(q => q.Id == id, null);

            if (rating == null)
                return NotFound();

            var result = _mapper.Map<RatingDTO>(rating);
            return Ok(result);
        }

    }
}
