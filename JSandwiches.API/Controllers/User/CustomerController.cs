using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.DTO.UsersDTO;
using JSandwiches.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _unitOfWork.Customer.GetAll(null, null, null);

            if (customers == null)
                return NotFound();

            var result = _mapper.Map<IList<CustomerDTO>>(customers);
            return Ok(result);
        }



        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            if (id < 1)
                return BadRequest();

            var customer = await _unitOfWork.Customer.Get(q => q.Id == id, null);

            if (customer == null)
                return NotFound();

            var result = _mapper.Map<CustomerDTO>(customer);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTO customer)
        {
            if (customer == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Customer>(customer);
                if (_unitOfWork.Customer.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditCustomer([FromBody] CustomerDTO customer)
        {
            if (customer == null || customer.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Customer>(customer);
                _unitOfWork.Customer.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.Customer.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
