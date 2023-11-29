using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.UsersDTO;

namespace JSandwiches.MVC.IRespository
{
    public interface IConsumUnitOfWork
    {
        IGenConsumRespo<AddressDTO> Address { get; }
        IGenConsumRespo<AddOnDTO> AddOn { get; }

    }
}
