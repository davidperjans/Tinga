using Application.Common;
using Application.Features.Users.Commands.RegisterUser;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.AuthTests
{
    [TestFixture]
    public class RegisterUserHandlerTests
    {
        private Mock<IAuthRepository> _mockAuthRepository;
        private Mock<IMapper> _mockMapper;
        private RegisterUserHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockAuthRepository = new Mock<IAuthRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new RegisterUserHandler(_mockAuthRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_PasswordsDoNotMatch_ReturnsFailure()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                Username = "testuser",
                Password = "password123",
                ConfirmPassword = "differentpassword"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Passwords do not match", result.Message);
        }

        [Test]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                Username = "testuser",
                Password = "password123",
                ConfirmPassword = "password123",
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User"
            };

            var user = new User();

            _mockMapper.Setup(m => m.Map<User>(It.IsAny<RegisterUserCommand>())).Returns(user);
            _mockAuthRepository.Setup(r => r.RegisterUserAsync(It.IsAny<User>()))
                               .ReturnsAsync(OperationResult<string>.Success("User registered successfully"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("User registered successfully", result.Data);
            _mockAuthRepository.Verify(r => r.RegisterUserAsync(It.Is<User>(u => u.PasswordHash != null)), Times.Once);
        }
    }
}
