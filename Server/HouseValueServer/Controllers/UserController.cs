using System;
using System.Web.Http;

namespace HouseValueServer.Controllers
{
    public class UserController : ApiController
    {
        // GET api/user/GetUserInfo/request
        [HttpGet]
        public void GetUserInfo(string request)
        {
            throw new NotImplementedException();
        }

        public void ValidateCredentials(int id)
        {
            throw new NotImplementedException();
        }
    }
}
