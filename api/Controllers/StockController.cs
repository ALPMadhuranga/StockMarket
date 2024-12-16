using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAllStocks() // Method for getting all stocks
        {
            var stocks = await _context.Stocks.ToListAsync(); // Getting all stocks from the database
            
            var stockDto = stocks.Select(s => s.ToStockDto()); // Mapping the stocks to StockDto objects and returning them 

            return Ok(stocks); // Returning the stocks
        }


        // Get stock by id
        [HttpGet("{id}")] // Attribute for the GetStockById method
        public async Task<IActionResult> GetStockById([FromRoute] int id) // Method for getting a stock by id
        {
            var stock = await _context.Stocks.FindAsync(id); // Finding the stock by id from the database

            if (stock == null) // If the stock is not found
            {
                return NotFound(); // Return a 404 status code
            }

            return Ok(stock.ToStockDto()); // Return the stock as a StockDto object
        }


        // Create a new stock
        [HttpPost] // Attribute for the CreateStock method
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto) // Method for creating a new stock
        {
            var stockModel = stockDto.ToStockFromCreateDTO(); // Mapping the CreateStockDto object to a Stock object
            await _context.Stocks.AddAsync(stockModel); // Adding the stock to the database
            await _context.SaveChangesAsync(); // Saving the changes to the database

            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto()); // Returning the created stock
        }


        // Update a stock
        [HttpPut] // Attribute for the UpdateStock method
        [Route("{id}")] // Route for the UpdateStock method

        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) // Method for updating a stock
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id); // Finding the stock by id from the database

            if (stockModel == null) // If the stock is not found
            {
                return NotFound(); // Return a 404 status code
            }

            stockModel.Symbol = updateDto.Symbol; // Updating the stock symbol
            stockModel.CompanyName = updateDto.CompanyName; // Updating the stock company name
            stockModel.PurchasePrice = updateDto.PurchasePrice; // Updating the stock purchase price
            stockModel.LastDividend = updateDto.LastDividend; // Updating the stock last dividend
            stockModel.Industry = updateDto.Industry; // Updating the stock industry
            stockModel.MarketCap = updateDto.MarketCap; // Updating the stock market cap

           await _context.SaveChangesAsync(); // Saving the changes to the database

            return Ok(stockModel.ToStockDto()); // Returning the updated stock
        }


        // Delete a stock
        [HttpDelete] // Attribute for the DeleteStock method
        [Route("{id}")] // Route for the DeleteStock method

        public async Task<IActionResult> DeleteStock([FromRoute] int id) // Method for deleting a stock
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id); // Finding the stock by id from the database

            if (stockModel == null) // If the stock is not found
            {
                return NotFound(); // Return a 404 status code
            }

            _context.Stocks.Remove(stockModel); // Removing the stock from the database
            await _context.SaveChangesAsync(); // Saving the changes to the database

            return NoContent(); // Return a 204 status code
        }
    }
}