
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DD_Server.Model;
using DD_Server.Persistence;
using DD_Server.Models;


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
            if (t.Count == 0)
            {
                _context.Dictionary.AddRange(DataList);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDictionary), DataList);
            }

            List<Dictionary> TempList1 = []; //For unique values
            List<Dictionary> TempList2 = [];  //For updated values that are not exactly the same.
            for (int i = 0; i < DataList.Count; i++)
            {
                Dictionary dictionary = _context.GetByDataPoint(DataList[i].DataPoint);

                if (dictionary == null)
                {
                    TempList1.Add(DataList[i]);
                }
                else
                {
                    // Compare other fields from BaseDictionary class
                    if (!AreEqualExceptGuid(DataList[i], dictionary))
                    {
                        TempList2.Add(DataList[i]);
                        Dictionary tempDictionary = _context.GetByDataPoint(DataList[i].DataPoint);
                        Audit audit = new();
                        audit.Container = tempDictionary.Container;
                        audit.DataPoint = tempDictionary.DataPoint;
                        audit.DbColumnName = tempDictionary.DbColumnName;
                        audit.DbDataType = tempDictionary.DbDataType;
                        audit.Definition = tempDictionary.Definition;
                        audit.FieldType = tempDictionary.FieldType;
                        audit.PossibleValues = tempDictionary.PossibleValues;
                        audit.Synonyms = tempDictionary.Synonyms;
                        audit.CalculatedInfo = tempDictionary.CalculatedInfo;
                        audit.DId = tempDictionary.Id;
                        audit.Status = "Rejected";
                        audit.UId = 1;
                        audit.TimeStamp = DateTime.UtcNow;
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

            _context.Dictionary.Remove(dictionary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DictionaryExists(Guid id)
        {
            return _context.Dictionary.Any(e => e.Id == id);
        }

        static bool AreEqualExceptGuid(BaseDictionary obj1, Dictionary obj2)
        {
            return obj1.Container == obj2.Container &&
                obj1.DataPoint == obj2.DataPoint &&
                obj1.DbColumnName == obj2.DbColumnName &&
                obj1.FieldType == obj2.FieldType &&
                obj1.DbDataType == obj2.DbDataType &&
                obj1.Definition == obj2.Definition &&
                obj1.PossibleValues.SequenceEqual(obj2.PossibleValues) &&
                obj1.Synonyms.SequenceEqual(obj2.Synonyms) &&
                obj1.CalculatedInfo == obj2.CalculatedInfo;
        }
    }
}
