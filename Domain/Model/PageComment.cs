using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain {
    public class PageComment : BaseEntity {
        [Display(Name = "آیدی خبر")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [ForeignKey("BlogNews")]
        public int PageId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(1000,ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(100,ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل معتبر وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "سایت")]
        [MaxLength(100,ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        public string Website { get; set; }

        [Display(Name = "کامنت")]
        [MaxLength(500,ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        public string Comment { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime Time { get; set; }

        public virtual BlogNew GetBlogNew { get; set; }
        public PageComment() {

        }
    }
}
