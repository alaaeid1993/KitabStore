using System;
using System.ComponentModel.DataAnnotations;

namespace Z_Store.Models
{
    public class ErrorViewModel
    {
        [Key]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
