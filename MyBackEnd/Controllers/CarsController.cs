using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBackEnd.Models;
using MyBackEnd.Views;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyBackEnd.Controllers
{
    [ApiController]
    [Route("/api/cars/")]
    public class CarsController : Controller
    {
        private readonly IDatabase database;
        
        public CarsController()
        {
            this.database = new Database();
            // this.database.createCarsTable();
        }

        [HttpGet("{id}")]
        public Car getCarWithID(string id)
        {
            int dbID = Int32.Parse(id);
            return this.database.getCarWithID(dbID);
        }

        [HttpPost("addcar")]
        public Response addCar([FromBody]Car car)
        {
            Console.WriteLine("Received Info By Post");
            return new Response
            {
                success = this.database.RunProcedure(car.name)
                //success = this.database.addCar(car)
            };
        }

        [HttpGet("createtables")]
        public Response populateTables()
        {
            return new Response
            {
                success = this.database.createCarsTable()
            };
        }

        [HttpGet("test")]
        public Response testConnectionToFrontEnd()
        {
            return new Response
            {
                success = true
            };
        }
    }
}
