using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.ProductDtos;
using FinalProject_Service.Dto.TVShowDtos;
using FinalProject_Service.Exceptions;
using FinalProject_Service.Extentions;
using FinalProject_Service.Services.Interfaces;
using FinalProject_ViewModel.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(TicsTubeDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(ProductCreateDto productCreateDto)
        {
            foreach (var genreId in productCreateDto.TVShowId)
            {
                if (!_context.Genres.Any(t => t.Id == genreId))
                {
                    throw new CustomException(404, "Genre", "Genre not found");
                }
            }

            foreach (var languageId in productCreateDto.MovieId)
            {
                if (!_context.Languages.Any(s => s.Id == languageId))
                {
                    throw new CustomException(404, "Language", "Language not found");
                }
            }
            
            var product = _mapper.Map<Product>(productCreateDto);
            foreach (var movieId in productCreateDto.MovieId)
            {
                product.MovieProducts.Add(new MovieProduct { MovieId = movieId });
            }
            foreach (var tvShowId in productCreateDto.TVShowId)
            {
                product.tvShowProducts.Add(new TvShowProduct { TvShowId = tvShowId });
            }
            if (productCreateDto.File != null)
            {
                product.Image = productCreateDto.File.SaveImage("uploads/Product");
            }
            _context.Products.Add(product);
            return _context.SaveChanges();
        }
        public async Task<int> DeleteAsync(int id)
        {
            var existProduct = await _context.Products
                .Include(p => p.tvShowProducts)
                .ThenInclude(p=>p.TVShow)
                .Include(p => p.MovieProducts)
                .ThenInclude(p=>p.Movie)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existProduct == null)
            {
                throw new CustomException(404, "Movie", "Movie not found");
            }
            MovieDeleteDto movieDeleteDto = new MovieDeleteDto();
            _mapper.Map(movieDeleteDto, existProduct);
            if (movieDeleteDto.File != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "movies", existProduct.Image);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
            _context.MovieProducts.RemoveRange(existProduct.MovieProducts);
            _context.tvShowProducts.RemoveRange(existProduct.tvShowProducts);
            _context.Products.Remove(existProduct);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductReturnDto>> GetAllAsync()
        {
            var existProduct = await _context.Products
                 .Include(p => p.tvShowProducts)
                 .ThenInclude(p => p.TVShow)
                 .Include(p => p.MovieProducts)
                 .ThenInclude(p => p.Movie).ToListAsync();
            return _mapper.Map<List<ProductReturnDto>>(existProduct);
        }

        public async Task<int> UpdateAsync(int id, ProductUpdateDto productUpdateDto)
        {
            var existProduct = await _context.Products
                 .Include(p => p.tvShowProducts)
                 .ThenInclude(p => p.TVShow)
                 .Include(p => p.MovieProducts)
                 .ThenInclude(p => p.Movie)
                 .FirstOrDefaultAsync(p => p.Id == id);
            if (existProduct == null)
            {
                throw new CustomException(404, "Movie", "Movie not found");
            }
            _mapper.Map(productUpdateDto, existProduct);
            foreach (var movieProduct in existProduct.MovieProducts.ToList())
            {
                _context.MovieProducts.Remove(movieProduct);
                existProduct.MovieProducts.Remove(movieProduct);
            }
            foreach (var movieId in productUpdateDto.MovieId ?? new())
            {
                if (!_context.Movies.Any(t => t.Id == movieId))
                {
                    throw new CustomException(404, "Actor", "Actor not found");
                }
                existProduct.MovieProducts.Add(new MovieProduct { MovieId = movieId });
            }
            foreach (var tvShow in existProduct.tvShowProducts.ToList())
            {
                _context.tvShowProducts.Remove(tvShow);
                existProduct.tvShowProducts.Remove(tvShow);
            }
            foreach (var tvShowId in productUpdateDto.TVShowId ?? new())
            {
                if (!_context.TVShows.Any(t => t.Id == tvShowId))
                {
                    throw new CustomException(404, "Genre", "Genre not found");
                }
                existProduct.tvShowProducts.Add(new TvShowProduct { TvShowId = tvShowId });
            }
            if (productUpdateDto.File != null)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "movies", existProduct.Image);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                existProduct.Image = productUpdateDto.File.SaveImage("uploads/movies");
            }
            return await _context.SaveChangesAsync();
        }
    }
}
