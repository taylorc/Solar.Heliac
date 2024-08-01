using Solar.Heliac.Application.Common.Interfaces;

namespace Solar.Heliac.Database;

public class MockCurrentUserService : ICurrentUserService
{
    public string UserId => "00000000-0000-0000-0000-000000000000";
}