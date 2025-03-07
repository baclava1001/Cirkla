using Cirkla_DAL;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircleController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CircleController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<CircleController>
        [HttpGet]
        public IEnumerable<Circle> Get()
        {
            return _dbContext.Circles;
        }

        // GET api/<CircleController>/5
        [HttpGet("{id}")]
        public Circle Get(int id)
        {
            return _dbContext.Circles.Find(id);
        }

        // POST api/<CircleController>
        [HttpPost]
        public Circle Post(Circle circle)
        {
            _dbContext.Circles.Add(circle);
            _dbContext.SaveChanges();
            return circle;
        }

        // PUT api/<CircleController>/5
        [HttpPut("{id}")]
        public Circle Put(int id, Circle circle)
        {
            _dbContext.Circles.Update(circle);
            _dbContext.SaveChanges();
            return circle;
        }

        // DELETE api/<CircleController>/5
        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            var circle = _dbContext.Circles.Find(id);
            _dbContext.Circles.Remove(circle);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
