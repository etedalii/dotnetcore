using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using MohDemo.DataAccess.Data.Repository.IRepository;
using MohDemo.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MohDemo
{
	public class AuthorizationRequirement : IAuthorizationRequirement { }

	public class PermissionHandler : AuthorizationHandler<AuthorizationRequirement>
	{
		private readonly IUnitOfWork _unitofWork;

		public PermissionHandler(IUnitOfWork unitofWork)
		{
			_unitofWork = unitofWork;
		}

		protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
		{
			if (Global.UserId == 0 || string.IsNullOrEmpty(Global.CurrentUserFullName))
			{
				context.Fail();
				return;
			}
			if (context.Resource is RouteEndpoint endpoint)
			{
				endpoint.RoutePattern.RequiredValues.TryGetValue("controller", out var _controller);
				endpoint.RoutePattern.RequiredValues.TryGetValue("action", out var _action);

				endpoint.RoutePattern.RequiredValues.TryGetValue("page", out var _page);
				endpoint.RoutePattern.RequiredValues.TryGetValue("area", out var _area);

				var isAuthenticated = context.User.Identity.IsAuthenticated;
				if (isAuthenticated && _controller != null && _action != null)
				{
					var code = context.User.Identity.Name;
					var user = _unitofWork.Users.Where(_ => _.UserName == code).FirstOrDefault();
					if (user == null)
					{
						context.Fail();
						return;
					}
					else
					{
						var userRole = _unitofWork.UserRole.Where(_ => _.UserId == user.Id).FirstOrDefault();
						if (userRole != null)
						{
							var role = _unitofWork.Role.Where(_ => _.Id == userRole.RoleId).FirstOrDefault();
							object formNameCtr = null, actionAct;
							endpoint.RoutePattern.RequiredValues.TryGetValue("controller", out formNameCtr);
							endpoint.RoutePattern.RequiredValues.TryGetValue("action", out actionAct);
							string formName = formNameCtr as string;
							string action = actionAct as string;
							eFormId result;
							if (Enum.TryParse(formName, out result))
							{
								if (string.Compare("Home", formName, true) == 0 || string.Compare("Index", action, true) != 0)
								{
									context.Succeed(requirement);
									return;
								}

								eFormId eForm = (eFormId)Enum.Parse(typeof(eFormId), formName);
								if (string.Compare(role.Name, "Administrator", true) == 0)
								{
									context.Succeed(requirement);
									return;
								}

								if (_unitofWork.AspNetFormRole.Count(x => x.AspNetFormId == (int)eForm && x.RoleId == role.Id) > 0 || (int)eForm < 1)
								{
									context.Succeed(requirement);
									return;
								}
								else
								{
									context.Fail();
									return;
								}
							}
						}
						else
						{
							context.Fail();
							return;
						}
					}
				}
				else
				{
					context.Fail();
				}
			}
		}
	}
}