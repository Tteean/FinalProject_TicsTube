using FinalProject_Core.Enum;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Services.Implementations;
using FinalProject_Service.Services.Interfaces;
using FinalProject_ViewModel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Controllers
{
    public class MovieController : Controller
    {
        private readonly TicsTubeDbContext _context;
        private readonly IIMDBService _iMDBService;
        private readonly UserManager<AppUser> _userManager;

        public MovieController(TicsTubeDbContext context, IIMDBService iMDBService, UserManager<AppUser> userManager)
        {
            _context = context;
            _iMDBService = iMDBService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            MovieVm vm = new MovieVm();
            vm.Movies = _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieActors)
                .ThenInclude(mg => mg.Actor)
                .Include(m => m.MovieLanguages)
                .ThenInclude(mg => mg.Language)
                .ToList();
            vm.Genres = _context.Genres.ToList();
            return View(vm);
        }
        [Authorize(Roles = "month_member,halfYear_member,year_member")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            var existMovie = _context.Movies
                .Include(m => m.Directors)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieLanguages)
                .ThenInclude(ml => ml.Language)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .Include(m=>m.MovieComments)
                .FirstOrDefault(x => x.Id == id);
            if (existMovie == null)
                return NotFound();
            string imdbRating = await _iMDBService.GetIMDbRatingAsync(existMovie.Title);
            ViewBag.Rating = imdbRating;
            return View(existMovie);
        }
        [HttpPost]
        [Authorize(Roles = "month_member,halfYear_member,year_member")]
        public async Task<IActionResult> AddComment([FromBody ]MovieComment movieComment)
        {

            if (!_context.Movies.Any(m => m.Id == movieComment.MovieId))
            {
                return RedirectToAction("notfound", "error");
            }
            var user = await _userManager.GetUserAsync(User);

            if (user == null || !await _userManager.IsInRoleAsync(user, "member"))
            {
                return RedirectToAction("login", "Account", Url.Action("detail", "Movie", movieComment.MovieId));
            }

            var vm = getMovieDetailVm(movieComment.MovieId, user.Id);
            vm.MovieComment = movieComment;
            if (!ModelState.IsValid) return View("detail", vm);
            

            movieComment.AppUserId = user.Id;
            movieComment.CreationDate = DateTime.Now;
            _context.MovieComments.Add(movieComment);
            await _context.SaveChangesAsync();
            


            return RedirectToAction("Detail", new { id = movieComment.MovieId });
        }
        private MovieVm getMovieDetailVm(int movieId, string userId = null)
        {
            var existmovie = _context.Movies
                .Include(m => m.Directors)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieLanguages)
                .ThenInclude(ml => ml.Language)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
               .Include(m => m.MovieComments.Take(1)).ThenInclude(mc => mc.AppUser)
               .FirstOrDefault(x => x.Id == movieId);


            MovieVm movieVm = new MovieVm()
            {
                Movie = existmovie,
                RelatedMovies = _context.Movies
                    .Include(m => m.Directors)
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieLanguages)
                .ThenInclude(ml => ml.Language)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                    .Where(x => x.DirectorId == existmovie.DirectorId && x.Id != existmovie.Id)
                    .Take(4)
                    .ToList(),

            };
            if (userId != null)
            {
                movieVm.HasCommentUser = _context.MovieComments.Any(x => x.MovieId == existmovie.Id && x.AppUserId == userId && x.Status != CommentStatus.Rejected);

            }
            else
            {
                movieVm.HasCommentUser = _context.MovieComments.Any(x => x.MovieId == existmovie.Id && x.Status != CommentStatus.Rejected);

            }
            movieVm.TotalComments = _context.MovieComments.Count(x => x.MovieId == existmovie.Id);
            return movieVm;
        }
    }
}
