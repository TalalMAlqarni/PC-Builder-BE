using AutoMapper;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.ProductDTO;

namespace src.Services.product
{
    public class ProductService : IProductService
    {
        protected readonly ProductRepository _productRepository;
        protected readonly IMapper _mapper;

        public ProductService(ProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<CreateProductDto, Product>(createProductDto);

            var newProduct = await _productRepository.AddProductAsync(product);

            return _mapper.Map<Product, GetProductDto>(newProduct);
        }

        public async Task<List<GetProductDto>> GetAllProductsAsync()
        {
            var productsList = await _productRepository.GetAllProductsAsync();
            return _mapper.Map<List<Product>, List<GetProductDto>>(productsList);
        }

        public async Task<List<GetProductDto>> GetAllBySearchAsync( // if the result of the search is null ,
            PaginationOptions paginationOptions
        )
        {
            var productsList = await _productRepository.GetAllResults(paginationOptions);
            if (productsList is null)
            {
                throw CustomException.NotFound("No results found");
            }
            return _mapper.Map<List<Product>, List<GetProductDto>>(productsList);
        }

        public async Task<GetProductDto> GetProductByIdAsync(Guid id)
        {
            var isFound = await _productRepository.GetProductByIdAsync(id);
            if (isFound is null)
            {
                throw CustomException.NotFound($"Product with {id} not found");
            }
            return _mapper.Map<Product, GetProductDto>(isFound);
        }

        public async Task<GetProductDto> UpdateProductInfoAsync(
            Guid id,
            UpdateProductInfoDto product
        )
        {
            var isFound = await _productRepository.GetProductByIdAsync(id);

            if (isFound is null)
            {
                throw CustomException.NotFound($"Product with {id} not found");
            }
            _mapper.Map(product, isFound);
            var updatedProduct = await _productRepository.UpdateProductInfoAsync(isFound);
            return _mapper.Map<Product, GetProductDto>(updatedProduct);
        }

        //delete product
        public async Task<bool> DeleteProductByIdAsync(Guid id)
        {
            var isFound = await _productRepository.GetProductByIdAsync(id);

            if (isFound is null)
            {
                throw CustomException.NotFound($"Product with {id} not found");
            }

            await _productRepository.DeleteProductAsync(isFound);
            return true;
        }

        //test sort functionality:
        // public async Task<List<GetProductDto>>GetAllBySortAsync(SortOptions sortOptions){

        //     var result = await _productRepository.


        // }

        // public async Task<bool>UpdateProductDescAsync(Guid id , UpdateProdouctDescDto updateProductDescDto){

        //     var isFound = await _productRepository.GetProductByIdAsync(id);
        //     if (isFound is null){

        //         return false;
        //     }

        //   await _productRepository.UpdateProductDescAsync(updateProductDescDto);

        //   return true;


        // }
    }
}
