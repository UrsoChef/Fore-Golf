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
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

                IEnumerable<GameGolfer> lastGame = await _gameGolferRepo.FindLatestGameandGolfersInMatch(game.MatchId, game.Id);
                foreach (GameGolfer item in lastGame)
                {
                    bool isExists = await _gameGolferRepo.CheckGolferInGame(game.Id, item.GolferId);
                    if (!isExists)
                    {
                        GameGolferViewModel newGolferInGame = new GameGolferViewModel
                        {
                            Game = game,
                            Golfer = item.Golfer,
                            Score = 0,
                        };
                        var addGolfer = _mapper.Map<GameGolfer>(newGolferInGame);
                        bool golferSuccess = await _gameGolferRepo.Create(addGolfer);

                        if (!golferSuccess)
                        {
                            ModelState.AddModelError("", "Create Game: Unable to Copy a previous list of Golfers");
                            continue;
                        }
                    }
                }


                //// ```pull all gameGolfer rows for a specific match
                //IEnumerable<GameGolfer> gameGolfers = await _db.GameGolfers.Include(g => g.Golfer).Where(q => q.Game.MatchId == matchid).ToListAsync();
                //                // give me the last game played from the list of games in a match, EXCLUDING the new game I just created *gameid*
                //                GameGolfer latestGame = gameGolfers.Where(g => g.GameId != gameid).Last();
                //                // return all golfers for the latest game I just created so that I can cope them over
                //                return gameGolfers.Where(gg => gg.GameId == latestGame.GameId);```
                Guid id = game.MatchId;
                return RedirectToAction(nameof(Details), "Match", id);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Create Game: Something went wrong in the game creation");
                return View(model);
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
                Game game = await _gameRepo.FindByID(id);

                foreach (var golfer in golfers)
                {
                    model.Add(new GameGolferStatusViewModel()
                    {
                        MatchId = game.MatchId,
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

            return RedirectToAction(nameof(ListGolfers), new { id });
        }

        public async Task<ActionResult> RemoveGolferFromGame(Guid id, Guid golferid)
        {
            var isExists = await _gameGolferRepo.CheckGolferInGame(id, golferid);
            if (isExists)
            {
                var golfer = await _gameGolferRepo.FindGolferInGame(id, golferid);
                var removeGolfer = _mapper.Map<GameGolfer>(golfer);
                await _gameGolferRepo.Delete(removeGolfer);
            }

            return RedirectToAction(nameof(ListGolfers), new { id });
        }

        // GET: Game/ViewScores
        public async Task<ActionResult> ViewScores(Guid id)
        {
            var isExists = await _gameGolferRepo.CheckGame(id);
            if (!isExists)
            {
                return NotFound();
            }

            var gameGolfers = await _gameGolferRepo.FindAllGolfersInGame(id);
            GameScoresViewModel model = new GameScoresViewModel()
            {
                GameId = id,
                MatchId = gameGolfers.First().Game.MatchId,
                GolferScores = _mapper.Map<IEnumerable<GolferScoreViewModel>>(gameGolfers),
            };

            return View(model);
        }

        // GET: Game/SetScore
        public async Task<ActionResult> SetScore(Guid id)
        {
            var isExists = await _gameGolferRepo.CheckGame(id);
            if (!isExists)
            {
                return NotFound();
            }

            IEnumerable<GameGolfer> gameGolfers = await _gameGolferRepo.FindAllGolfersInGame(id);
            Game game = await _gameRepo.FindByID(id);
            ICollection<SetScoreViewModel> model = _mapper.Map<ICollection<SetScoreViewModel>>(gameGolfers);
            foreach (var item in model)
            {
                item.MatchId = game.MatchId;
            };
            return View(model);
        }

        // POST: Game/SetScore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetScore(SetScoreViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Set Scores: Model state is invalid");
                    return View(model);
                }
                if (model.Id == null)
                {
                    ModelState.AddModelError("", "Set Scores: No ID");
                    return View(model);
                }
                var golferGameIds = await _gameGolferRepo.FindGolferInGame(model.GameId, model.GolferId);
                model.Id = golferGameIds.Id;
                var gameGolfer = await _gameGolferRepo.FindByID(model.Id);
                gameGolfer.Score = model.Score;
                var isSuccess = await _gameGolferRepo.Update(gameGolfer);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Set Scores: Unable to update the score");
                    return View(model);
                }
                return RedirectToAction(nameof(SetScore),model.GameId);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Set Scores: Something went wrong in setting the score");
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
    }
}