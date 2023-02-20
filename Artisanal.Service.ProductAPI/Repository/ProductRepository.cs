using Artisanal.Service.ProductAPI.DbContexts;
using Artisanal.Service.ProductAPI.Models;
using Artisanal.Service.ProductAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Artisanal.Service.ProductAPI.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
           // Product product =  await _db.Products.Where(x => x.ProductId ==productDto.ProductId).FirstOrDefaultAsync();
           Product pr = _mapper.Map<Product>(productDto);
            if (pr.ProductId <= 0) {
                
            
                _db.Products.Add(pr);
                _db.SaveChanges();
                 }

            else {
                _db.Products.Update(pr);
                _db.SaveChanges(); }
            
            return _mapper.Map<ProductDto>(pr); ;
        }

        public async Task <bool> DeleteProduct(int productId)
        {
            Product product = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            if (product == null) { return false; } else {
                _db.Products.Remove(product);
                _db.SaveChanges(); return true;
                
            }
           
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {

            List<Product> productList = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }
    }
}
