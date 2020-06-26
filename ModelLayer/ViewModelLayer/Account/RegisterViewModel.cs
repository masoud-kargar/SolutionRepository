using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer {
    public class RegisterViewModel {
        [Display(Name = "نام کاربری")]
        [MaxLength(200, ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [Remote("IsUserIsUse","Account",HttpMethod = "POST", AdditionalFields = "__RequestVerificationToken")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل ")]
        [MaxLength(200, ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [EmailAddress]
        [Remote("IsEmailIsUse", "Account", HttpMethod = "POST", AdditionalFields = "__RequestVerificationToken")]
        public string Email { get; set; }

        [Display(Name = "پسورد ")]
        [MaxLength(200, ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار پسورد ")]
        [MaxLength(200, ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [DisplayFormat(DataFormatString = "0: yyyy/MM/dd")]
        public DateTime Time { get; set; }

    }
}
