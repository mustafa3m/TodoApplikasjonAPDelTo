using Microsoft.AspNetCore.Mvc;

namespace TodoApplikasjonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private static List<Todo> todos = new List<Todo>();
        private static int nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> GetTodoById(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public ActionResult<Todo> CreateTodo([FromBody] Todo newTodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            newTodo.Id = nextId++;
            todos.Add(newTodo);
            return CreatedAtAction(nameof(GetTodoById), new { id = newTodo.Id }, newTodo);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTodoById(int id, [FromBody] Todo updatedTodo)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            todo.Title = updatedTodo.Title;
            todo.Description = updatedTodo.Description;
            todo.IsCompleted = updatedTodo.IsCompleted;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodoById(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            todos.Remove(todo);
            return NoContent();
        }
    }
}
