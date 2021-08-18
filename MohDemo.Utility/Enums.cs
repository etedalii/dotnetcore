using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohDemo.Utility
{
	public enum eDeleteResult
	{
		Failed = 1,
		Success = 2,
		UseAnotherPlace = 3,
		UseAnotherPlaceForEdit = 4
	}

	public enum eFormId
	{
		Other = -3,
		Account = -2,
		Map = -1,
		Home = 0,
		Zone = 1,
		Role = 2,
		RoleToForm = 3,
		SystemUser = 4,
		ServiceGroup = 5,
		Service = 6,
		RequestExpirationPattern = 7
	}

	public enum eGroupId
	{
		BasicDefinitions = 1,
		Setting = 2,
		GeneralDefinition = 3
	}
}