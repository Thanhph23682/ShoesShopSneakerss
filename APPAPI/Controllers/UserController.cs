using APPAPI.Services;
using APPAPI.ViewModels;
using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace APPAPI.Controllers
{
 [Route("api/[controller]")]
 [ApiController]
 public class UserController : ControllerBase
 {
  ShopDbContext _context = new ShopDbContext();
  private readonly CRUDapi<User> _crud;




  public UserController()
  {
   _crud = new CRUDapi<User>(_context, _context.Users);
  }

  [HttpGet]
  public IEnumerable<User> GetAll()
  {
   return _crud.GetAllItems().ToList();

  }
  [Route("Create")]
  [HttpPost]
  public bool Create(User obj)
  {
   return _crud.CreateItem(obj);
  }

  [HttpPut]
  [Route("Update")]
  public bool Update(User obj)
  {

   User item = _crud.GetAllItems().FirstOrDefault(p => p.ID == obj.ID);
   if (item != null)
   {
    item.RoleID = obj.RoleID;
    item.UserName = obj.UserName;
    item.FullName = obj.FullName;
    item.Password = obj.Password;
    item.PhoneNumber = obj.PhoneNumber;
    item.Image = obj.Image;
    item.Status = obj.Status;

    return _crud.UpdateItem(item);
   }
   else
   {
    return false;
   }
  }
  [Route("Delete")]
  [HttpDelete]
  public bool Delete(int? id)
  {
   User item = _crud.GetAllItems().FirstOrDefault(p => p.ID == id);

   if (item != null)
   {
    // Nếu tìm thấy, thực hiện xóa
    return _crud.DeleteItem(item);
   }
   else
   {
    // Nếu không tìm thấy, trả về false
    return false;
   }

  }
  [Route("Register")]
  [HttpPost]
  public bool Register(User obj)
  {


   if (!_context.Roles.Any())
   {
    var adminRole = new Role
    {
     RoleName = "admin",
     Description = "Quản trị viên",
    };
    var staffRole = new Role
    {
     RoleName = "staff",
     Description = "Nhân viên"
    };
    _context.Roles.AddRange(adminRole, staffRole);
    _context.SaveChanges();

   }
   // Gán RoleID dựa trên một điều kiện (ví dụ: tên người dùng hoặc một trường nào đó)
   if (obj.UserName.StartsWith("admin", StringComparison.OrdinalIgnoreCase))
   {
    obj.RoleID = _context.Roles.SingleOrDefault(p => p.RoleName == "Admin")?.ID ?? 0;
   }
   else
   {
    obj.RoleID = _context.Roles.SingleOrDefault(p => p.RoleName == "Staff")?.ID ?? 0;
   }

   obj.Status = true;
   return _crud.CreateItem(obj);


  }
  [Route("Login")]
  [HttpPost]
  public async Task<IActionResult> Login(User obj)
  {
   var user = _context.Users.SingleOrDefault(p => p.UserName == obj.UserName && p.Password == obj.Password);
   if (user == null)
   {
    return BadRequest("Người dùng không tồn tại");
   }
   var token = await GenerationToken(user);
   return Ok(new TokenVM
   {
    AccessToken = token.AccessToken,
    RefreshToken = token.RefreshToken,
   }
);


  }

  private async Task<TokenVM> GenerationToken(User obj)
  {
   var jwtTokenHandler = new JwtSecurityTokenHandler();
   var secretkeyBytes = Encoding.UTF8.GetBytes("JWT:SecretKey");
   var claims = new ClaimsIdentity(new List<Claim>
{
     new Claim (ClaimTypes.Name, obj.UserName),
     new Claim (JwtRegisteredClaimNames.Email, obj.FullName),
     new Claim ("UserId",obj.ID.ToString()),
     new Claim ("RoleId",obj.RoleID.ToString()),

     new Claim (ClaimTypes.Role,obj.Role?.RoleName?? "User"),

});
   var tokenDescription = new SecurityTokenDescriptor
   {
    Subject = claims,
    Expires = DateTime.UtcNow.AddMinutes(20),
    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretkeyBytes), SecurityAlgorithms.HmacSha512Signature)
   };

   var token = jwtTokenHandler.CreateToken(tokenDescription);
   var accessToken = jwtTokenHandler.WriteToken(token);
   var refreshToken = GenerateRefreshToken();

   return new TokenVM()
   {
    AccessToken = accessToken,
    RefreshToken = refreshToken
   };

   
  }
  private string GenerateRefreshToken()
  {
   var random = new byte[32];
   using (var rng = RandomNumberGenerator.Create())
   {
    rng.GetBytes(random);
    return Convert.ToBase64String(random);
   }

  }
 }
}

