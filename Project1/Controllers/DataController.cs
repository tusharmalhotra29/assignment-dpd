using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project1.Models.DTO;
using Project1.Security.Token;
using System.Collections.Generic;

namespace Project1.Controllers
{
    

    [ApiController]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private static List<DataModel> dataStore = new List<DataModel>();
        private static int nextId = 1;

        [HttpPost]
        [Authorize] // Requires authentication to access this endpoint.
        public IActionResult StoreData(KeyValueRequest request)
        {
            try
            {
                // Validation - For simplicity, you can use basic validation here.
                if (string.IsNullOrEmpty(request.Key))
                {
                    throw new InvalidKeyException("Invalid or missing key.");
                }

                if (string.IsNullOrEmpty(request.Value))
                {
                    throw new InvalidValueException("Invalid or missing value.");
                }

                // Check if the key already exists.
                if (dataStore.Any(d => d.Key == request.Key))
                {
                    throw new KeyExistsException("The provided key already exists in the database. To update an existing key, use the update API.");
                }

                // Create a new data model and store it.
                var newData = new DataModel
                {
                    Id = nextId++,
                    Key = request.Key,
                    Value = request.Value
                    // Add any additional properties as needed.
                };

                dataStore.Add(newData);

                // Return the success message in the response.
                return Ok(new { status = "success", message = "Data stored successfully." });
            }
            catch (InvalidKeyException ex)
            {
                return BadRequest(new { status = "error", message = ex.Message, errorCode = "INVALID_KEY" });
            }
            catch (InvalidValueException ex)
            {
                return BadRequest(new { status = "error", message = ex.Message, errorCode = "INVALID_VALUE" });
            }
            catch (KeyExistsException ex)
            {
                return Conflict(new { status = "error", message = ex.Message, errorCode = "KEY_EXISTS" });
            }
            catch (Exception ex)
            {
                // Handle other exceptions here.
                return StatusCode(500, new { status = "error", message = "An unexpected error occurred.", errorCode = "INTERNAL_ERROR" });
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // No authentication required to access this endpoint.
        public IActionResult GetDataById(int id)
        {
            var data = dataStore.FirstOrDefault(d => d.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpPut("{id}")]
        [Authorize] // Requires authentication to access this endpoint.
        public IActionResult UpdateData(int id, KeyValueRequest request)
        {
            var data = dataStore.FirstOrDefault(d => d.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            // Validation - For simplicity, you can use basic validation here.
            if (string.IsNullOrEmpty(request.Key) || string.IsNullOrEmpty(request.Value))
            {
                return BadRequest(new { status = "error", message = "Key and Value are required." });
            }

            // Update the data.
            data.Key = request.Key;
            data.Value = request.Value;

            // Return success message in the response.
            return Ok(new { status = "success", message = "Data updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize] // Requires authentication to access this endpoint.
        public IActionResult DeleteData(int id)
        {
            var data = dataStore.FirstOrDefault(d => d.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            // Delete the data from the store.
            dataStore.Remove(data);

            // Return success message in the response.
            return Ok(new { status = "success", message = "Data deleted successfully." });
        }



        public class InvalidKeyException : Exception
        {
            public InvalidKeyException(string message) : base(message)
            {
            }
        }

        public class InvalidValueException : Exception
        {
            public InvalidValueException(string message) : base(message)
            {
            }
        }

        public class KeyExistsException : Exception
        {
            public KeyExistsException(string message) : base(message)
            {
            }
        }

        public class InvalidTokenException : Exception
        {
            public InvalidTokenException(string message) : base(message)
            {
            }
        }
    }


}
