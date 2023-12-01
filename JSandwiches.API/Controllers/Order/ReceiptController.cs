using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.Models.Order;
using JSandwiches.Models.OrderDTO;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReceiptController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReceipts()
        {
            var receipts = await _unitOfWork.Receipt.GetAll(null, null, null);

            if (receipts == null)
                return NotFound();

            var result = _mapper.Map<IList<ReceiptDTO>>(receipts);
            return Ok(result);
        }



        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReceipt(int id)
        {
            if (id < 1)
                return BadRequest();

            var receipt = await _unitOfWork.Receipt.Get(q => q.Id == id, null);

            if (receipt == null)
                return NotFound();

            var result = _mapper.Map<ReceiptDTO>(receipt);
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateReceipt([FromBody] CreateReceiptDTO receipt)
        {
            if (receipt == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Receipt>(receipt);
                if (_unitOfWork.Receipt.Insert(result).IsCompletedSuccessfully)
                    await _unitOfWork.Save();
                return Created("api/[controller]", result);
            }
            return BadRequest();
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditReceipt([FromBody] ReceiptDTO receipt)
        {
            if (receipt == null || receipt.Id < 1)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Receipt>(receipt);
                _unitOfWork.Receipt.Update(result);
                await _unitOfWork.Save();
                return Ok(result);
            }
            return BadRequest();
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteReceipt(int id)
        {
            if (id < 1)
                return BadRequest();

            await _unitOfWork.Receipt.Delete(id);
            await _unitOfWork.Save();
            return NoContent();

        }

    }
}
