using Microsoft.AspNetCore.Mvc;

namespace DataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDetailsController : Controller
    {
        [HttpGet(Name = "GetUserDetails")]
        public UserDetails Get()
        {
            UserDetails userDetails = new UserDetails();

            return userDetails;
        }
    }

    
}
