using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lcw_Project.Context;
using Lcw_Project.Entities;
using Lcw_Project.ViewModel.ProductModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lcw_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;
        private readonly LcWaikikiProjectContext _context;
        public ProductController(IMapper mapper, ILogger<CustomerController> logger, LcWaikikiProjectContext context)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IEnumerable<ProductListResponseModel> GetList()
        {
            return _context.LcwProducts.ProjectTo<ProductListResponseModel>(_mapper.ConfigurationProvider);
        }
        [HttpGet("{id:long}")]
        public ProductResponseModel GetById(long id)
        {
            if (id <= 0)
            {
                _logger.LogError("Product/GetById Id değeri sıfır veya sıfırdan küçük.");
                throw new ArgumentNullException(nameof(id));
            }
            return _context.LcwProducts.ProjectTo<ProductResponseModel>(_mapper.ConfigurationProvider).FirstOrDefault(x => x.id == id);

        }
        [HttpPost]
        public ProductResponseModel Post([FromBody] ProductCreateRequestModel model)
        {
            if (model == null)
            {
                _logger.LogError("Product/Post Request model boş.");
                throw new ArgumentNullException("request cannot be empty");
            }
            LcwProduct entity = _mapper.Map<LcwProduct>(model);
            _context.LcwProducts.Add(entity);
            _context.SaveChanges();
            return GetById(entity.Id);
        }

        [HttpPut("{id:long}")]
        public ProductResponseModel Put(long id, [FromBody] ProductUpdateRequestModel model)
        {
            if (model == null)
            {
                _logger.LogError("Product/Put Request model boş.");
                throw new ArgumentNullException("request cannot be empty");
            }
            if (id <= 0)
            {
                _logger.LogError("Product/Put Id değeri sıfır veya sıfırdan küçük.");
                throw new ArgumentNullException(nameof(id));
            }
            var entity = _context.LcwProducts.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogError("Product/Put Kayıt veritabanında bulunamadı.");
                throw new KeyNotFoundException("product is not found");
            }
            _mapper.Map(model, entity);
            _context.SaveChanges();
            return GetById(entity.Id);
        }

        [HttpDelete("{id:long}")]
        public bool Delete(long id)
        {
            if (id <= 0)
            {
                _logger.LogError("Product/Delete Id değeri sıfır veya sıfırdan küçük.");
                throw new ArgumentNullException(nameof(id));
            }
            var entity = _context.LcwProducts.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogError("Product/Delete Kayıt veritabanında bulunamadı.");
                throw new KeyNotFoundException("product is not found");
            }
            _context.LcwProducts.Remove(entity);
            int retn = _context.SaveChanges();
            return retn > 0;
        }
    }
}
