using ServiceLayer.CustomException;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs.DashBoard
{
    public class DashBoardDto
    {
        [Required(ErrorMessage = "Year is required.")]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "DateStart is required.")]
        [DataType(DataType.Date, ErrorMessage = "DateStart must be a valid date.")]
        public string DateStart { get; set; }

        [Required(ErrorMessage = "DateEnd is required.")]
        [DataType(DataType.Date, ErrorMessage = "DateEnd must be a valid date.")]
        public string DateEnd { get; set; }
    }
}
