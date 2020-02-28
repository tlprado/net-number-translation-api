using NumberTranslation.Utils;

namespace NumberTranslation.Domain.Interfaces
{
	public interface ITranslationService
	{
		Response<decimal> KwegonianToDecimal(string kwegonianNumber);
	}
}
