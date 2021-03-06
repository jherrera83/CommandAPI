using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController: ControllerBase
    {
        private readonly CommandContext _context;

        public CommandsController(CommandContext context) => _context = context;
        
        //GET: api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommandItems(){
            //random comments about tryng CI 
            return _context.CommandItems;
        }

        //GET: api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id){
            var commandItem = _context.CommandItems.Find(id);
            if(commandItem==null)
                return NotFound();
            return commandItem;
        }

        //POST: api/commands
        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command){
            _context.CommandItems.Add(command);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return CreatedAtAction("GetCommandItem", new Command{Id=command.Id}, command);
        }

        //UPDATE: api/commands/{id}
        [HttpPut("{Id}")]
        public ActionResult PutCommandItem(int id, Command command){
            if(id!=command.Id){
                return BadRequest();
            }
            _context.Entry(command).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        //DELETE: api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult<Command>DeleteCommandItem(int id){
            var commandItem = _context.CommandItems.Find(id);
            if(commandItem == null)
               return NotFound();
            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();

            return commandItem;
        }
    }
}