using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features.Authentication;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using System.Text;

namespace ModelLayer {
    public  class LoginViewModel {
        [Display(Name = "نام کاربری")]
        [MaxLength(200, ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل ")]
        [MaxLength(200, ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "پسورد ")]
        [MaxLength(200, ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public IEnumerable<AuthenticationScheme> ExternalLogin { get; set; }

    }
}
