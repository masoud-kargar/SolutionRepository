using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain {
    public class BlogNew : BaseEntity {

        [Display(Name = "تیتر خبر")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(500,ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "عکس خبر")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(100,ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        public string ImgName { get; set; }

        [Display(Name = "گروه صفه ")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [ForeignKey("GetCategory")]
        public Guid CategoryId { get; set; }

        [Display(Name = "متراژ خانه ")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public int houseSize { get; set; }

        [Display(Name = "تعداد اتاق خاب")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public byte bedroomNumber { get; set; }

        [Display(Name = "قیکت کل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public int HomeMaxprice { get; set; }


        [Display(Name = "قیکت اجاره ماهانه")]
        public int HomeWages { get; set; }


        [Display(Name = "تعداد سرویس های بهداشتی")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public byte bedWCNumber { get; set; }


        [Display(Name = "تعداد پارکینگ ها")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public byte ParkingNumber { get; set; }


        [Display(Name = "توضیح امکانات")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string Possibilities { get; set; }

        [Display(Name = "توضیح کامل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        public string Description { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [MaxLength(1000,ErrorMessage = "مقدار{0}نباید بیش از{1}کارکتر باشد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [DataType(DataType.MultilineText)]
        public string MinDescription { get; set; }

        [Display(Name = "بازدید ")]
        public int Visit { get; set; }


        [Display(Name = "اسلایدر")]
        public bool ShowSlider { get; set; }

        [Display(Name = "تاریخ")]
        [DisplayFormat(DataFormatString = "0: yyyy/MM/dd")]
        public DateTime Time { get; set; }

        [Display(Name = "دسته بندی")]
        public Category GetCategory { get; set; }
        public virtual List<PageComment> PageComments { get; set; }


    }
}
