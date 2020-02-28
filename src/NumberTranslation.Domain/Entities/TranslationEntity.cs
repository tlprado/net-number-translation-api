namespace NumberTranslation.Domain.Entities
{
	public class TranslationEntity
	{
		public int? ArabicNumeral { get; private set; }

		public TranslationEntity() {}

		public void TranslateKwegonian(string number)
		{
			ArabicNumeral = number switch
			{
				"kil" => 1,
				"jin" => 5,
				"pol" => 10,
				"kilow" => 50,
				"jij" => 100,
				"jinjin" => 500,
				"polsx" => 1000,
				_ => null,
			};
		}
	}
}
