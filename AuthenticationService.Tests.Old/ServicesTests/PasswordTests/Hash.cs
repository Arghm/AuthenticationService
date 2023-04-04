using System;
using FluentAssertions;
using AuthenticationService.Application.Handlers;
using Xunit;

namespace AuthenticationService.Tests.ServicesTests.PasswordTests
{
    public class Hash
    {
        [Fact]
        public void Hash_Ok()
        {
            var passwordHasher = new PasswordHasher();

            var password = "qwerty12345";

            var result = passwordHasher.Hash(password);
            var isValid = passwordHasher.IsValid(password, result);

            isValid.Should().BeTrue();
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void Hash_ThrowsArgumentException_WhenPasswordHasWhiteSpace()
        {
            var passwordHasher = new PasswordHasher();

            var password = "  ";

            Action result = () => passwordHasher.Hash(password);

            result.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Hash_ThrowsArgumentException_WhenPasswordIsNull()
        {
            var passwordHasher = new PasswordHasher();

            string password = null;

            Action result = () => passwordHasher.Hash(password);

            result.Should().Throw<ArgumentException>();
        }
    }
}
