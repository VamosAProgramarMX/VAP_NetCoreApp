using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VAP_NetCoreApp.Models;
using VAP_NetCoreApp.Models.Queries;
using VAP_NetCoreApp.Web.Models;

namespace VAP_NetCoreApp.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly Database _database;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
            _database = new Database();
        }

        public IActionResult Index()
        {
            return View(_database.Users.GetAll());
        }

        private void SetRolesInViewBag()
        {
            ViewBag.Roles = _database.Roles.GetAll()
                            .Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() });
        }

        #region Create
        public IActionResult Create()
        {
            SetRolesInViewBag();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            var result = _database.Users.CreateUsingStoredProcedure(user);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                SetRolesInViewBag();
                return View(user);
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            var user = _database.Users.GetByID(id);
            if (user == null)
                return NotFound();

            SetRolesInViewBag();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            var result = _database.Users.Update(user);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                SetRolesInViewBag();
                return View(user);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Delete
        public ActionResult Delete(int id)
        {
            var user = _database.Users.GetByID(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _database.Users.Delete(id);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(id);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
