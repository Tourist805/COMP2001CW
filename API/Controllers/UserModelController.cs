using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIWork.Models;

namespace APIWork.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserModelController : Controller
    {
        private readonly DataAccess _database;

        public UserModelController(DataAccess context)
        {
            _database = context;
        }

        // POST api/usermodel
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            string response = "";
            if (user == null)
            {
                return BadRequest();
            }
            if(getValidation(user) == true)
            {
                return Unauthorized();
            }
            register(user, out response);

            return new ObjectResult(user.UserId);
        }

        // GET api/usermodel/id
        [HttpGet]
        public IActionResult Get(UserModel user)
        {
            if (user == null)
            {
                return NotFound();
            }
            
            if (getValidation(user) == true)
            {
                return new ObjectResult(user.UserId);
            }

            return NotFound();
        }

        // PUT api/usermodel/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserModel user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            _database.Update(user, id);
            return new NoContentResult();
        }

        // DELETE api/usermodel/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _database.Delete(id);
            return new NoContentResult();
        }

        private bool getValidation(UserModel user)
        {
            if (_database.Validate(user) == true)
            {
                return true;
            }
            return false;
        }

        private void register(UserModel user, out string responseMessage)
        {
            responseMessage = "";
            if(getValidation(user) == false)
            {
               _database.Register(user, out responseMessage);
            }
        }
    }
}