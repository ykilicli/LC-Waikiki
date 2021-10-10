using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lcw_Project.Context;
using Lcw_Project.Entities;
using Lcw_Project.ViewModel.CustomerOrderModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project.Controllers
{
    [Route("api/customers/{customerId:long}/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;
        private readonly LcWaikikiProjectContext _context;
        public OrderController(IMapper mapper, ILogger<OrderController> logger, LcWaikikiProjectContext context)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<CustomerOrderListResponseModel> GetList(long customerId)
        {
            var customer = _context.LcwCustomers.FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                _logger.LogError("Order/GetList müşteri bilgisi bulunamadı.");
                throw new KeyNotFoundException("customer is not found");
            }

            return _context.LcwCustomerOrders.Where(x => x.CustomerId == customerId).ProjectTo<CustomerOrderListResponseModel>(_mapper.ConfigurationProvider).ToArray();
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public CustomerOrderResponseModel GetById(long customerId, long id)
        {
            var customer = _context.LcwCustomers.FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                _logger.LogError("Order/GetById müşteri bilgisi bulunamadı.");
                throw new KeyNotFoundException("customer is not found");
            }

            return _context.LcwCustomerOrders.Where(x => x.CustomerId == customerId).ProjectTo<CustomerOrderResponseModel>(_mapper.ConfigurationProvider).FirstOrDefault(x => x.id == id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public CustomerOrderResponseModel Post(long customerId, [FromBody] CustomerOrderCreateRequestModel model)
        {
            if (model == null)
            {
                _logger.LogError("Customer/Post Request model boş.");
                throw new ArgumentNullException("request cannot be empty");
            }

            var customer = _context.LcwCustomers.FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                _logger.LogError("Order/GetById müşteri bilgisi bulunamadı.");
                throw new KeyNotFoundException("customer is not found");
            }

            if (string.IsNullOrWhiteSpace(model.address))
            {
                _logger.LogError("Order/GetById adres bilgisi boş geçilemez.");
                throw new InvalidOperationException("address cannot be empty");
            }

            var t = model.orderItems.FirstOrDefault(x => x.id <= 0 && (x.price <= 0 || x.productId == 0 || x.quantity <= 0));
            if (t != null)
            {
                if (t.quantity <= 0)
                {
                    _logger.LogError("Order/GetById miktar bilgisi sıfır veya sıfırdan küçük olamaz.");
                    throw new InvalidOperationException("quantity must be greater than zero");
                }
                if (t.price <= 0)
                {
                    _logger.LogError("Order/GetById fiyat bilgisi sıfır veya sıfırdan küçük olamaz.");
                    throw new InvalidOperationException("price must be greater than zero");
                }
                if (t.productId <= 0 || !_context.LcwProducts.Any(x => x.Id == t.productId))
                {
                    _logger.LogError("Order/GetById ürün bilgisi bulunamadı.");
                    throw new KeyNotFoundException("product is not found");
                }
            }

            var order = new LcwCustomerOrder()
            {
                CustomerId = model.customerId,
                Address = model.address,
                Date = model.date,
                LcwCustomerOrderItems = model.orderItems.ToList().Select(x => new LcwCustomerOrderItem
                {
                    ProductId = x.productId,
                    Quantity = x.quantity,
                    Price = x.price
                }).ToList()
            };
            _context.LcwCustomerOrders.Add(order);
            _context.SaveChanges();
            return GetById(customerId, order.Id);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public CustomerOrderResponseModel Put(long customerId, long id, [FromBody] CustomerOrderUpdateRequestModel model)
        {
            if (model == null)
            {
                _logger.LogError("Order/Put model bilgisi boş.");
                throw new ArgumentNullException("request cannot be empty");
            }

            var customer = _context.LcwCustomers.FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                _logger.LogError("Order/Put müşteri bilgisi bulunamadı.");
                throw new KeyNotFoundException("customer not found");
            }

            var data = _context.LcwCustomerOrders.Include(x => x.LcwCustomerOrderItems).FirstOrDefault(x => x.CustomerId == customerId && x.Id == id);
            if (data == null)
            {
                _logger.LogError("Order/Put sipariş bilgisi bulunamadı.");
                throw new KeyNotFoundException("order is not found");
            }

            var t = model.orderItems.FirstOrDefault(x => x.id <= 0 && (x.price <= 0 || x.productId == 0 || x.quantity <= 0));
            if (t != null)
            {
                if (t.quantity <= 0)
                {
                    _logger.LogError("Order/Put miktar bilgisi sıfır veya sıfırdan küçük olamaz.");
                    throw new InvalidOperationException("quantity must be greater than zero");
                }
                if (t.price <= 0)
                {
                    _logger.LogError("Order/Put fiyat bilgisi sıfır veya sıfırdan küçük olamaz.");
                    throw new InvalidOperationException("price must be greater than zero");
                }
                if (t.productId <= 0 || !_context.LcwProducts.Any(x => x.Id == t.productId))
                {
                    _logger.LogError("Order/Put ürün bilgisi bulunamadı.");
                    throw new KeyNotFoundException("product is not found");
                }
            }

            if (string.IsNullOrWhiteSpace(model.address))
            {
                throw new InvalidOperationException("address cannot be empty");
            }

            data.Address = model.address;
            //delet order detail
            var deleteItems = data.LcwCustomerOrderItems.Where(x => !model.orderItems.Any(y => y.id == x.Id));
            foreach (var item in deleteItems)
            {
                _context.LcwCustomerOrderItems.Remove(item);
            }
            //insert order detail
            foreach (var item in model.orderItems.Where(x => x.id <= 0).Select(x => new LcwCustomerOrderItem { ProductId = x.productId, Quantity = x.quantity, Price = x.price }))
            {
                data.LcwCustomerOrderItems.Add(item);
            }
            //update order detail
            foreach (var item in model.orderItems.Where(x => x.id > 0))
            {
                var itemData = data.LcwCustomerOrderItems.FirstOrDefault(x => x.Id == item.id);
                if (itemData != null)
                    itemData.Quantity = item.quantity;
            }

            _context.SaveChanges();
            return GetById(customerId, id);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public void Delete(long customerId, long id)
        {
            var customer = _context.LcwCustomers.FirstOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                _logger.LogError("Order/Delete müşteri bilgisi bulunamadı.");
                throw new KeyNotFoundException("customer is not found");
            }

            var data = _context.LcwCustomerOrders.Include(x => x.LcwCustomerOrderItems).FirstOrDefault(x => x.CustomerId == customerId && x.Id == id);
            if (data == null)
            {
                _logger.LogError("Order/Delete sipariş bilgisi bulunamadı.");
                throw new KeyNotFoundException("order is not found");
            }

            var deleteItems = data.LcwCustomerOrderItems;
            foreach (var item in deleteItems)
            {
                _context.LcwCustomerOrderItems.Remove(item);
            }

            _context.LcwCustomerOrders.Remove(data);
            _context.SaveChanges();
        }

    }
}
