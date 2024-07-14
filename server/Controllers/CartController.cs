using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PizzaDev.Helpers;
using PizzaDev.Interfaces;
using PizzaDev.Interfaces.Repository;
using PizzaDev.Interfaces.Service;
using PizzaDev.Mappers;
using PizzaDev.Models;

namespace PizzaDev.Controllers;

[ApiController]
[Route("api/cart")]

public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }    
    
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var cart = await _cartService.GetCartAsync(1);
        return Ok(cart);
    }

    [HttpPost("add/{pizzaId:int}")]
    public async Task<IActionResult> AddPizzaToCart([FromRoute] int pizzaId, [FromQuery] PizzaAddToCartQueryParams requestParams)
    {
        var addedItem = await _cartService.AddItemAsync(pizzaId, requestParams);
        if (addedItem == null)
        {
            return NotFound("Pizza does not exists");
        }
        return Ok(addedItem.FromCartItemToDto());
    }

    [HttpDelete("remove/{pizzaId:int}")]
    public async Task<IActionResult> RemovePizzaFromCart([FromRoute] int pizzaId, [FromQuery] PizzaAddToCartQueryParams requestParams)
    {
        var deleted = await _cartService.RemoveItemAsync(pizzaId, false, requestParams);
        if (!deleted) return BadRequest("Pizza does not found");
        return NoContent();
    }

    [HttpDelete("removeAll/{pizzaId:int}")]
    public async Task<IActionResult> RemoveAllPizzasFromCart([FromRoute] int pizzaId, [FromQuery] PizzaAddToCartQueryParams requestParams)
    {
        var deleted = await _cartService.RemoveItemAsync(pizzaId, true, requestParams);
        if (!deleted) return BadRequest("Pizza does not found");
        return NoContent();
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> ClearCart()
    {
        var cleared = await _cartService.ClearCartAsync(1);
        if (!cleared) return BadRequest();
        
        return NoContent();
    }
    
    
}