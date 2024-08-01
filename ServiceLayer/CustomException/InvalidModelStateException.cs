using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.CustomException
{
    public class InvalidModelStateException : Exception
    {
        public ModelStateDictionary ModelState { get; }

        public InvalidModelStateException(ModelStateDictionary modelState)
        {
            ModelState = modelState;
        }
    }
}
