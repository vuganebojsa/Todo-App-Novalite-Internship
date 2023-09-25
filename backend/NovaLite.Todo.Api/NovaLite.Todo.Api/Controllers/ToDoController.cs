using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaLite.Todo.Api.Exceptions;
using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.DTOs.Attachment;
using NovaLite.Todo.Core.DTOs.Reminder;
using NovaLite.Todo.Core.DTOs.TodoList;
using NovaLite.Todo.Core.Interfaces;

namespace NovaLite.Todo.Api.Controllers
{
    [Route("api/v1/lists")]
    [Authorize]
    [ApiController]
    public class ToDoController : BaseController
    {
        private readonly ITodoService _toDoService;

        public ToDoController(ITodoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDoLists()
        {
            try
            {
                var email = await GetUserEmail();

                var todolists = await _toDoService.GetAllLists(email);
                return Ok(todolists);


            }
            catch (NotFoundException exc)
            {
                return NotFound(exc.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoList(Guid id)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var email = await GetUserEmail();

                var todolist = await _toDoService.GetToDoListBy(id, email);
                return Ok(todolist);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] CreateToDoListDTO todoList)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var email = await GetUserEmail();
                var createdTdl = await _toDoService.Create(todoList, email);
                return Ok(createdTdl);
            }
            catch (Exception exception) { return BadRequest(exception.Message); }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateToDoList([FromBody] UpdateToDoListDTO toDoListDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var email = await GetUserEmail();
                var editedTdl = await _toDoService.Update(toDoListDTO, email);
                return Ok(editedTdl);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoList(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var email = await GetUserEmail();

                bool isDeleted = await _toDoService.Delete(id, email);
                return Ok(isDeleted);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }

        [HttpPut("todolist-item")]
        public async Task<IActionResult> UpdateToDoListItem([FromBody] UpdateToDoListItemDTO toDoListDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var email = await GetUserEmail();

                UpdateToDoListItemDTO editedTdl = await _toDoService.UpdateToDoListItem(toDoListDTO, email);
                return Ok(editedTdl);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }

        [HttpPost("todolist-item")]
        public async Task<IActionResult> CreateToDoListItem([FromBody] CreateToDoListItemDTO toDoListDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var email = await GetUserEmail();

                CreatedToDoListItemDTO editedTdl = await _toDoService.CreateToDoListItem(toDoListDTO, email);
                return Ok(editedTdl);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }
        [HttpPost("reminder")]
        public async Task<IActionResult> CreateReminder([FromBody] CreateReminderDTO reminder)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var email = await GetUserEmail();

                CreatedReminderDTO createdReminder = await _toDoService.CreateToDoListReminder(reminder, email);
                return Ok(createdReminder);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }

        }


        [HttpPost("attachment")]
        public async Task<IActionResult> CreateAttachment([FromBody] CreateAttachmentDTO attachment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var email = await GetUserEmail();
                CreateAttachmentResponseDTO newAttachment = await _toDoService.CreateAttachment(attachment, email);
                return Ok(newAttachment);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }
        [HttpGet("attachment/{id}")]
        public async Task<IActionResult> GetAttachment(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var email = await GetUserEmail();

                TokenDTO sasToken = new(await _toDoService.GetAttachmentToken(id, email));
                return Ok(sasToken);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }
    }
}
