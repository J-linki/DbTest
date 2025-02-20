using System.Diagnostics;
using DbTest.Data;
using DbTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbTest.Controllers
{
    public class DbController : Controller
    {
        private readonly ILogger<DbController> _logger;
        private readonly TestContext _context;

        public DbController(ILogger<DbController> logger, TestContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("/GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            UserModel? user = await _context.Users!.FindAsync(id);
            
            if (user is null) return BadRequest("User not found.");
            
            return Ok($"User: {user.Name}, Password: {user.Password}");
        }


        [HttpPut("/AddUser")]
        public async Task<IActionResult> AddUser(string name, string password, string? email = null, decimal balance = 0)
        {
            var user = new UserModel { Name = name, Email = email, Password = password, Balance = balance };
            
            await _context.Users!.AddAsync(user);
            
            await _context.SaveChangesAsync();
            
            return Ok("User added");
        }

        [HttpPut("/AddStock")]
        public async Task<IActionResult> AddStock(decimal growth, decimal value, string companyName)
        {
            CompanyModel? company = await _context.Companys.FirstOrDefaultAsync(c => c.Name == companyName);
            
            if (company is null) return BadRequest("Company not found");

            var Stock = new StocksModel { GrowthRate = growth, Value = value, Company = company };

            company.Stocks.Add(Stock);

            await _context.Stocks.AddAsync(Stock);

            await _context.SaveChangesAsync();
            return Ok("Stock Added");
        }

        
        [HttpPut("/AddCompany")]
        public async Task<IActionResult> AddCompany(string name, decimal marketValue = 0)
        {
            var Company = new CompanyModel { Name = name, MarketValue = marketValue };
            
            await _context.AddAsync(Company);

            await _context.SaveChangesAsync();

            return Ok("Added the company");
        }

        [HttpPost("/GiveStocks")]
        public async Task<IActionResult> GiveStocks(int userid, string companyName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                UserModel? user = await _context.Users!.FindAsync(userid);

                if (user is null) return BadRequest("User not found");


                //TODO: only give one stock
                await _context.Stocks.Where(s => s.Company.Name == companyName)
                                     .ExecuteUpdateAsync(s => s
                                     .SetProperty(s => s.UserId, s => user.Id));

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok("Stock given");
            }
            catch(Exception ex)
            {
                
                await transaction.RollbackAsync();
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("/DeleteUser")]
        public async Task<IActionResult> RemoveUser(int userid)
        {
            await _context.Users!.Where(u => u.Id == userid)
                                 .ExecuteDeleteAsync();
            
            await _context.SaveChangesAsync();

            return Ok("User removed");
        }

        [HttpDelete("/DeleteCompany")]
        public async Task<IActionResult> RemoveCompanuy(string companyName)
        {
            await _context.Companys!.Where(c => c.Name == companyName)
                                 .ExecuteDeleteAsync();

            await _context.SaveChangesAsync();

            return Ok("User removed");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
