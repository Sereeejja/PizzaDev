using Microsoft.AspNetCore.Mvc;
using PizzaDev.Dtos;
using PizzaDev.Helpers;
using PizzaDev.Interfaces;
using PizzaDev.Interfaces.Service;
using PizzaDev.Mappers;

namespace PizzaDev.Controllers;

[ApiController]
[Route("api/pizza")]

public class PizzaController : ControllerBase
{
    private readonly IPizzaService _pizzaService;
    
    public PizzaController(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PizzaQueryParams queryParams)
    {
        /*var (pizzas, pages) = await _pizzaRepo.GetAllAsync(queryParams);*/
        var (pizzas, pages) = await _pizzaService.GetAllAsync(queryParams);
        
        var response = new { Data = pizzas, Pages = pages };
        return Ok(response);
    }

    [HttpGet("{pizzaId:int}")]
    public async Task<IActionResult> GetOne([FromRoute] int pizzaId)
    {
        var pizza = await _pizzaService.GetOneAsync(pizzaId);
        if (pizza == null) return NotFound("Pizza does not exists");
        return Ok(pizza.PizzaToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePizzaRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var created = await _pizzaService.CreateAsync(request);
        if (created == null) return BadRequest();
        return CreatedAtAction(nameof(GetOne), new { pizzaId = created.Id }, created.PizzaToDto());
    }

    [HttpDelete("{pizzaId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int pizzaId)
    {
        var deleted = await _pizzaService.DeleteAsync(pizzaId);
        if (!deleted) return BadRequest();

        return NoContent();
    }

    [HttpPost("{pizzaId:int}")]
    public async Task<IActionResult> Edit([FromRoute] int pizzaId, [FromBody] EditPizzaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest("Bad pizza params");
        var pizza = await _pizzaService.EditAsync(pizzaId, request);
        if (pizza == null) return BadRequest();

        return Ok(pizza.PizzaToDto());
    }

    [HttpPost("size/{pizzaId:int}/{sizeId:int}")]
    public async Task<IActionResult> AddSize([FromRoute] int pizzaId, [FromRoute] int sizeId)
    {
        /*var createdPizzaSize = await _pizzaRepo.AddSizeAsync(pizzaId, sizeId);*/
        var createdPizzaSize = await _pizzaService.AddSizeAsync(pizzaId, sizeId);
        if (createdPizzaSize == null) return BadRequest("Unknown pizza or size");
        return StatusCode(201, createdPizzaSize.PizzaSizeToDto());
    }
    
}