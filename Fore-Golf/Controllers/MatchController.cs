using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fore_Golf.Contracts;
using Fore_Golf.Data;
using Fore_Golf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fore_Golf.Controllers
{
    public class MatchController : Controller
    {
        private readonly IGolferRepository _golferRepo;
        private readonly IGameRepository _gameRepo;
        private readonly IGameGolferRepository _gameGolferRepo;
        private readonly IMatchRepository _matchRepo;
        private readonly IMapper _mapper;

        public MatchController(IGolferRepository golferRepo, IGameRepository gameRepo, IGameGolferRepository gameGolferRepo, IMatchRepository matchRepo, IMapper mapper)
        {
            _golferRepo = golferRepo;
            _gameRepo = gameRepo;
            _gameGolferRepo = gameGolferRepo;
            _matchRepo = matchRepo;
            _mapper = mapper;
        }
        // GET: Match
        public async Task<ActionResult> Index()
        {
            var match = await _matchRepo.FindAll();
            var model = _mapper.Map<List<MatchViewModel>>(match);
            return View(model);
        }

        // GET: Match/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            //var match = await _matchRepo.FindByID(id);
            //var model = _mapper.Map<MatchViewModel>(match);
            //return View(model);

            Match matchDetails = await _matchRepo.FindGamesAndGolfersInMatch(id);
            MatchViewModel model = _mapper.Map<MatchViewModel>(matchDetails);
            //model.NumberOfGames = matchDetails.Games.Count();
            return View(model);
        }

        // GET: Match/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Match/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MatchViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Create Match: Model state is invalid");
                    return View();
                }

                var match = _mapper.Map<Match>(model);
                var isSuccess = await _matchRepo.Create(match);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Create Match: Unable to Create a new match");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Create Match: Something went wrong in the match creation");
                return View(model);
            }
        }

        // GET: Match/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var match = await _matchRepo.FindByID(id);
            var model = _mapper.Map<MatchViewModel>(match);
            return View(model);
        }

        // POST: Match/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, MatchViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Edit Match: Model state is invalid");
                    return View();
                }

                var match = _mapper.Map<Match>(model);
                var isSuccess = await _matchRepo.Update(match);

                if (isSuccess == false)
                {
                    ModelState.AddModelError("", "Create Match: Unable to Update the existing match");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Edit Match: Something went wrong in editing the match");
                return View(model);
            }
        }

        // GET: Match/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Match/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}