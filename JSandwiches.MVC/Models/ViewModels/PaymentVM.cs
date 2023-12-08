using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.DTO.OrderDTO;

namespace JSandwiches.MVC.Models.ViewModels
{
    public class PaymentVM
    {
        public PaymentDTO Payment { get; set; }
        public OrderDTO Order { get; set; }
        public List<AddOnDTO> lstAddOns { get; set; }
        public MenuItemDTO MenuItem { get; set; }
    }
}
