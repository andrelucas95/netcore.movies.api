using System;
using System.Net;
using System.Threading.Tasks;
using IMDB.Movies.API.Application;
using IMDB.Movies.API.Application.Extensions;
using IMDB.Movies.API.Application.Models;
using IMDB.Movies.API.Application.Services;
using IMDB.Movies.API.Models;
using IMDB.Movies.API.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Movies.API.Controllers
{
    [ApiController]
    [Route("administrators")]
    public class AdministratorController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AdministratorController(
            UserManager<IdentityUser> userManager, 
            JwtService jwtService, 
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        [HttpPost, Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.ADMINISTRATOR);
                var roles = await _userManager.GetRolesAsync(user);

                var appUser = new User(Guid.Parse(user.Id), model.Name, Roles.ADMINISTRATOR);
                await _userRepository.Add(appUser);

                return Ok(_jwtService.GenerateToken(user, roles));
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPut, Route("")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = User.Claims.Id();

            var user = await _userRepository.Get(Guid.Parse(userId));

            if (user == null) return NotFound();

            user.UpdateName(model.Name);

            await _userRepository.Save();

            return NoContent();
        }

        [Authorize]
        [HttpPut, Route("deactivate")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Remove()
        {
            var userId = User.Claims.Id();

            var user = await _userRepository.Get(Guid.Parse(userId));

            if (user == null) return NotFound();

            user.Deactive();

            await _userRepository.Save();

            return NoContent();
        }

        [Authorize]
        [HttpGet, Route("users")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> List(
            [FromQuery] int index = 1,
            [FromQuery] int pageSize = 8)
        {
            var result = await _userRepository.List(Roles.USER, true, pageSize, index);
            
            return Ok(new PageDataResponse<User>(result.users, result.total));
        }
    }
}