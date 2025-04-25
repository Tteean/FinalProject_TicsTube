using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateAsync(ProductCreateDto productCreateDto);
        Task<List<ProductReturnDto>> GetAllAsync();
        Task<int> UpdateAsync(int id, ProductUpdateDto productUpdateDto);
        Task<int> DeleteAsync(int id);
    }
}
