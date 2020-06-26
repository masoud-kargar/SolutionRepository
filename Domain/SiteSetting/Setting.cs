
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain {
    public class Setting : BaseEntity {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? LastTimeChanged { get; set; }
    }
}
