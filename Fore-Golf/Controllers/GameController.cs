﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fore_Golf.Contracts;
using Fore_Golf.Data;
using Fore_Golf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Fore_Golf.Controllers
{
    public class GameController : Controller
    {
        private readonly IGolferRepository _golferRepo;
        private readonly IGameRepository _gameRepo;
        private readonly IMatchRepository _matchRepo;
        private readonly IGameGolferRepository _gameGolferRepo;
        private readonly IMapper _mapper;

            public GameController(
                IGolferRepository golferRepo, 
                IGameRepository gameRepo, 
                IMatchRepository matchRepo,
                IGameGolferRepository gameGolferRepo, 
                IMapper mapper)
        {
            _golferRepo = golferRepo;
            _gameRepo = gameRepo;
            _matchRepo = matchRepo;
            _gameGolferRepo = gameGolferRepo;
            _mapper = mapper;
        }

        // GET: Game
        public async Task<ActionResult> Index()
        {
            var games = await _gameRepo.FindAll();
            var model = _mapper.Map<List<Game>, List<GameViewModel>>(games.ToList());
            return View(model);
        }

        // GET: Game/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var game = await _gameRepo.FindByID(id);
            var model = _mapper.Map<GameViewModel>(game);
            return View(model);
        }

        // GET: Game/Create
        public async Task<ActionResult> Create(Guid matchId)
        {
            var match = await _matchRepo.FindByID(matchId);
            var model = new GameViewModel()
            {
                MatchId = matchId,
                GameDate = DateTime.Now,
                Location = match.Location,
                Match = match
            };
            
            return View(model);
        }

        // POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GameViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Create Game: Model state is invalid");
                    return View(model);
                }
                if (model.MatchId == null)
                {
                    ModelState.AddModelError("", "Create Game: No Match ID");
                    return View(model);
                }
                
                model.Id = Guid.NewGuid();
                var match = await _matchRepo.FindByID(model.MatchId);
                //var game = _mapper.Map<Game>(model);
                var game = new Game
                {
                    Id = model.Id,
                    GameDate = model.GameDate,
                    Match = match,
                    Location = model.Location
                };  

                var isSuccess = await _gameRepo.Create(game);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Create Game: Unable to Create a new game");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Create Game: Something went wrong in the game creation");
                return View(model);
            }
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Game/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Game/Delete/5
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

        public async Task<ActionResult> ListGolfers(Guid id)
            {
                var isExists = await _gameRepo.IsExists(id);
                if (!isExists)
                {
                    return NotFound();
                }

                var model = new List<GameGolferStatusViewModel>();

                var golfers = await _golferRepo.FindAll();
                var gameGolfers = (await _gameGolferRepo.FindAllGolfersInGame(id)).Select(gg => gg.Golfer.Id);

                foreach (var golfer in golfers)
                {
                    model.Add(new GameGolferStatusViewModel()
                    {
                        GameId = id,
                        Golfer = _mapper.Map<Golfer, GolferViewModel>(golfer),
                        IsInGame = gameGolfers.Contains(golfer.Id),
                    });
                }

                return View(model);
            }

        public async Task<ActionResult> AddGolferToGame(Guid id, Guid golferid)
        {
            var isExists = await _gameGolferRepo.CheckGolferInGame(id, golferid);
            if (!isExists)
            {
                var newGolferInGame = new GameGolferViewModel
                {
                    Game = await _gameRepo.FindByID(id),
                    Golfer = await _golferRepo.FindByID(golferid),
                };
                var addGolfer = _mapper.Map<GameGolfer>(newGolferInGame);
                await _gameGolferRepo.Create(addGolfer);
            }

            return RedirectToAction(nameof(ListGolfers));
        }
    }
}