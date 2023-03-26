using ContactManager.Controllers;
using ContactManager.Interfaces;
using ContactManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace TestContactsWebAPI
{
    public class ContactControllerTest
    {
        private readonly ContactController _contactController;
        private readonly DefaultHttpContext _context;
        private readonly IRepositoryManager _repositoryManager;

        public ContactControllerTest()
        {
            _repositoryManager = new RepositoryManagerFake();
            _contactController = new ContactController(_repositoryManager);

            var userFake = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", "D17592DC-EF09-4962-F366-08DB2B10FCA3"),
            }, "fake"));

            _context = new DefaultHttpContext();

            _contactController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = userFake }
            };
        }

        [Fact]
        public void Get_WhenCalledReturnsStatus200OKTwoContactsList()
        {
            // Act
            var result = _contactController.Get() as ObjectResult;
            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsType<List<ContactDTO>>(result.Value);
            Assert.Equal(2,(result.Value as List<ContactDTO>).Count);
        }

        [Fact]
        public void GetByExistingContactIdReturnsStatus200OKContactDTO()
        {
            // Arrange
            var ExistingContactId = new Guid("3AC4CCE1-48D2-41D3-E7C1-08DB2B9CBBE8");
            // Act
            var result = _contactController.Get(ExistingContactId) as ObjectResult;
            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsType<ContactDTO>(result.Value);
        }

        [Fact]
        public void GetByContactIdOtherOwnerReturnsStatus404NotFound()
        {
            // Arrange
            var ContactIdFromOtherOwner = new Guid("EED907EF-AA49-4EE5-380D-08DB2BE0175D");
            // Act
            var result = _contactController.Get(ContactIdFromOtherOwner) as StatusCodeResult;
            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public void GetByContactIdNonExistentStatus404NotFound()
        {
            // Arrange
            Guid nonExitentContactGuid = new Guid("38F115A1-4EAD-4480-9E12-A084EF1D5F72");
            // Act
            var result = _contactController.Get(nonExitentContactGuid) as StatusCodeResult;
            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public void Add_ContactInvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Contact()
            {
                FirstName = "Missing"
            };
            _contactController.ModelState.AddModelError("Name", "Required");
            // Act
            var badResponse = _contactController.Post(nameMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}
