﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Capstone_Connect.Data;
using Capstone_Connect.Dtos;
using Capstone_Connect.Model;

namespace Capstone_Connect.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class VisitorController : Controller
    {
        private readonly ICapstoneConnectRepo _repository;
        public VisitorController(ICapstoneConnectRepo repository)
        {
            _repository = repository;
        }

        [Authorize(AuthenticationSchemes = "AdminScheme")]
        [Authorize(Policy = "VisitorMinimum")]
        [HttpGet("GetAuth/{email}")]
        public ActionResult<string> GetAuth(string email) => Ok(_repository.GetAuth(email));

        [HttpPost("RegisterVisitor")]
        public ActionResult RegisterVisitor(VisitorInDto visitor)
        {
            if (visitor.Email == "")
            {
                return Ok("Invalid Email");
            }
            Visitor v = _repository.GetVisitorByEmail(visitor.Email);
            if (v != null)
            {
                return Ok("Email not available.");
            }
            else
            {
                Visitor c = new Visitor { Email = visitor.Email, Password = visitor.Password, FullName = visitor.FullName};
                Visitor addedUser = _repository.RegisterVisitor(c);
                return Ok("User successfully registered");
            }
        }

        [HttpGet("LikeProject")]
        public ActionResult VisitorLike(int projectID, int visitorID)
        {
            _repository.VisitorLike(projectID, visitorID);
            return Ok();
        }
    }
}