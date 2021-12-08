using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICityService cityService;
        private IMapper Mapper => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<OrderDTO, OrderViewModel>().ReverseMap();
            cfg.CreateMap<CityDTO, CityViewModel>().ReverseMap();
        }).CreateMapper();
        public OrderController(IOrderService orderService, ICityService cityService)
        {
            this.orderService = orderService;
            this.cityService = cityService;
        }

        public IActionResult Get(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = Mapper.Map<OrderDTO, OrderViewModel>(orderService.Get(id.Value));

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            var cities = Mapper.Map<IEnumerable<CityDTO>, List<CityViewModel>>(cityService.GetAll());
            ViewBag.CityId = new SelectList(cities, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await orderService.CreateAsync(Mapper.Map<OrderViewModel, OrderDTO>(model));

            return RedirectToAction("Get", new { id = model.Id });
        }
    }
}
