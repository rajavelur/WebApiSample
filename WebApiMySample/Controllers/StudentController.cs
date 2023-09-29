using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApiMySample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        static List<Student> stdList = new List<Student> {  new Student{ ID=123, Age = 21, Name = "Rajini", Gender ="Male"},
                                                            new Student{ ID=234, Age = 22, Name = "Kamal", Gender ="Male"},
                                                            new Student{ ID=456, Age = 20, Name = "Vijay", Gender ="Male"},
                                                            new Student{ ID=789, Age = 20, Name = "Ajith", Gender ="Male"}};

        [HttpGet]
        public IActionResult Get()
        {         
            return Ok(stdList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var stds = stdList.Where(std => std.ID == id);
            if (stds != null)
            {               
                return Ok(stds);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student std)
        {
            Student objStd = stdList.Where(std => std.ID == id).FirstOrDefault();
            if (objStd != null)
            {
                objStd.Name = std.Name;
                objStd.Age = std.Age;
                objStd.Gender = std.Gender;
                return Ok(objStd);
            }
            else
            {
                return NotFound();
            }           
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student std)
        {            
            stdList.Add(std);
            return Created("", std);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student objStd = stdList.Where(std => std.ID == id).FirstOrDefault();
            if (objStd != null)
            {
                stdList.Remove(objStd);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }

    public class Student
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
    }
}
