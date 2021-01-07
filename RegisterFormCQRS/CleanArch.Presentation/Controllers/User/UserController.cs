using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArch.Application.Models;
using CleanArch.Application.Users.Commands.CreateUser;
using CleanArch.Presentation.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Presentation.Controllers.User
{
    public class UserController : ApiController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost(ApiRoutes.User.UserApi.CreateUser)]
        public async Task<ActionResult<ResponseResult>> CreateClassRoom(CreateUserCommand command)
        {
            if (command == null)
                return BadRequest();

            var success = await Mediator.Send(command);
            return Ok(new ResponseResult(true, new string[] { }, "Created Successfully"));
        }
    }
}
