using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain {
    public class Category : BaseEntity {

        [Display(Name = "نام دسته")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(100,ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        public string Name { get; set; }

        [ForeignKey("Parent")]
        public Guid? ParentId { get; set; }

        [Display(Name = "آیکون")]
        [MaxLength(100,ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        public string Icon { get; set; }

        public virtual Category Parent { get; set; }
        public Category() {

        }
        //Navigation Property 
        public virtual List<BlogNew> BlogNews { get; set; }
    }
}
