using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.DTO.OrderDTO;
using JSandwiches.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private List<string> includes = new List<string>()
            {
                "Order"
            };
        public PaymentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _unitOfWork.Payment.GetAll(null, null, null);

            if (payments == null)
                return NotFound();

            var result = _mapper.Map<IList<PaymentDTO>>(payments);
            return Ok(result);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPayment(int id)
        {
            if (id < 1)
                return BadRequest();

            var payment = await _unitOfWork.Payment.Get(q => q.Id == id, null);

            if (payment == null)
                return NotFound();

            var result = _mapper.Map<PaymentDTO>(payment);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDTO payment)
        {
            if (payment == null)
                return BadRequest();


            var result = _mapper.Map<Payment>(payment);
            if (_unitOfWork.Payment.Insert(result).IsCompletedSuccessfully)
                await _unitOfWork.Save();
            return Created("api/[controller]", result);

        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditPayment([FromBody] PaymentDTO payment)
        {
            if (payment == null || payment.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Payment>(payment);
                _unitOfWork.Payment.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.Payment.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }
    }
}
