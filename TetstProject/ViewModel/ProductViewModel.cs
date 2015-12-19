using System.ComponentModel.DataAnnotations;

namespace TetstProject.ViewModel
{
   public class ProductViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [RegularExpression(@"\d+$", ErrorMessage = "Испульзуйте только цыфры")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [RegularExpression(@"\-?\d+(\,\d{0,})?", ErrorMessage = "Испульзуйте только цыфры.Разделитель-\",\" ")]
        public string Price { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено! Создайте сначала склад!")]
        public string StoreName { get; set; }
    }
}

