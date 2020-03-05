using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Dtos;
using Shop.Models;

namespace Shop.Controllers {
    [ApiController]
    [Route ("v1/users")]
    public class UserController : ControllerBase {

        [HttpPost]
        [Route ("")]
        public async Task<ActionResult<User>> Post ([FromServices] DataContext context, [FromBody] UserRegisterDTO model) {
            if (ModelState.IsValid) {
                if (model.Password != model.PasswordAgain)
                    return BadRequest (new { message = "As senhas não coincidem" });

                var user = new User {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    Email = model.Email,
                    Role = "customer"
                };

                context.Users.Add (user);
                await context.SaveChangesAsync ();
                return user;
            } else {
                return BadRequest (ModelState);
            }
        }
    }
}