using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Meet_Twitch.Core.Models;
using Meet_Twitch.Core.Data;
using Meet_Twitch.Core.Services;
using Meet_Twitch.ViewModels;
using Meet_Twitch.Core;
using Meet_Twitch.Core.Enums;
using Meet_Twitch.Core.Extensions;
using System;
using Meet_Twitch.Core.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Meet_Twitch.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITwitchRepository _twitchRepository;
        private readonly ITwitchService _twitchService;

        public HomeController(ILogger<HomeController> logger, ITwitchRepository twitchRepository, ITwitchService twitchService)
        {
            _logger = logger;
            _twitchRepository = twitchRepository;
            _twitchService = twitchService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new GamesAndLanguagesViewModel
            {
                Games = await _twitchRepository.GetTopGamesAsync(),
            };
            model.Games = model.Games.OrderBy(g => g.Name).ToList();

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> ProposeStreamers(string gameId,
                                                          TwitchLanguages language,
                                                          TwitchPeriods period = TwitchPeriods.Week,
                                                          TwitchSorts sort = TwitchSorts.Views)
        {
            var model = new StreamersAndVideos();
            var videos = new List<Video>();
            var streamers = new List<User>();
            Game game = null;

            try
            {
                if (gameId != null)
                {
                    videos = await _twitchRepository.GetVideosAsync(gameId, language, period, sort) ?? throw new DataNotFoundException("Getting Videos from Twitch API Failed");
                    game = await _twitchRepository.GetGameAsync(gameId) ?? throw new DataNotFoundException("Getting the Game from Twitch API Failed");
                }

                if (videos != null)
                {
                    List<string> creatorIds = FindVideosCreatorsIds(videos);
                    streamers = await _twitchRepository.GetUsers(creatorIds) ?? throw new DataNotFoundException("Getting Users from Twitch API Failed");
                    streamers.Take(12).OrderByDescending(u => u.ViewCount).ToList();
                    foreach (var video in videos)
                    {
                        video.ThumbnailUrl = _twitchService.SetTwitchUrlImageSize(video.ThumbnailUrl, 350, 200) ?? "";
                        video.Title = video.Title.ShortenTo(25);
                    }
                }

                model.Streamers = streamers;
                model.Videos = videos;
                model.Game = game;
                model.Language = language;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Home Controller - ProposeStreamers() {DateTime.Now} {ex.Message} {ex.InnerException}");
                throw;
            }
            return PartialView("_PropositionCarouselPartial", model);
        }

        private List<string> FindVideosCreatorsIds(List<Video> videos)
        {
            return videos.Select(v => v.UserId).ToList();
        } 



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
