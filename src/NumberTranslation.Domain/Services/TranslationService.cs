using System;
using System.Text.RegularExpressions;
using NumberTranslation.Domain.Entities;
using NumberTranslation.Domain.Interfaces;
using NumberTranslation.Utils;

namespace NumberTranslation.Domain.Services
{
	public class TranslationService : ITranslationService
	{
		public Response<decimal> KwegonianToDecimal(string kwegonianNumber)
		{
			try
			{
				var listNumbers = SeparateText(kwegonianNumber);
				int result = 0;

				for (int i = 0; i < listNumbers.Length; i++)
				{
					var translete = new TranslationEntity();

					int arabicNumeral1 = SetArabicNumeral(translete, listNumbers[i]);

					if(arabicNumeral1 == 0) return Response<decimal>.Fail("Number not exists");

					if (i + 1 < listNumbers.Length)
					{
						int arabicNumeral2 = SetArabicNumeral(translete, listNumbers[i + 1]);

						if (arabicNumeral2 == 0) return Response<decimal>.Fail("Number not exists");

						if (arabicNumeral1 >= arabicNumeral2)
						{
							result += arabicNumeral1;
						}
						else
						{
							result = result + arabicNumeral2 - arabicNumeral1;
							i++;
						}
					}
					else
					{
						result += arabicNumeral1;
						i++;
					}
				}

				return Response<decimal>.Ok(result);
			}
			catch (Exception)
			{
				//TODO: Fazer LOG da Exception
				throw;
			}
		}

		private string[] SeparateText(string kwegonianNumber)
		{
			string[] numberList = Regex.Split(kwegonianNumber, @"\W+");
			return numberList;
		}

		private int SetArabicNumeral(TranslationEntity translete, string number)
		{
			translete.TranslateKwegonian(number);

			return translete.ArabicNumeral == null ? 0 : Convert.ToInt32(translete.ArabicNumeral);
		}
	}
}
