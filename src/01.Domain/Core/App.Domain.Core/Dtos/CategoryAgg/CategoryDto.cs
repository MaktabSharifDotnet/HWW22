using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.CategoryAgg
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Display(Name="نام دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display (Name="توضیحات کوتاه")]
        [Required (ErrorMessage = "لطفا{0} کامل کنید")]
        public string Description { get; set; }
    }
}
