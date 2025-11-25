using FluentAssertions;
using FSI.PayManager.Application.Security;
using Xunit;

namespace FSI.PayManager.UnitTests.Application.Security
{
    public sealed class JwtSettingsTests
    {
        [Fact]
        public void JwtSettings_Should_Instantiate_With_Default_Values()
        {
            // Act
            var settings = new JwtSettings();

            // Assert
            settings.Issuer.Should().BeNullOrEmpty();
            settings.Audience.Should().BeNullOrEmpty();
            settings.Secret.Should().BeNullOrEmpty();
            settings.AccessTokenMinutes.Should().Be(0);
        }

        [Fact]
        public void JwtSettings_Should_Allow_Setting_Properties()
        {
            // Arrange
            var settings = new JwtSettings();

            // Act
            settings.Issuer = "PayManagerAuth";
            settings.Audience = "PayManagerUsers";
            settings.Secret = "SuperSecretKey123";
            settings.AccessTokenMinutes = 120;

            // Assert
            settings.Issuer.Should().Be("PayManagerAuth");
            settings.Audience.Should().Be("PayManagerUsers");
            settings.Secret.Should().Be("SuperSecretKey123");
            settings.AccessTokenMinutes.Should().Be(120);
        }

        [Fact]
        public void JwtSettings_Should_Have_Valid_String_Properties()
        {
            // Act
            var settings = new JwtSettings
            {
                Issuer = "IssuerTest",
                Audience = "AudienceTest",
                Secret = "SecretTest"
            };

            // Assert
            settings.Issuer.Should().NotBeNullOrWhiteSpace();
            settings.Audience.Should().NotBeNullOrWhiteSpace();
            settings.Secret.Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(30)]
        [InlineData(1440)] // 24h
        public void JwtSettings_Should_Accept_Valid_AccessTokenMinutes(int minutes)
        {
            // Act
            var settings = new JwtSettings { AccessTokenMinutes = minutes };

            // Assert
            settings.AccessTokenMinutes.Should().Be(minutes);
            settings.AccessTokenMinutes.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-60)]
        public void JwtSettings_Should_Allow_Setting_Invalid_Minutes_But_Value_Should_Be_Stored(int minutes)
        {
            // Act
            var settings = new JwtSettings { AccessTokenMinutes = minutes };

            // Assert
            settings.AccessTokenMinutes.Should().Be(minutes);
        }

        [Fact]
        public void JwtSettings_Should_Have_Proper_Property_Types()
        {
            // Arrange
            var settings = new JwtSettings();

            // Assert
            settings.Issuer.Should().BeOfType<string?>();
            settings.Audience.Should().BeOfType<string?>();
            settings.Secret.Should().BeOfType<string?>();
            settings.AccessTokenMinutes.Should().BeOfType<int>();
        }

        [Fact]
        public void JwtSettings_Should_Be_A_Plain_Configuration_Object()
        {
            // Act
            var settings = new JwtSettings();

            // Assert
            settings.GetType().IsClass.Should().BeTrue();
            settings.GetType().IsSealed.Should().BeTrue("JwtSettings should be sealed for safety");
        }
    }
}