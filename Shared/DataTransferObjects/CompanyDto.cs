﻿using System;
namespace Shared.DataTransferObjects
{
	//[Serializable]
	//public record CompanyDto(Guid Id,string name, string FullAddress);

	public record CompanyDto {
		public Guid Id { get; init; }
		public string? Name { get; init; }
		public string? FullAddress { get; init; }
		public string? Country { get; init; }
	}

	public record CompanyForCreationDto(string Name, string Address, string Country,
		IEnumerable<EmployeeForCreationDto>? Employees);

	public record CompanyForUpdateDto(string Name, string Address, string Country,
		IEnumerable<EmployeeForCreationDto> Employees);
}

