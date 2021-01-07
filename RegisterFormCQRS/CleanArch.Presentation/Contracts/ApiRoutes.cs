using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Presentation.Contracts
{
    public static class ApiRoutes
    {
        public const string ApiBase = "api";
        public const string controller = "[controller]";
        public const string version = "";


        public static class User
        {
            public const string AreaName = "user";

            public static class UserApi
            {
                public const string Apicontroller = "User";

                public const string GetAllUser = ApiBase + "/" +  AreaName + "/" + Apicontroller;
                public const string GetUserById = ApiBase + "/" + AreaName + "/" + Apicontroller + "/" + "{Id}";
                public const string CreateUser = ApiBase + "/" + AreaName + "/" + Apicontroller;
                public const string UpdateUser = ApiBase + "/" + AreaName + "/" + Apicontroller + "/" + "{Id}";
                public const string DeleteUser = ApiBase + "/" + AreaName + "/" + Apicontroller + "/" + "{Id}";

            }
        }
    }
}
