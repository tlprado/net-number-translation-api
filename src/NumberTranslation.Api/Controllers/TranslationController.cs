using System.Net;
using NumberTranslation.Utils;
using Microsoft.AspNetCore.Mvc;
using NumberTranslation.Domain.Interfaces;

namespace NumberTranslation.Api.Controllers
{
	[ApiController]
	[Produces("application/json")]
	[Route("[controller]")]
	public class TranslationController : Controller
    {
		private readonly ITranslationService _translationService;
		
		public TranslationController(ITranslationService translationService)
		{
			_translationService = translationService;
		}

		[HttpGet]
		[Route("{kewegonianNumber}")]
		[ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.BadRequest)]
		public IActionResult Get([FromRoute] string kewegonianNumber)
		{
			var response = _translationService.KwegonianToDecimal(kewegonianNumber);

			return response.IsFailure ? (IActionResult)BadRequest(response) : Ok(response.Value);
		}
	}
}