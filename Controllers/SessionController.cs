using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Dtos;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers {
    [ApiController]
    [Route ("v1/sessions")]
    public class SessionController : ControllerBase {

        [HttpGet]
        [Route ("")]
        [Authorize]
        public string GetAuthenticated () {
            return String.Format ("Autenticado - {0}", User.Identity.Name);
        }

        [HttpGet]
        [Route ("refresh-token")]
        [Authorize]
        public async Task<ActionResult<dynamic>> RefreshToken ([FromServices] DataContext context) {
            var email = User.Claims
                .Where (c => c.Type == ClaimTypes.Email)
                .Select (v => v.Value).FirstOrDefault ();

            var user = await context.Users
                .AsNoTracking ()
                .Where (x => x.Email == email)
                .FirstOrDefaultAsync ();
            if (user == null)
                return NotFound (new { message = "Usuário não encontrado" });

            var token = TokenService.GenerateToken (user);

            UserResultDTO dto = new UserResultDTO {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };

            return new {
                user = dto, token = token
            };
        }

        [HttpPost]
        [Route ("")]
        public async Task<ActionResult<dynamic>> Post ([FromServices] DataContext context, [FromBody] UserLoginDTO model) {
            if (ModelState.IsValid) {

                var user = await context.Users
                    .AsNoTracking ()
                    .Where (x => x.Email == model.Email && x.Password == model.Password)
                    .FirstOrDefaultAsync ();
                if (user == null)
                    return NotFound (new { message = "Usuário ou senha inválidos" });

                var token = TokenService.GenerateToken (user);

                UserResultDTO dto = new UserResultDTO {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role
                };

                return new {
                    user = dto, token = token
                };

            } else {
                return BadRequest (ModelState);
            }
        }
    }
}