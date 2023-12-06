using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.DTO.OrderDTO;

namespace JSandwiches.MVC.Models.ViewModels
{
    public class OrderVM
    {
        public OrderDTO Order { get; set; }

        public List<AddOnCheckBox> LstAddOnsCheckBox { get; set; }
        public List<string> SelectedAddOns {  get; set; }

        public List<MenuItemDTO> LstMenuItem { get; set; }
    }
}
