using Application.Features.SocialLinks.Commands.CreateSocialLink;
using Application.Features.SocialLinks.Commands.DeleteSocialLink;
using Application.Features.SocialLinks.Commands.UpdateSocialLink;
using Application.Features.SocialLinks.Dtos;
using Application.Features.SocialLinks.Models;
using Application.Features.SocialLinks.Queries.GetListSocialLink;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SocialLinksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSocialLinkCommand createSocialLinkCommand)
    {
        CreatedSocialLinkDto createdSocialLinkDto =await Mediator.Send(createSocialLinkCommand);
        return Created("", createdSocialLinkDto);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteSocialLinkCommand deleteSocialLinkCommand)
    {
        DeletedSocialLinkDto deletedSocialLinkDto = await Mediator.Send(deleteSocialLinkCommand);
        return Ok(deletedSocialLinkDto);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSocialLinkCommand updateSocialLinkCommand)
    {
        UpdatedSocialLinkDto updatedSocialLinkDto = await Mediator.Send(updateSocialLinkCommand);
        return Ok(updatedSocialLinkDto);
    }
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSocialLinkQuery query = new() { PageRequest = pageRequest };
        GetListSocialLinkModel getListSocialLinkModel = await Mediator.Send(query);
        return Ok(getListSocialLinkModel);
    }
}
