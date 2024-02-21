
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DD_Server.Model;
using DD_Server.Persistence;
using DD_Server.Models;
using DD_Server.Helper;

namespace DD_Server.Controllers2
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DictionaryController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/Dictionary
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dictionary>>> GetDictionary()
        {
            return await _context.Dictionary.ToListAsync();
        }

        // GET: api/Dictionary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dictionary>> GetDictionary(Guid id)
        {
            var dictionary = await _context.Dictionary.FindAsync(id);

            if (dictionary == null)
            {
                return NotFound();
            }

            return dictionary;
        }

        // POST: api/Dictionary/Bulk
        [HttpPost("Bulk")]
        public async Task<ActionResult<IEnumerable<Dictionary>>> PostDataBulk(List<Dictionary> DataList)
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
            ComparingExceptGuid compExceptGuid = new ComparingExceptGuid(_context);

            if (t.Count == 0)
            {
                _context.Dictionary.AddRange(DataList);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDictionary), DataList);
            }

            List<Dictionary> TempList1 = new List<Dictionary>(); // For unique values
            List<Dictionary> TempList2 = new List<Dictionary>();
            for (int i = 0; i < DataList.Count; i++)
            {
                Dictionary dictionary = _context.GetByDataPoint(DataList[i].DataPoint);

                DataList[i].TimeStamp = DateTime.UtcNow;

                if (dictionary == null)
                {
                    TempList1.Add(DataList[i]);
                }
                else
                {
                    // Compare other fields from BaseDictionary class
                    if (compExceptGuid.AreEqualExceptGuid(DataList[i], dictionary))
                    {
                        TempList2.Add(DataList[i]);
                        Dictionary tempDictionary = _context.GetByDataPoint(DataList[i].DataPoint);
                        Audit audit = new(tempDictionary.Container, tempDictionary.DataPoint, tempDictionary.DbColumnName, tempDictionary.FieldType, tempDictionary.DbDataType, tempDictionary.Definition, tempDictionary.PossibleValues, tempDictionary.Synonyms, tempDictionary.CalculatedInfo, "Rejected", tempDictionary.TimeStamp, tempDictionary.Id, tempDictionary.UId);
                        _context.Audits.Add(audit);
                        _context.Dictionary.Remove(tempDictionary);
                    }
                }
            }

            _context.Dictionary.AddRange(TempList1);

            _context.Dictionary.AddRange(TempList2);
            await _context.SaveChangesAsync();

            return await _context.Dictionary.ToListAsync();
        }
        // PUT: api/Dictionary/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDictionary(Guid id, Dictionary dictionary)
        {
            if (id != dictionary.Id)
            {
                return BadRequest();
            }

            var tempDictionary = _context.Dictionary.Find(id);
            Audit audit = new(tempDictionary.Container, tempDictionary.DataPoint, tempDictionary.DbColumnName, tempDictionary.FieldType, tempDictionary.DbDataType, tempDictionary.Definition, tempDictionary.PossibleValues, tempDictionary.Synonyms, tempDictionary.CalculatedInfo, "Rejected", tempDictionary.TimeStamp, tempDictionary.Id, tempDictionary.UId);
            _context.Audits.Add(audit);

            _context.Entry(dictionary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DictionaryExists(id))
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

        // POST: api/Dictionary
        [HttpPost]
        public async Task<ActionResult<Dictionary>> PostDictionary(Dictionary dictionary)
        {
            _context.Dictionary.Add(dictionary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDictionary", new { id = dictionary.Id }, dictionary);
        }

        // DELETE: api/Dictionary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDictionary(Guid id)
        {
            var dictionary = await _context.Dictionary.FindAsync(id);
            if (dictionary == null)
            {
                return NotFound();
            }
            Audit audit = new(dictionary.Container, dictionary.DataPoint, dictionary.DbColumnName, dictionary.FieldType, dictionary.DbDataType, dictionary.Definition, dictionary.PossibleValues, dictionary.Synonyms, dictionary.CalculatedInfo, "Rejected", dictionary.TimeStamp, dictionary.Id, dictionary.UId);
            _context.Dictionary.Remove(dictionary);
            _context.Audits.Add(audit);
            await _context.SaveChangesAsync();

            return Ok(audit);
        }

        private bool DictionaryExists(Guid id)
        {
            return _context.Dictionary.Any(e => e.Id == id);
        }

    }
}
