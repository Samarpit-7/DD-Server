
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DD_Server.Model;
using DD_Server.Persistence;
using OfficeOpenXml;
using Newtonsoft.Json;

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

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files[0];

            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Process the file as needed, e.g., save to database
                ConvertExcelToJson(filePath);



                return Ok(new { message = "File uploaded successfully" });
            }

            return BadRequest("File not provided");
        }
        private void ConvertExcelToJson(string filePath)
        {
            var result = new List<Dictionary>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                for (var rowNumber = 2; rowNumber <= worksheet.Dimension.End.Row; rowNumber++)
                {
                    var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Column];

                    var dataItem = new Dictionary
                    {
                        Container = row[rowNumber, 1].Text,
                        DataPoint = row[rowNumber, 2].Text,
                        DbColumnName = row[rowNumber, 3].Text,
                        FieldType = row[rowNumber, 4].Text,
                        DbDataType = row[rowNumber, 5].Text,
                        Definition = row[rowNumber, 6].Text,
                        PossibleValues = row[rowNumber, 7].Text.Split(',').Select(value => value.Trim()).ToArray(),
                        Synonyms = row[rowNumber, 8].Text.Split(',').Select(value => value.Trim()).ToArray(),
                        CalculatedInfo = row[rowNumber, 9].Text,
                        // IsLocked = false
                    };

                    result.Add(dataItem);
                }
            }
            _context.Dictionary.AddRange(result);
            _context.SaveChanges();
            // return result;
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

        // PUT: api/Dictionary/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
    }
}
