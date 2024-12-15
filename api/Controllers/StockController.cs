using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")] // Route for the StockController
    [ApiController] // Attribute for the StockController
    public class StockController : ControllerBase // Class for the StockController
    {
        private readonly ApplicationDBContext _context; // Instance of the ApplicationDBContext class 
        public StockController(ApplicationDBContext context) // Constructor for the StockController class
        {
            _context = context; // Assigning the context to the _context variable
        }

        // Get all stocks
        [HttpGet] // Attribute for the GetStocks method
        public IActionResult GetAllStocks() // Method for getting all stocks
        {
            var stocks = _context.Stocks.ToList() // Getting all stocks from the database
            .Select(s => s.ToStockDto()); // Mapping the stocks to StockDto objects and returning them 

            return Ok(stocks); // Returning the stocks
        }


        // Get stock by id
        [HttpGet("{id}")] // Attribute for the GetStockById method
        public IActionResult GetStockById([FromRoute] int id) // Method for getting a stock by id
        {
            var stock = _context.Stocks.Find(id); // Finding the stock by id from the database

            if (stock == null) // If the stock is not found
            {
                return NotFound(); // Return a 404 status code
            }

            return Ok(stock.ToStockDto()); // Return the stock as a StockDto object
        }

    }
}