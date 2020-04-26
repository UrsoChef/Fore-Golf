using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fore_Golf.Data;
using Fore_Golf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fore_Golf.Contracts;

namespace Fore_Golf.Controllers
{
    public class GolferController : Controller
    {
        private readonly IGolferRepository _repo;
        private readonly IMapper _mapper;

        public GolferController(IGolferRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: Golfer
        public async Task<ActionResult> Index()
        {
            var golfers = await _repo.FindAll();
            var model = _mapper.Map<List<Golfer>, List<GolferViewModel>>(golfers.ToList());
            return View(model);
        }

        // GET: Golfer/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var isExists = await _repo.IsExists(id);
            if (!isExists)
            {
                return NotFound();
            }
            var golfer = await _repo.FindByID(id);
            var model = _mapper.Map<GolferViewModel>(golfer);
            return View(model);
        }

        // GET: Golfer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Golfer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GolferViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Create Golfer: Model state is invalid");
                    return View(model);
                }
                var dateCreated = DateTime.Now;
                model.DateJoined = dateCreated;
                var golfer = _mapper.Map<Golfer>(model);
                var isSuccess = await _repo.Create(golfer);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Create Golfer: Unable to Create a new golfer");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Create Golfer: Something went wrong in the golfer creation");
                return View(model);
            }
        }

        // GET: Golfer/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var golfer = await _repo.FindByID(id);
            var model = _mapper.Map<GolferViewModel>(golfer);
            return View(model);
        }

        // POST: Golfer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GolferViewModel model)
        {
            try
            {
                // update logic here
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var golfer = _mapper.Map<Golfer>(model);
                var isSuccess = await _repo.Update(golfer);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Edit Golfer: Update not successfull");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Edit Golfer: Something went wrong");
                return View(model);
            }
        }

        // GET: Golfer/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            // delete logic here
            var golfer = await _repo.FindByID(id);
            if (golfer == null)
            {
                return NotFound();
            }
            var isSuccess = await _repo.Delete(golfer);
            if (!isSuccess)
            {
                ModelState.AddModelError("", "Delete Golfer: Could not Delete");
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Golfer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, GolferViewModel model)
        {
            try
            {
                var golfer = await _repo.FindByID(id);
                if (golfer == null)
                {
                    return NotFound();
                }
                var isSuccess = await _repo.Delete(golfer);
                if (!isSuccess)
                {
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}