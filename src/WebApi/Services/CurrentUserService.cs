using Solar.Heliac.Application.Common.Interfaces;
using System.Security.Claims;

namespace Solar.Heliac.WebApi.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}