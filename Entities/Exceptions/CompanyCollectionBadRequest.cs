using System;
namespace Entities.Exceptions
{
	public sealed class CompanyCollectionBadRequest:BadRequestException
	{
		public CompanyCollectionBadRequest():base("Company collectino sent from a client is null.")
		{
		}
	}
}

