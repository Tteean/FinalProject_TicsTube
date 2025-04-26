using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ProductDtos;
using FinalProject_Service.Extentions;
using FinalProject_Service.Services.Implementations;
using FinalProject_Service.Services.Interfaces;
using FinalProject_ViewModel.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject_Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,moderator")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductService productService, TicsTubeDbContext context)
        {
            _mapper = mapper;
            _productService = productService;
            _context = context;
        }

        public IActionResult Index(int page = 1, int take = 2)
        {
            var query = _context.Products
                .Include(b => b.MovieProducts).ThenInclude(ps => ps.Movie)
                .Include(b => b.tvShowProducts).ThenInclude(pt => pt.TVShow);

            return View(PaginatedList<Product>.Create(query, take, page));
        }
        public IActionResult Create()
        {
            ViewBag.Movies = _context.Movies.ToList();
            ViewBag.TVShows = _context.TVShows.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto productCreateDto)
        {
            ViewBag.Movies = _context.Movies.ToList();
            ViewBag.TVShows = _context.TVShows.ToList();

            if (!ModelState.IsValid)
                return View();
            await _productService.CreateAsync(productCreateDto);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var existProduct = _context.Products
                .Include(b => b.MovieProducts)
                .ThenInclude(ps => ps.Movie)
                .Include(b => b.tvShowProducts)
                .ThenInclude(pt => pt.TVShow)
                .FirstOrDefault(p => p.Id == id);
            if (existProduct is null)
                return NotFound();


            ViewBag.Movies = _context.Movies.ToList();
            ViewBag.TVShows = _context.TVShows.ToList();
            var productUpdateDto = _mapper.Map<ProductUpdateDto>(existProduct);
            return View(productUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            await _productService.UpdateAsync(id, productUpdateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
