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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            model.NumberOfGames = matchDetails.Games.Count();
            model.NumberOfPlayers = matchDetails.Games.SelectMany(g => g.GameGolfers).Select(g => g.Golfer).Distinct().Count();
            return View(model);
        }

        // GET: Match/Summary/
        public async Task<ActionResult> Summary(Guid id)
        {
            Match match = await _matchRepo.FindByID(id);
            if (match.Id == null)
            {
                return NotFound();
            }

            Match theMatch = await _matchRepo.FindGamesAndGolfersInMatch(id);
            IEnumerable<Golfer> golfers = theMatch.Games.SelectMany(g => g.GameGolfers).Select(g => g.Golfer).Distinct();
            MatchScoresViewModel model = _mapper.Map<MatchScoresViewModel>(theMatch);
            model.Golfers = _mapper.Map<IEnumerable<GolferViewModel>>(golfers);
            return View(model);
        }


        public async Task<ActionResult> CopyGolfersFromLastGame(Guid matchid, Guid gameid)
        {
            Game existingGame = await _gameRepo.FindByID(gameid);
            IEnumerable<GameGolfer> lastGame = await _gameGolferRepo.FindLatestGameandGolfersInMatch(matchid, gameid);
            Guid id = matchid;
            foreach (GameGolfer item in lastGame)
            {
                bool isExists = await _gameGolferRepo.CheckGolferInGame(gameid, item.GolferId);
                if (!isExists)
                {
                    GameGolferViewModel newGolferInGame = new GameGolferViewModel
                    {
                        Game = existingGame,
                        Golfer = item.Golfer,
                        Score = 0,
                    };
                    var addGolfer = _mapper.Map<GameGolfer>(newGolferInGame);
                    await _gameGolferRepo.Create(addGolfer);
                }
            }
            return RedirectToAction(nameof(Details), new { id });
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

        // POST: Match/Cancel/5
        public async Task<ActionResult> Complete(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Create Match: Model state is invalid");
                    return View();
                }

                Match match = await _matchRepo.FindByID(id);
                match.Status = true;
                bool isSuccess = await _matchRepo.Update(match);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Create Match: Unable to Complete match");
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Match/Cancel/5
        public async Task<ActionResult> Cancel(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Create Match: Model state is invalid");
                    return View();
                }

                Match match = await _matchRepo.FindByID(id);
                match.Status = false;
                bool isSuccess = await _matchRepo.Update(match);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Create Match: Unable to Cancel match");
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}