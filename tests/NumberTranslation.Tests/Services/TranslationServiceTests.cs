using FluentAssertions;
using NumberTranslation.Domain.Interfaces;
using NumberTranslation.Domain.Services;
using System;
using Xunit;

namespace NumberTranslation.Tests.Services
{
	public class TranslationServiceTests
	{
		private readonly ITranslationService _translationService;
		public TranslationServiceTests()
		{
			_translationService = new TranslationService();
		}

		[Theory]
		[InlineData("polsx polsx pol jin kil", 2016)]
		[InlineData("polsx polsx pol kil jin", 2014)]
		[InlineData("polsx polsx kil jin", 2004)]
		[InlineData("kil kil kil", 3)]
		[InlineData("kil pol", 9)]
		[InlineData("pol pol kil jin", 24)]
		[InlineData("kil jin", 4)]
		[InlineData("kilow jin", 55)]
		[InlineData("kilow kil pol", 59)]
		[InlineData("jij kil pol", 109)]
		[InlineData("jinjin kil jin", 504)]
		public void ShouldReturnTranslatedNumber(string originalNumber, decimal translateNumber)
		{
			var result = _translationService.KwegonianToDecimal(originalNumber);
			result.Value.Should().Be(translateNumber);
		}

		[Fact]
		public void ShouldReturnTranslatedNumberWithErro()
		{
			var result = _translationService.KwegonianToDecimal("lalaldsfsfdfaf");

			result.IsFailure.Should().BeTrue();
			result.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public void ShouldReturnTranslatedNumberWithErroInSecondNumber()
		{
			var result = _translationService.KwegonianToDecimal("polsx dfaf");

			result.IsFailure.Should().BeTrue();
			result.IsSuccess.Should().BeFalse();
		}

		[Fact]
		public void ShouldThrowError()
		{
			string customerId = null;

			var response = FluentActions.Invoking(() => _translationService.KwegonianToDecimal(customerId));

			response.Should().Throw<Exception>();
		}
	}
}
