using JSandwiches.Models.DTO.UsersDTO;
using JSandwiches.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JSandwiches.MVC.Models.ViewModels
{
    public class CustomerVM2
    {
            public CustomerDTO Customer { get; set; }
            public AddressDTO Address { get; set; }
            public CustomerAddressDTO CustomerAddress { get; set; }
            public IEnumerable<SelectListItem> ddlParishes { get; set; }
            public ApplicationUser ApplicationUser { get; set; }
    }
}
