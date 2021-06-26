﻿using FluentValidation.Results;
using System;

namespace DSC.Core.Communication
{
    public class BaseResult
    {
        public ValidationResult ValidationResult { get; set; }
        public Object response { get; set; }

        public BaseResult()
        {
            ValidationResult = new ValidationResult();
            response = null;
        }

    }
}