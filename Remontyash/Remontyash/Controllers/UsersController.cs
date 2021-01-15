using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Remontyash.Models;
using Remontyash.ViewModels;

namespace Remontyash.Controllers
{
    public class UsersController : Controller
    {
        private readonly RemontDBContext _context;

        public UsersController(RemontDBContext context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Index()
        {
            var remontDBContext = _context.Users.Include(u => u.Emp).Include(u => u.Role);
            return View(await remontDBContext.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Emp)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "Администратор")]
        public IActionResult Create()
        {
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio");
            ViewData["Roleid"] = new SelectList(_context.Roles, "RoleId", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Create([Bind("Userid,Login,Password,Empid,Roleid")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Userid = Guid.NewGuid();
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio", user.Empid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "RoleId", "Name", user.Roleid);
            return View(user);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.Include(a=>a.Role).FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return Redirect("/Home/Index");
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio", user.Empid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "RoleId", "Name", user.Roleid);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Userid,Login,Password,Empid,Roleid")] User user)
        {
            if (id != user.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Empid"] = new SelectList(_context.Emps, "Empid", "Fio", user.Empid);
            ViewData["Roleid"] = new SelectList(_context.Roles, "RoleId", "Name", user.Roleid);
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Emp)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task Authenticate(User user)
        {
            var u = user.Login;
            var r = user.Role.Name;
            // создаем один claim
            var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
                    };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }
        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Userid == id);
        }
    }
}
