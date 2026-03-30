using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using Lisye_BackendTest_MidLevel.Models;
using System.Data;

namespace Lisye_BackendTest_MidLevel.Controllers
{
    // Resource-based URL: /api/myofficeacpd
    [Route("api/[controller]")]
    [ApiController]
    public class MyofficeacpdController : ControllerBase
    {
        private readonly string _connectionString;

        public MyofficeacpdController(IConfiguration configuration)
        {
            // Retrieve the connection string from appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // GET: api/myofficeacpd (Retrieve all records)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM MyOffice_ACPD";
            var result = await db.QueryAsync<Acpd>(sql);
            return Ok(result);
        }

        // GET: api/myofficeacpd/{id} (Retrieve single record by ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM MyOffice_ACPD WHERE ACPD_SID = @id";
            var result = await db.QueryFirstOrDefaultAsync<Acpd>(sql, new { id });

            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/myofficeacpd (Create a new record)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Acpd data)
        {
            if (data == null) return BadRequest();

            using IDbConnection db = new SqlConnection(_connectionString);

            if (string.IsNullOrEmpty(data.ACPD_SID))
                data.ACPD_SID = Guid.NewGuid().ToString("N").Substring(0, 20).ToUpper();

            const string sql =  @"INSERT INTO MyOffice_ACPD 
                (ACPD_SID, ACPD_Cname, ACPD_Ename, ACPD_Sname, ACPD_Email, ACPD_Status, 
                 ACPD_Stop, ACPD_StopMemo, ACPD_LoginID, ACPD_LoginPWD, ACPD_Memo, 
                 ACPD_NowDateTime, ACPD_NowID, ACPD_UPDDateTime, ACPD_UPDID) 
                VALUES 
                (@ACPD_SID, @ACPD_Cname, @ACPD_Ename, @ACPD_Sname, @ACPD_Email, @ACPD_Status, 
                 @ACPD_Stop, @ACPD_StopMemo, @ACPD_LoginID, @ACPD_LoginPWD, @ACPD_Memo, 
                 GETDATE(), @ACPD_NowID, GETDATE(), @ACPD_UPDID)";

            await db.ExecuteAsync(sql, data);
            return CreatedAtAction(nameof(GetById), new { id = data.ACPD_SID }, data);
        }

        // PUT: api/myofficeacpd/{id} (Update existing record)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Acpd data)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            // Check if the record exists first
            var exists = await db.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM MyOffice_ACPD WHERE ACPD_SID = @id", new { id });

            if (exists == 0) return NotFound(); 

            const string sql = @"UPDATE MyOffice_ACPD 
                        SET ACPD_Cname = @ACPD_Cname, 
                            ACPD_Ename = @ACPD_Ename, 
                            ACPD_Sname = @ACPD_Sname,
                            ACPD_Email = @ACPD_Email,
                            ACPD_LoginID = @ACPD_LoginID,
                            ACPD_LoginPWD = @ACPD_LoginPWD,
                            ACPD_UPDDateTime = GETDATE()
                        WHERE ACPD_SID = @id";

            await db.ExecuteAsync(sql, new
            {
                data.ACPD_Cname,
                data.ACPD_Ename,
                data.ACPD_Sname,
                data.ACPD_Email,
                data.ACPD_LoginID,
                data.ACPD_LoginPWD,
                id
            });
            return Ok(data); 
        }

        // DELETE: api/myofficeacpd/{id} (Remove record)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var affectedRows = await db.ExecuteAsync(
                "DELETE FROM MyOffice_ACPD WHERE ACPD_SID = @id", new { id });

            if (affectedRows == 0) return NotFound(); // Returns 404 if no record deleted
            return NoContent(); // Returns 204 No Content
        }
    }
}