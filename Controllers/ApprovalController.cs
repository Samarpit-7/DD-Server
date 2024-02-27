
using DD_Server.Models;
using DD_Server.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DD_Server.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ApprovalController(AppDbContext context)
        {
            _context = context;
        }
        

        [HttpPut("approve/{Id}")]
        public async Task<ActionResult<Request>> ApproveRequests(Guid Id, string status)
        {
            var request = await _context.Requests.FindAsync(Id);
            request.Status = status;
            _context.Entry(request).State = EntityState.Modified;

            if(request.Status == "APPROVED")
            {
                var dictionary = _context.Dictionary.Find(request.DId);
                if(request.Action == "INSERT" && dictionary == null)
                {
                    _context.Dictionary.Add(dictionary);
                }
                else if(request.Action == "UPDATE" && dictionary != null)
                {
                    _context.Entry(dictionary).State = EntityState.Modified;
                }
                else if(request.Action == "DELETE" && dictionary != null)
                {
                    _context.Dictionary.Remove(dictionary);
                }
                else{
                    return BadRequest("Invalid ");
                }
            }

            await _context.SaveChangesAsync();
            return request;
        }
    }
}