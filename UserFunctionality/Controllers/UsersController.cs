using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserFunctionality.DataAccess;
using UserFunctionality.Models;

namespace UserFunctionality.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDbContext _context;

        public UsersController(UserDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string SearchText = "")
        {
            List<Users> users;

            if (SearchText != "" && SearchText != null)
            {
                users = _context.users.Where(p => p.FirstName.Contains(SearchText)).ToList();
            }
            else {
                users = _context.users.ToList();
            }
       
            return View(users);
        }

        // GET: Users/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(users);
        //}

        public async Task<IActionResult> Search(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            // Call your stored procedure here. Example:
            var users = await _context.users
                            .FromSqlRaw("EXEC SearchUsers @p0, @p1, @p2", searchTerm, pageNumber, pageSize)
                            .ToListAsync();

            return View(users); // Make sure to create a corresponding view
        }
    }
}
