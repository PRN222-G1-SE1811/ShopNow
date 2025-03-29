// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Identity.Models.ManageViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessage = "Must input {0}")]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Must input {0}")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
