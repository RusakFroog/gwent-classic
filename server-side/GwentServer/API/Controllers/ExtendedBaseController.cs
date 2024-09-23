using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ExtendedBaseController : ControllerBase
{
    protected int UserId
    {
        get
        {
            if (HttpContext.Items["UserId"] != null)
                return (int)HttpContext.Items["UserId"];

            throw new Exception("User is not authenticated.");
        }
    }
}
