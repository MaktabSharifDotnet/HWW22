using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HWW22.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "دسته‌بندی")]
        public string? CategoryName { get; set; }


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        
        public IFormFile? Image { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1000, 2000000000, ErrorMessage = "قیمت باید بین ۱۰۰۰ تا ۲ میلیارد تومان باشد")]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "موجودی انبار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, 10000, ErrorMessage = "موجودی باید عددی مثبت باشد")]
        public int Inventory { get; set; }


        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }
    }
}
