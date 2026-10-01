using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HrmApi.Controllers
{
    [RoutePrefix("api/documents")]
    public class DocumentController : ApiController
    {
        private string connectionString = "Server=46.165.212.23,5412;Database=Hrms_DB;User Id=sa;Password=#Meta8080;Connection Timeout=8000;";

        [HttpPost]
        [Route("upload")]
        public async Task<IHttpActionResult> UploadFile()
        {
            // Step 1: Check if the request contains multipart content
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("Only multipart content is supported.");
            }

            // Step 2: Create a provider to read the multipart content
            var provider = new MultipartMemoryStreamProvider();

            // Step 3: Attempt to read the content asynchronously
            await Request.Content.ReadAsMultipartAsync(provider);

            // Step 4: Check if any files were uploaded
            if (provider.Contents.Count == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Step 5: Get the first file from the uploaded content
            var fileContent = provider.Contents[0];

            // Step 6: Check if the file content is null (it shouldn't be, but just in case)
            if (fileContent == null)
            {
                return BadRequest("Uploaded file is null.");
            }

            // Step 7: Read the file content and other file details
            var fileBytes = await fileContent.ReadAsByteArrayAsync();

            // Rename the conflicting fileName variable in one of the places
            var uploadedFileName = fileContent.Headers.ContentDisposition.FileName.Trim('\"');  // Rename fileName to uploadedFileName
            var fileType = fileContent.Headers.ContentType.ToString();

            // Step 8: Save the file into the database (SQL Server in your case)
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    var query = "INSERT INTO documents (file_name, file_data, file_type) VALUES (@fileName, @fileData, @fileType)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Update the parameter name to match the renamed variable
                        command.Parameters.Add("@fileName", System.Data.SqlDbType.VarChar).Value = uploadedFileName;  // Use uploadedFileName here
                        command.Parameters.Add("@fileData", System.Data.SqlDbType.VarBinary).Value = fileBytes;
                        command.Parameters.Add("@fileType", System.Data.SqlDbType.VarChar).Value = fileType;

                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            return Ok("File uploaded successfully.");
        }



    }
}
