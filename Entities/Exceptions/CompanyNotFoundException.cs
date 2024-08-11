using System;
namespace Entities.Exceptions
{
	public sealed class CompanyNotFoundException : NotFoundException
	{
		public CompanyNotFoundException(Guid companyId) : base($"the company with id: {companyId} doesn't exist in the database.")
		{
		}
	}
}

