using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IWebHostEnvironment _env;

        public DocumentController(IDocumentRepository documentRepository, IWebHostEnvironment env)
        {
            _documentRepository = documentRepository;
            _env = env;
        }

        // ------------------------
        // Get all documents
        // ------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documents = await _documentRepository.GetAllAsync();
            return Ok(documents);
        }

        // ------------------------
        // Get document by ID
        // ------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var doc = await _documentRepository.GetByIdAsync(id);
            if (doc == null)
                return NotFound();
            return Ok(doc);
        }

        // ------------------------
        // Upload document
        // ------------------------
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadDocumentDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File is required");

            // Save file to disk (local storage)
            var uploadsFolder = Path.Combine(_env.ContentRootPath, "UploadedFiles");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.File.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // Create document entity
            var document = new Document
            {
                DocumentId = Guid.NewGuid(),
                FileName = uniqueFileName,
                OriginalFileName = dto.File.FileName,
                FileType = dto.File.ContentType,
                MimeType = dto.File.ContentType,
                FileSize = dto.File.Length,
                StoragePath = filePath,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(), // TODO: Replace with authenticated user
                Version = 1,
                Status = 1
            };

            await _documentRepository.AddAsync(document);

            // TODO: Add mapping to PatientDocumentMap if dto.PatientId != null

            return CreatedAtAction(nameof(GetById), new { id = document.DocumentId }, document);
        }

        // ------------------------
        // Delete document
        // ------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null)
                return NotFound();

            // Optional: Delete file from disk
            if (!string.IsNullOrEmpty(document.StoragePath) && System.IO.File.Exists(document.StoragePath))
            {
                System.IO.File.Delete(document.StoragePath);
            }

            await _documentRepository.DeleteAsync(document);
            return NoContent();
        }

        // ------------------------
        // Download document
        // ------------------------
        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null)
                return NotFound();

            var path = document.StoragePath;
            if (!System.IO.File.Exists(path))
                return NotFound("File not found on disk");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(fileBytes, document.MimeType ?? "application/octet-stream", document.OriginalFileName);
        }
    }
}
