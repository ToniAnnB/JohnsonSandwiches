using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.DTO.UsersDTO;
using JSandwiches.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private List<string> includes = new List<string>()
        {
            "Parish"
        };

        public AddressController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddress()
        {
            var addresss = await _unitOfWork.Address.GetAll(null, null, includes);

            if (addresss == null)
                return NotFound();

            var result = _mapper.Map<IList<AddressDTO>>(addresss);
            return Ok(result);
        }



        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAddress(int id)
        {
            if (id < 1)
                return BadRequest();

            var address = await _unitOfWork.Address.Get(q => q.Id == id, includes);

            if (address == null)
                return NotFound();

            var result = _mapper.Map<AddressDTO>(address);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDTO address)
        {
            if (address == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Address>(address);
                if (_unitOfWork.Address.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditAddress([FromBody] AddressDTO address)
        {
            if (address == null || address.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Address>(address);
                _unitOfWork.Address.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.Address.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
