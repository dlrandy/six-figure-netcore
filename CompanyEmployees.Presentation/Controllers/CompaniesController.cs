﻿using System;
using CompanyEmployees.Presentation.ModelBinders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

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

		[HttpGet(Name = "GetCompanies")]
		[Authorize(Roles = "Manager")]
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

		[HttpGet("{id:guid}", Name = "CompanyById")]
		public IActionResult GetCompany(Guid id) {
			var company = _service.CompanyService.GetCompany(id, false);
			return Ok(company);
		}

		[HttpGet("collection/({ids})", Name = "CompanyCollection")]
		public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids) {
			var companies = _service.CompanyService.GetByIds(ids, false);
			return Ok(companies);
		}


		[HttpPost("collection")]
		public IActionResult CraeteCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection) {
			var result = _service.CompanyService.CreateCompanyCollection(companyCollection);
			return CreatedAtRoute("CompanyCollection", new { result.ids },result.companies);
		}

		[HttpPost]
		public IActionResult CraeteCompany([FromBody] CompanyForCreationDto company) {
			if (company is null)
			{
				return BadRequest("CompanyForCreationDto object is null");
			}

			var createdCompany = _service.CompanyService.CreateCompany(company);
			return CreatedAtRoute("CompanyById", new {id = createdCompany.Id },createdCompany);
		}
		[HttpDelete("{id:guid}")]
		public IActionResult DeleteCompany(Guid id)
		{
			_service.CompanyService.DeleteCompany(id, trackChanges: false);
			return NoContent();
		}

		[HttpPut("{id:guid}")]
		public IActionResult UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
		{
			if (company is null)
			{
				return BadRequest("CompanyForUpdateDto object is null");
			}
			_service.CompanyService.UpdateCompany(id, company, trackChanges: true);
			return NoContent();
		}
	}
}

