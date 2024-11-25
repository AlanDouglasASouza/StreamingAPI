using Microsoft.AspNetCore.Mvc;
using StreamingAPI.Models;
using StreamingAPI.Data;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly StreamingContext _context;

    public UsersController(StreamingContext context)
    {
        _context = context;
    }

    // Route to get all users (excluding passwords)
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _context.Users
            .Select(u => new
            {
                u.Id,
                u.Name,
                u.Email
            })
            .ToList();

        return Ok(users);
    }

    // Route to create a new user
    [HttpPost]
    public IActionResult Create(UserCreationDTO userDto)
    {
        if (userDto == null || string.IsNullOrWhiteSpace(userDto.Name) ||
            string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
        {
            return BadRequest("All fields (Name, Email, Password) are required.");
        }

        // Check if the email already exists
        if (_context.Users.Any(u => u.Email == userDto.Email))
        {
            return Conflict("A user with this email already exists.");
        }

        // Create a new user entity
        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password // Make sure to hash the password in a real application
        };

        // Add the user to the database
        _context.Users.Add(user);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new
        {
            user.Id,
            user.Name,
            user.Email
        });
    }

    // Route to get a user by ID (excluding password)
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _context.Users
            .Where(u => u.Id == id)
            .Select(u => new
            {
                u.Id,
                u.Name,
                u.Email
            })
            .FirstOrDefault();

        if (user == null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }
}


