using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.Users;
using JSandwiches.Models.UsersDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerAddressController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerAddress()
        {
            var customerAddi = await _unitOfWork.CustomerAddress.GetAll(null, null, null);

            if (customerAddi == null)
                return NotFound();

            var result = _mapper.Map<IList<CustomerAddressDTO>>(customerAddi);
            return Ok(result);
        }



        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerAddress(int id)
        {
            if (id < 1)
                return BadRequest();

            var customerAddi = await _unitOfWork.CustomerAddress.Get(q => q.Id == id, null);

            if (customerAddi == null)
                return NotFound();

            var result = _mapper.Map<CustomerAddressDTO>(customerAddi);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomerAddress([FromBody] CreateCustomerAddressDTO customerAddi)
        {
            if (customerAddi == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<CustomerAddress>(customerAddi);
                if (_unitOfWork.CustomerAddress.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCustomerAddress([FromBody] CustomerAddressDTO customerAddi)
        {
            if (customerAddi == null || customerAddi.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<CustomerAddress>(customerAddi);
                _unitOfWork.CustomerAddress.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerAddress(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.CustomerAddress.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
