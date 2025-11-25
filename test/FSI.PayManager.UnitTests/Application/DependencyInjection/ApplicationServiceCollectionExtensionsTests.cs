using Xunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using FSI.PayManager.Application.DependencyInjection;
using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using FSI.PayManager.Application.Services;

namespace FSI.PayManager.UnitTests.Application.DependencyInjection
{
    public sealed class ApplicationServiceCollectionExtensionsTests
    {
        private readonly IServiceCollection _services;

        public ApplicationServiceCollectionExtensionsTests()
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void AddApplication_ShouldRegister_AllCrudServices()
        {
            // Arrange
            _services.AddApplication();

            // Act
            var provider = _services.BuildServiceProvider();

            // Assert
            provider.GetService<ICrudAppService<UserDto>>()
                .Should().NotBeNull()
                .And.BeOfType<UserAppService>();

            provider.GetService<ICrudAppService<WalletDto>>()
                .Should().NotBeNull()
                .And.BeOfType<WalletAppService>();

            provider.GetService<ICrudAppService<CategoryDto>>()
                .Should().NotBeNull()
                .And.BeOfType<CategoryAppService>();

            provider.GetService<ICrudAppService<RecurringTransactionDto>>()
                .Should().NotBeNull()
                .And.BeOfType<RecurringTransactionAppService>();

            provider.GetService<ICrudAppService<FinancialTransactionDto>>()
                .Should().NotBeNull()
                .And.BeOfType<FinancialTransactionAppService>();

            provider.GetService<ICrudAppService<ReminderDto>>()
                .Should().NotBeNull()
                .And.BeOfType<ReminderAppService>();
        }

        [Fact]
        public void AddApplication_ShouldRegister_AuthService()
        {
            // Arrange
            _services.AddApplication();

            // Act
            var provider = _services.BuildServiceProvider();

            // Assert
            provider.GetService<IAuthAppService>()
                .Should().NotBeNull()
                .And.BeOfType<AuthAppService>();
        }

        [Fact]
        public void AddApplication_ShouldRegister_ServicesAsScoped()
        {
            // Arrange
            _services.AddApplication();

            // Act
            var descriptor = _services.FirstOrDefault(s => s.ServiceType == typeof(ICrudAppService<UserDto>));

            // Assert
            descriptor.Should().NotBeNull();
            descriptor!.Lifetime.Should().Be(ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddApplication_ShouldResolveDifferentInstances_OnDifferentScopes()
        {
            // Arrange
            _services.AddApplication();
            var provider = _services.BuildServiceProvider();

            // Act
            using var scope1 = provider.CreateScope();
            using var scope2 = provider.CreateScope();

            var instance1 = scope1.ServiceProvider.GetRequiredService<ICrudAppService<UserDto>>();
            var instance2 = scope2.ServiceProvider.GetRequiredService<ICrudAppService<UserDto>>();

            // Assert
            instance1.Should().NotBeSameAs(instance2);
        }

        [Fact]
        public void AddApplication_ShouldResolveSameInstance_WithinSameScope()
        {
            // Arrange
            _services.AddApplication();
            var provider = _services.BuildServiceProvider();

            // Act
            using var scope = provider.CreateScope();

            var instance1 = scope.ServiceProvider.GetRequiredService<ICrudAppService<UserDto>>();
            var instance2 = scope.ServiceProvider.GetRequiredService<ICrudAppService<UserDto>>();

            // Assert
            instance1.Should().BeSameAs(instance2);
        }
    }
}