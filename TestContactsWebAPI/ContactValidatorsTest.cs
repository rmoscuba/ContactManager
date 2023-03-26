using ContactManager.Controllers;
using ContactManager.Interfaces;
using ContactManager.Models;
using ContactManager.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace TestContactsWebAPI
{
    public class ContactValidatorsTest
    {
        private readonly Contact _contact;

        public ContactValidatorsTest()
        {
            _contact = new Contact();
        }

            [Fact]
        public void Get_WhenCalledReturnsStatus200OKTwoContactsList()
        {
            // Arrange
            DateTime DateOfBirth15YearsAgo = DateTime.Now.AddYears(-15);

            var minimunAgeAttribute = new MinimunAgeAttribute(
                    minAge: 18, ErrorMessage: "Contact must be 18 years or older"
                );

            // Act
            var actualIsValidResult = minimunAgeAttribute.IsValid(DateOfBirth15YearsAgo);

            // Assert
            Assert.False(actualIsValidResult);
        }

    }
}
