using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
	public record EmployeeDto(Guid id,  string name, int age, string position);
    //public record EmployeeForCreationDto(
    //       [Required(ErrorMessage = "Employee name is a required field.")]
    //	[MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    //	string Name,
    //       [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
    //       int Age,
    //	[Required(ErrorMessage = "Position is a required field.")]
    //	[MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
    //	string Position
    //       );
    //public record EmployeeForUpdateDto(string name, int age, string position);

    public abstract record EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }
        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
        public int Age { get; init; }
        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string? Position { get; init; }
    }
    public record EmployeeForCreationDto : EmployeeForManipulationDto;
    public record EmployeeForUpdateDto : EmployeeForManipulationDto;
}

