using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.UsersDTO;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParishController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParishController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetParishs()
        {
            var parishes = await _unitOfWork.Parish.GetAll(null, null, null);

            if (parishes == null)
                return NotFound();

            var result = _mapper.Map<IList<ParishDTO>>(parishes);
            return Ok(result);
        }



        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetParish(int id)
        {
            if (id < 1)
                return BadRequest();

            var parish = await _unitOfWork.Parish.Get(q => q.Id == id, null);

            if (parish == null)
                return NotFound();

            var result = _mapper.Map<ParishDTO>(parish);
            return Ok(result);
        }


    }
}
