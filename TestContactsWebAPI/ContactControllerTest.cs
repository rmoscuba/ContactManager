using ContactManager.Controllers;
using ContactManager.Interfaces;
using ContactManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public void Get_WhenCalledReturnTwoContactsList()
        {
            // Act
            var result = _contactController.Get();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.IsType<List<ContactDTO>>(result);
        }
    }
}
