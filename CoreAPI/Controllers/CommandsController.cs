using AutoMapper;
using CoreAPI.Data;
using CoreAPI.Dtos;
using CoreAPI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using CoreAPI.Security;
using Microsoft.Extensions.Logging;
using CoreAPI.Utilities;

namespace CoreAPI.Controllers
{
    [ApiKey]    

    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommanderRepo _repository ;
 
        public CommandsController(ICommanderRepo _repository , IMapper mapper   )
        {
            this._repository = _repository;
            _mapper = mapper;
 
        }

        //GET api/commands

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            //   _logger.LogDebug("CommandController.GetAllCommands method called!!!");

            throw new Exception("test exception");
            var commandItems = _repository.GetAllCommands();
             return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }



        //GET api/commands/{id}
        //[Authorize]       //Apply this attribute to lockdown this ActionResult (or others)
        [ServiceFilter(typeof(CommandValidateEntityExistsAttribute<Command>))]

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            //var commandItem = HttpContext.Items["entity"] as Command;

            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        //POST api/commands/
        //[ServiceFilter(typeof(NotFoundValidationAttribute))]
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            // this bit of code is relaced by nofound validationattr
            if (commandCreateDto == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        //PUT api/commands/{id}
        //[ServiceFilter(typeof(NotFoundValidationAttribute))]
 
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            if (commandUpdateDto == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        //[ServiceFilter(typeof(NotFoundValidationAttribute))]

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}