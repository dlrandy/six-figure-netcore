﻿using System;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace CompanyEmployees.Presentation.Controllers
{
	[Route("api/companies/{companyId}/employees")]
	[ApiController]
	public class EmployeesController : ControllerBase {
		private readonly IServiceManager _service;
		public EmployeesController(IServiceManager service)
		{
			_service = service;
		}
		[HttpGet]
		public async Task<IActionResult> GetEmployeesForCompany(Guid companyId,
			[FromQuery] EmployeeParameters employeeParameters)
		{
			var pagedResult = await _service.EmployeeService.GetEmployeesAsync(companyId, employeeParameters,trackChanges: false);
			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
			return Ok(pagedResult.employees);
		}
		[HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
		public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id) {
			var employee = await _service.EmployeeService.GetEmployeeAsync(companyId, id, trackChanges: false);
			return Ok(employee);
		}
		[HttpPost]
		public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee) {
			if (employee is null)
			{
				return BadRequest("EmployeeForCreationDto object is null");
			}
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("age","kkak kakakak aka");
				return UnprocessableEntity(ModelState);
			}
			var employeeToReturn = await _service.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges:false);
			return CreatedAtRoute("GetEmployeeForCompany", new { companyId, employeeToReturn.id},employeeToReturn);
		}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
		{
			await _service.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, false);
			return NoContent();
		}
		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id,
			[FromBody] EmployeeForUpdateDto employee)
		{
			if (employee is null)
			{
				return BadRequest("EmployeeForUpdateDto object is null");
			}
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee,
				companyTrackChanges: false, employeeTrackChanges: true);
			return NoContent();
		}
		[HttpPatch("{id:guid}")]
		public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
			[FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
		{
			if (patchDoc is null)
			{
				return BadRequest("patchDOc object sent from client is null");
			}
			var result = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id,
				compTrackChanges: false, empTrackChanges: true);
			patchDoc.ApplyTo(result.employeeToPatch, ModelState);
			TryValidateModel(result.employeeToPatch);
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch,
				result.employeeEntity);
			return NoContent();
		}

	}
}

