using Xunit;
using FluentAssertions;
using NumberTranslation.Domain.Entities;

namespace NumberTranslation.Tests.Domain
{
	public class TranslationTests
	{
		[Theory]
		[InlineData("kil", 1)]
		[InlineData("jin", 5)]
		[InlineData("pol", 10)]
		[InlineData("kilow", 50)]
		[InlineData("jij", 100)]
		[InlineData("jinjin", 500)]
		[InlineData("polsx", 1000)]
		public void ShouldReturnTheNumberTranslation(string originalNumber, int translateNumber)
		{
			TranslationEntity translation = new TranslationEntity();

			translation.TranslateKwegonian(originalNumber);

			translation.ArabicNumeral.Should().Be(translateNumber);
		}

		[Fact]
		public void ShouldReturnNullIfNotTranslates() 
		{
			TranslationEntity translation = new TranslationEntity();

			translation.TranslateKwegonian("popo");

			translation.ArabicNumeral.Should().BeNull();
		}
	}
}
