using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Fore_Golf.Models;
using Fore_Golf.Contracts;
using AutoMapper;
using Fore_Golf.Data;

namespace Fore_Golf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMatchRepository _matchRepo;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMatchRepository matchRepo, IMapper mapper)
        {
            _logger = logger;
            _matchRepo = matchRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<Match> matches = await _matchRepo.FindAll();
            ICollection<MatchViewModel> model = _mapper.Map<ICollection<MatchViewModel>>(matches);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
