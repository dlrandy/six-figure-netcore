using System;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers
{
	[Route("api/companies")]//[Route("api/[controller]")]
	[ApiController]
	public class CompaniesController : ControllerBase
	{
		private readonly IServiceManager _service;
		public CompaniesController(IServiceManager service)
		{
			_service = service;
		}

		[HttpGet]
		public ActionResult GetCompanies() {
			//try
			//{
				var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
				return Ok(companies);
			//}
			//catch (Exception ex)
			//{
			//	return StatusCode(500, "Internal server error");
			//}
		}

		[HttpGet("{id:guid}")]
		public IActionResult GetCompany(Guid id) {
			var company = _service.CompanyService.GetCompany(id, false);
			return Ok(company);
		}
	}
}

