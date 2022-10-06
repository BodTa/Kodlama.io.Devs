using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models.GetListModel;
using Application.Features.OperationClaims.Queries.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
            GetListOperationClaimModel getListOperationClaimModel = await Mediator.Send(getListOperationClaimQuery);
            return Ok(getListOperationClaimModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreatedOperationClaimDto createdOperationClaimDto = await Mediator.Send(createOperationClaimCommand);
            return Ok(createdOperationClaimDto);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            DeletedOperationClaimDto result = await Mediator.Send(deleteOperationClaimCommand);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            UpdatedUserOperationClaimDto result = await Mediator.Send(updateOperationClaimCommand);
            return Ok(result);
        }
    }
}
