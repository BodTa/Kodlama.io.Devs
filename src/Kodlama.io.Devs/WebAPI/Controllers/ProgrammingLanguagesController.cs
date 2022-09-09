using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.ProgrammingLanguageGetById;
using Application.Features.ProgrammingLanguages.Queries.ProgrammingLanguageGetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            ProgrammingLanguageGetListQuery programmingLanguageGetListQuery = new() { PageRequest = pageRequest };
            ProgrammingLanguageGetListModel result = await Mediator.Send(programmingLanguageGetListQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
       public async Task<IActionResult> GetById([FromRoute] ProgrammingLanguageGetByIdQuery programmingLanguageGetByIdQuery)
        {
            ProgrammingLanguageGetByIdDto programmingLanguageGetByIdDto =await  Mediator.Send(programmingLanguageGetByIdQuery);
            return Ok(programmingLanguageGetByIdDto);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguage)
        {
            CreatedProgrammingLanguageDto createdProgrammingLanguageDto = await Mediator.Send(createProgrammingLanguage);
            return Created("", createdProgrammingLanguageDto);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
        {
            UpdatedProgrammingLanguageDto updatedProgrammingLanguageDto = await Mediator.Send(updateProgrammingLanguageCommand);
            return Ok(updatedProgrammingLanguageDto);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
        {
            DeletedProgrammingLanguageDto deletedProgrammingLanguageDto = await Mediator.Send(deleteProgrammingLanguageCommand);
            return Ok(deletedProgrammingLanguageDto);  
        }

    }
}
