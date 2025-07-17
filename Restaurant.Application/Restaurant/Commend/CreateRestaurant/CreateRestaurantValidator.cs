using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.Restaurant.DTOS;
using FluentValidation;

namespace Restaurant.Application.Restaurant.Commend.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> ValidCategories = ["Italian","Enlgish","Japanese","Mexican","Amarican"];
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
             .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(dto => dto.Category).Must(ValidCategories.Contains).WithMessage("Invalid Category , Please Choose From valid Categories");
            /* .Custom((value, context) =>
             {
                 var IsValidCategories = ValidCategories.Contains(value);
                 if (!IsValidCategories) 
                 {
                     context.AddFailure(");
                 }

             }

             );*/

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please provide a valid postal code (XX-XXX).");
        }
    }
}
