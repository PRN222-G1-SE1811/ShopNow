// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Identity.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Must input {0}")]
        [EmailAddress(ErrorMessage="Wrong format of email")]
        public string Email { get; set; }
    }
}
