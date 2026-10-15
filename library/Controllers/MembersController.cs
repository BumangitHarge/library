using library.Data;
using library.Models;
using Microsoft.AspNetCore.Mvc;

namespace library.Controllers
{
    public class MembersController : Controller
    {
        private readonly MemberRepository _members;

        public MembersController(MemberRepository members)
        {
            _members = members;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _members.GetAllAsync();
            return View(members);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Member());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (!ModelState.IsValid)
                return View(member);

            await _members.AddAsync(member);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var member = await _members.GetByIdAsync(id);

            if (member is null)
                return NotFound();

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Member member)
        {
            if (!ModelState.IsValid)
                return View(member);

            await _members.UpdateAsync(member);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var member = await _members.GetByIdAsync(id);

            if (member is null)
                return NotFound();

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Member member)
        {
            await _members.DeleteAsync(member.MemberId);
            return RedirectToAction(nameof(Index));
        }
    }
}