using Application.Features.Teches.Commands.CreateTech;
using Application.Features.Teches.Commands.DeleteTech;
using Application.Features.Teches.Commands.UpdateTech;
using Application.Features.Teches.Dtos;
using Application.Features.Teches.Models;
using Application.Features.Teches.Queries.GetListTech;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TechsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTechQuery techGetListQuery = new() { PageRequest = pageRequest };
        GetListTechModel result = await Mediator.Send(techGetListQuery);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTechCommand createTechCommand)
    {
       CreatedTechDto createdTechDto = await Mediator.Send(createTechCommand);
        return Created("", createdTechDto);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTechCommand updateTechCommand)
    {
        UpdatedTechDto updatedTechDto = await Mediator.Send(updateTechCommand);
        return Ok(updatedTechDto);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteTechCommand deleteTechCommand)
    {
        DeletedTechDto deletedTechDto = await Mediator.Send(deleteTechCommand);
        return Ok(deletedTechDto);
    }
}
