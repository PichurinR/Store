using System.ComponentModel.DataAnnotations;


namespace TetstProject.ViewModel
{
    public class StoreViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Name { get; set; }
    }
}