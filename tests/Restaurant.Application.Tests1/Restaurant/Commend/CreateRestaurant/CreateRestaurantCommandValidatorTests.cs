using Xunit;
using Restaurant.Application.Restaurant.Commend.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace Restaurant.Application.Restaurant.Commend.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            var command = new CreateRestaurantCommand()
            {
                Name = "test",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345",
                Description = "djs"

            };

            var validator = new CreateRestaurantCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();


        }

        [Fact()]
        public void validator_FoInValidCommand_ShouldNotHaveValidationErrors()
        {
            var command = new CreateRestaurantCommand()
            {
                Name = "te",
                Category = "It",
                ContactEmail = "@test.com",
                PostalCode = "12345",
                Description = "djs"

            };

            var validator = new CreateRestaurantCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);

        }

        [Theory]
        [InlineData("Italian")]
        [InlineData("English")]
        [InlineData("Japanese")]
        [InlineData("American")]
        [InlineData("Mexican")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            // arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand { Category = category };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }

        [Theory()]
        [InlineData("10220")]
        [InlineData("102-20")]
        [InlineData("10 220")]
        [InlineData("10-2 20")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCod(string postalcode)
        {
            var validate = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand { PostalCode = postalcode };

            var result = validate.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c=>c.PostalCode);
        }
    }
}