using System;
namespace Shared.DataTransferObjects
{
	public record EmployeeDto(Guid id,  string name, int age, string position);
	public record EmployeeForCreationDto( string name, int age, string position);

	 
}

