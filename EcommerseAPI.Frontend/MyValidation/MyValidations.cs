using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerseAPI.Frontend.MyValidation
{
    static class MyValidations
    {
        public static async Task<bool> Validate<TDto>(TDto dto)
        {
            if (dto == null)
                return false;

            var context = new ValidationContext(dto);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            if (!Validator.TryValidateObject(dto, context, results, true))
            {
                foreach (var error in results)
                {
                    AnsiConsole.MarkupLine($"[red]Error: {error.ErrorMessage}[/]");
                }
                return false;
            }
            else return true;
        }
    }
}
