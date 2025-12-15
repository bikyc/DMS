using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = dto.Username,
                PasswordHash = HashPassword(dto.Password),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                RoleId = dto.RoleId,
                IsActive = true,
                CreatedOn = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        private string HashPassword(string password)
        {
            // Implement secure hash, e.g., BCrypt or SHA256
            return password;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}
