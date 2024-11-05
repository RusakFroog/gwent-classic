using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;

namespace Shared.Extensions;

public class ExtendedControllerBase : ControllerBase
{
    protected Guid UserId
    {
        get
        {
            if (HttpContext.Items["UserId"] != null)
                return (Guid)HttpContext.Items["UserId"];

            throw new UnauthorizationException("User is not authenticated.");
        }
    }
}
