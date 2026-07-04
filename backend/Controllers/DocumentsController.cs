using backend.DTOs;
using backend.Mapping;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsService _documentsService;
        private readonly DocumentMapper _documentMapper;
        private readonly IWebHostEnvironment _environment;

        public DocumentsController(
            IDocumentsService documentsService,
            DocumentMapper documentMapper,
            IWebHostEnvironment environment)
        {
            _documentsService = documentsService;
            _documentMapper = documentMapper;
            _environment = environment;
        }

        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<DocumentResponse>>> GetMyDocuments()
        {
            var accountId = GetCurrentAccountId();
            var documents = await _documentsService.GetAllDocumentsForUser(accountId);
            var response = _documentMapper.ToResponses(documents);

            return Ok(response);
        }

        [HttpGet("my/to-submit")]
        public async Task<ActionResult<IEnumerable<DocumentResponse>>> GetMyDocumentsToBeSubmitted()
        {
            var accountId = GetCurrentAccountId();
            var documents = await _documentsService.GetDocumentsToBeSubmitted(accountId);
            var response = _documentMapper.ToResponses(documents);

            return Ok(response);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpGet("employee/{accountId}")]
        public async Task<ActionResult<IEnumerable<DocumentResponse>>> GetDocumentsForEmployee(int accountId)
        {
            var documents = await _documentsService.GetAllDocumentsForUser(accountId);
            var response = _documentMapper.ToResponses(documents);

            return Ok(response);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<DocumentTypeResponse>>> GetDocumentTypes()
        {
            var documentTypes = await _documentsService.GetAllDocumentTypes();
            var response = _documentMapper.ToDocumentTypeResponses(documentTypes);

            return Ok(response);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpPost("types")]
        public async Task<IActionResult> AddDocumentType(CreateDocumentTypeRequest request)
        {
            var documentType = _documentMapper.ToEntity(request);

            await _documentsService.AddDocumentType(documentType);
            return Created();
        }

        [HttpPost("{documentId}/upload")]
        public async Task<IActionResult> UploadDocument(int documentId, IFormFile file)
        {
            if (file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            var document = await _documentsService.GetDocumentById(documentId);

            if (document == null)
            {
                return NotFound();
            }

            if (document.AccountId != GetCurrentAccountId())
            {
                return Forbid();
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath ?? "wwwroot", "uploads", "documents");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            await using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"/uploads/documents/{fileName}";
            await _documentsService.SubmitDocument(documentId, fileUrl);

            return Created(fileUrl, null);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpPut("{documentId}/status")]
        public async Task<IActionResult> UpdateDocumentStatus(int documentId, UpdateDocumentStatusRequest request)
        {
            var document = await _documentsService.GetDocumentById(documentId);

            if (document == null)
            {
                return NotFound();
            }

            await _documentsService.UpdateDocumentStatus(documentId, request.Status);
            return NoContent();
        }

        private int GetCurrentAccountId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
