using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_DataAccess.Data;
using FinalProject_Service.Dto.ActorDtos;
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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly TicsTubeDbContext _context;
        private readonly IMapper _mapper;
        private readonly PhotoService _photoService;

        public ProductService(TicsTubeDbContext context, IMapper mapper, PhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        public async Task<int> CreateAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            if (productCreateDto.MovieOrShow == "movie")
            {
                if (productCreateDto.MovieId == null || !_context.Movies.Any(m => m.Id == productCreateDto.MovieId))
                {
                    throw new CustomException(404, "Movie", "Movie not found");
                }
                product.MovieProducts.Add(new MovieProduct { MovieId = productCreateDto.MovieId.Value });
            }
            else if (productCreateDto.MovieOrShow == "tvshow")
            {
                if (productCreateDto.TVShowId == null || !_context.TVShows.Any(t => t.Id == productCreateDto.TVShowId))
                {
                    throw new CustomException(404, "TVShow", "TV Show not found");
                }
                product.tvShowProducts.Add(new TvShowProduct { TvShowId = productCreateDto.TVShowId.Value });
            }
            else
            {
                throw new CustomException(400, "InvalidType", "Invalid type selected");
            }
            if (productCreateDto.File != null)
            {
                product.Image = await _photoService.UploadImageAsync(productCreateDto.File);

            }
            _context.Products.Add(product);
            return _context.SaveChanges();
        }
        public async Task<int> DeleteAsync(int id)
        {
            var existProduct = await _context.Products
       .Include(p => p.tvShowProducts)
       .ThenInclude(p => p.TVShow)
       .Include(p => p.MovieProducts)
       .ThenInclude(p => p.Movie)
       .FirstOrDefaultAsync(p => p.Id == id);

            if (existProduct == null)
            {
                throw new CustomException(404, "Product", "Product not found");
            }

            if (!string.IsNullOrEmpty(existProduct.Image))
            {
                await _photoService.DeleteAsync(existProduct.Image);
            }


            if (existProduct.MovieProducts.Any())
            {
                _context.MovieProducts.RemoveRange(existProduct.MovieProducts);
            }
            if (existProduct.tvShowProducts.Any())
            {
                _context.tvShowProducts.RemoveRange(existProduct.tvShowProducts);
            }

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
            _context.MovieProducts.RemoveRange(existProduct.MovieProducts);
            existProduct.MovieProducts.Clear();

            _context.tvShowProducts.RemoveRange(existProduct.tvShowProducts);
            existProduct.tvShowProducts.Clear();

            if (productUpdateDto.MovieOrShow == "movie")
            {
                if (productUpdateDto.MovieId == null)
                {
                    throw new CustomException(400, "Movie", "Movie must be selected");
                }

                if (!_context.Movies.Any(m => m.Id == productUpdateDto.MovieId.Value))
                {
                    throw new CustomException(404, "Movie", "Movie not found");
                }

                existProduct.MovieProducts.Add(new MovieProduct { MovieId = productUpdateDto.MovieId.Value });
            }
            else if (productUpdateDto.MovieOrShow == "tvshow")
            {
                if (productUpdateDto.TVShowId == null)
                {
                    throw new CustomException(400, "TV Show", "TV Show must be selected");
                }

                if (!_context.TVShows.Any(t => t.Id == productUpdateDto.TVShowId.Value))
                {
                    throw new CustomException(404, "TV Show", "TV Show not found");
                }

                existProduct.tvShowProducts.Add(new TvShowProduct { TvShowId = productUpdateDto.TVShowId.Value });
            }
            else
            {
                throw new CustomException(400, "ContentType", "Invalid content type");
            }

            if (productUpdateDto.File != null)
            {
                if (!string.IsNullOrEmpty(existProduct.Image))
                {
                    await _photoService.DeleteAsync(existProduct.Image);
                }
                existProduct.Image = await _photoService.UploadImageAsync(productUpdateDto.File);
            }

            return await _context.SaveChangesAsync();
        }
    }
}
