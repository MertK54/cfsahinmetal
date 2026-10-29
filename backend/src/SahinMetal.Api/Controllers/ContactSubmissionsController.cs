using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahinMetal.Api.DTOs.Contact;
using SahinMetal.Api.Entities;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactSubmissionsController : ControllerBase
{
    private readonly IContactSubmissionService _submissionService;

    public ContactSubmissionsController(IContactSubmissionService submissionService)
    {
        _submissionService = submissionService;
    }

    // POST: api/contactsubmissions
    [HttpPost]
    public async Task<IActionResult> CreateSubmission([FromBody] SubmissionDto dto)
    {
        var submission = await _submissionService.CreateSubmissionAsync(dto);
        return Ok(submission);
    }

    // GET: api/contactsubmissions
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ContactSubmission>>> GetSubmissions()
    {
        var submissions = await _submissionService.GetSubmissionsAsync();
        return Ok(submissions);
    }

    // PUT: api/contactsubmissions/5/read
    [HttpPut("{id:int}/read")]
    [Authorize]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var success = await _submissionService.MarkAsReadAsync(id);
        if (!success)
        {
            return NotFound("Mesaj bulunamadı.");
        }
        return NoContent();
    }
}
