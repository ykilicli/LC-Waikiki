using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lcw_Project.Context;
using Lcw_Project.Entities;
using Lcw_Project.ViewModel.CustomerModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lcw_Project.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;
        private readonly LcWaikikiProjectContext _context;
        public CustomerController(IMapper mapper, ILogger<CustomerController> logger, LcWaikikiProjectContext context)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<CustomerListResponseModel> GetList()
        {
            return _context.LcwCustomers.ProjectTo<CustomerListResponseModel>(_mapper.ConfigurationProvider);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public CustomerResponseModel GetById(long id)
        {
            if (id <= 0)
            {
                _logger.LogError("Customer/GetById Id değeri sıfır veya sıfırdan küçük.");
                throw new ArgumentNullException(nameof(id));
            }
            return _context.LcwCustomers.ProjectTo<CustomerResponseModel>(_mapper.ConfigurationProvider).FirstOrDefault(x => x.id == id);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public CustomerResponseModel Post([FromBody] CustomerCreateRequestModel model)
        {
            if (model == null)
            {
                _logger.LogError("Customer/Post Request model boş.");
                throw new ArgumentNullException("request cannot be empty");
            }
            LcwCustomer entity = _mapper.Map<LcwCustomer>(model);
            _context.LcwCustomers.Add(entity);
            _context.SaveChanges();
            return GetById(entity.Id);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public CustomerResponseModel Put(long id, [FromBody] CustomerUpdateRequestModel model)
        {
            if (model == null)
            {
                _logger.LogError("Customer/Put Request model boş.");
                throw new ArgumentNullException("request cannot be empty");
            }
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var entity = _context.LcwCustomers.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogError("Customer/Put Kayıt veritabanında bulunamadı.");
                throw new KeyNotFoundException("customer is not found");
            }

            _mapper.Map(model, entity);
            _context.SaveChanges();

            return GetById(entity.Id);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public bool Delete(long id)
        {
            if (id <= 0)
            {
                _logger.LogError("Customer/Delete Id değeri sıfır veya sıfırdan küçük.");
                throw new ArgumentNullException(nameof(id));
            }
            var entity = _context.LcwCustomers.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                _logger.LogError("Customer/Delete Kayıt veritabanında bulunamadı.");
                throw new KeyNotFoundException("customer is not found");
            }
            _context.LcwCustomers.Remove(entity);
            int retn = _context.SaveChanges();
            return retn > 0;
        } 
    }
}
