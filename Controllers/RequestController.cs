using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DD_Server.Models;
using DD_Server.Persistence;
using DD_Server.Helper;

namespace DD_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // POST: api/Dictionary/Bulk
        [HttpPost("Bulk")]
        public async Task<ActionResult<IEnumerable<Request>>> PostDataBulk(List<Dictionary> DataList)
        {
            if (DataList == null || DataList.Count == 0)
            {
                return BadRequest("No Data provided");
            }

            List<Dictionary> t = _context.Dictionary.ToList();
            DataList = DataList.Select(obj =>
            {
                obj.TimeStamp = DateTime.UtcNow;
                return
                obj;
            }).ToList();
            ComparingExceptGuid HelperFunction = new ComparingExceptGuid(_context);

            List<Request> r = [];

            if (t.Count == 0)
            {
                for(int i=0 ; i<DataList.Count ; i++)
                {
                    r.Add(HelperFunction.Convert_Dictionary_to_Request(DataList[i],"INSERT"));
                }
                _context.Requests.AddRange(r);
                await _context.SaveChangesAsync();
                return Ok(r);
            }

            for (int i = 0; i < DataList.Count; i++)
            {
                Dictionary dictionary = _context.GetByDataPoint(DataList[i].DataPoint);

                if (dictionary == null)
                {
                    _context.Requests.Add(HelperFunction.Convert_Dictionary_to_Request(DataList[i],"INSERT"));
                    await _context.SaveChangesAsync();
                }
                else if (!HelperFunction.AreEqualExceptGuid(DataList[i], dictionary) )
                {
                    _context.Requests.Add(HelperFunction.Convert_Dictionary_to_Request(DataList[i],"UPDATE"));
                    dictionary.IsLocked = true;
                    _context.Entry(dictionary).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Request/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(Guid id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Request/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(Guid id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Request
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<Request>> PostDeleteRequest(List<Guid> DataList)
        {
            if(DataList.Count == 0)
            {
                return NoContent();
            }
            ComparingExceptGuid HelperFunction = new ComparingExceptGuid(_context);
            List<Request> request = [];
            for(int i=0 ; i<DataList.Count ; i++)
            {
                Dictionary dictionary = _context.Dictionary.Find(DataList[i]);
                if(dictionary == null)
                {
                    return BadRequest("No Records Found");
                }
                else if(!dictionary.IsLocked){
                    request.Add(HelperFunction.Convert_Dictionary_to_Request(dictionary,"DELETE"));
                    dictionary.IsLocked = true;
                    _context.Entry(dictionary).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            _context.Requests.AddRange(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }


        // DELETE: api/Request/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(Guid id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
