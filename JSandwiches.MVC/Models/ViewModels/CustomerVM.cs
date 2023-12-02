using JSandwiches.Models.DTO.UsersDTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSandwiches.MVC.Models.ViewModels
{
    public class CustomerVM
    {
        public CustomerDTO Customer { get; set; }
        public AddressDTO Address { get; set; }
        public CustomerAddressDTO CustomerAddress { get; set; }
        public IEnumerable<SelectListItem> ddlParishes { get; set; }
    }
}
