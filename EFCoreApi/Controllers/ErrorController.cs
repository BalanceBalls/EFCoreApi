using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreApi.Controllers
{
	[ApiExplorerSettings(IgnoreApi = true)]
	[ApiController]
	public class ErrorController : ControllerBase
	{
		[Route("/error")]
		[AllowAnonymous]
		public IActionResult Error() => Problem();
	}
}