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
                List<Game> gamesInMatch = await _gameRepo.GetGamesInMatch(model.MatchId);
                int numberOfGames = gamesInMatch.Count();
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
                bool gameGolfersExist = await _gameGolferRepo.CheckGame(game.Id);
                if (!gameGolfersExist && numberOfGames > 0)
                {
                    List<GameGolfer> lastGame = await _gameGolferRepo.FindLatestGameandGolfersInMatch(game.MatchId, game.Id);
                    foreach (GameGolfer item in lastGame)
                    {
                        bool isExists = await _gameGolferRepo.CheckGolferInGame(game.Id, item.GolferId);
                        if (!isExists)
                        {
                            GameGolferViewModel newGolferInGame = new GameGolferViewModel
                            {
                                Game = game,
                                Golfer = item.Golfer,
                                GameHandicap = item.GameHandicap,
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
                }
                Guid id = game.MatchId;
                return RedirectToAction(nameof(Details), "Match", new { id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Create Game: Something went wrong in the game creation");
                return RedirectToAction(nameof(Index), "Match");
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
                Golfer golfer = await _golferRepo.FindByID(golferid);
                Game game = await _gameRepo.FindByID(id);
                GameGolferViewModel newGolferInGame = new GameGolferViewModel
                {
                    Game = game,
                    Golfer = golfer,
                    GameHandicap = golfer.Handicap,
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

            List<GameGolfer> gameGolfers = await _gameGolferRepo.FindAllGolfersInGame(id);
            List<GameGolferViewModel> test = _mapper.Map<List<GameGolferViewModel>>(gameGolfers);
            test = test.OrderBy(t => t.NetScore).ToList();
            GameGolferScoreViewModel model = new GameGolferScoreViewModel()
            {
                MatchId = gameGolfers.First().Game.MatchId,
                Games = gameGolfers.First().Game,
                GolferScores = test,
            };
            
            return View(model);
        }

        // GET: Game/SetScore
        public async Task<ActionResult> SetScore(Guid id)
        {
            var isExists = await _gameGolferRepo.CheckGame(id);
            if (!isExists)
            {
                Game noGolfers = await _gameRepo.FindByID(id);
                TempData["Message"] = "There are no golfers in this game";
                ModelState.AddModelError("", "Set Scores: Add golfers before setting their scores");
                return RedirectToAction(nameof(Details), "Match", new { Id = noGolfers.MatchId });
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
                GameGolfer gameGolfer = await _gameGolferRepo.FindByID(model.Id);
                gameGolfer.Score = model.Score;
                var isSuccess = await _gameGolferRepo.Update(gameGolfer);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Set Scores: Unable to update the score");
                    return View(model);
                }
                return RedirectToAction(nameof(SetScore), new { id = model.GameId });
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