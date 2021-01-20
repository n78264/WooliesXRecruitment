using API.Contracts;
using API.Extensions;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IShoppingTrolleyService _shoppingTrolleyService;
        private readonly IUserService _userService;
        public HomeController(IProductService productService, IShoppingTrolleyService shoppingTrolleyService,
                              IUserService userService)
        {
            _productService = productService ??
                throw new ArgumentNullException(nameof(productService));
            _shoppingTrolleyService = shoppingTrolleyService ??
                throw new ArgumentNullException(nameof(shoppingTrolleyService));
            _userService = userService;
        }

        [HttpGet]
        public IActionResult User()
        {
            try
            {
                var user = _userService.GetUser();
                if (user == null) return NotFound("Couldn't find User!");
                return Ok(user);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Application Failure");
            }
        }

        [HttpGet]
        [Route("{sortoption}")]
        public async Task<IActionResult> Sort([FromQuery] string sortoption)
        {
            try
            {
                SortOption parsedSortOption;
                if (Enum.TryParse<SortOption>(sortoption, true, out parsedSortOption))
                {
                    var products = await _productService.GetProducts(parsedSortOption);
                    if (products.IsNullOrEmpty()) 
                        return NotFound("Couldn't find any products!");

                    return Ok(products);
                }
                else
                    return BadRequest("Issue parsing query sort parameters");
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Application Failure");
            }
        }

        [HttpPost]
        public IActionResult TrolleyCalculator(ShoppingTrolley shoppingTrolley)
        {
            try
            {
                var total = _shoppingTrolleyService.GetLowestTrolleyTotal(shoppingTrolley);
                return Ok(total);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Application Failure");
            }
        }
    }
}
