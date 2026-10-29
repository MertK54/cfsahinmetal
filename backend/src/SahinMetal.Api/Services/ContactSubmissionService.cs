using Microsoft.EntityFrameworkCore;
using SahinMetal.Api.Common.Helpers;
using SahinMetal.Api.Data;
using SahinMetal.Api.DTOs.Contact;
using SahinMetal.Api.Entities;
using SahinMetal.Api.Interfaces;

namespace SahinMetal.Api.Services;

public class ContactSubmissionService : IContactSubmissionService
{
    private readonly ApplicationDbContext _context;

    public ContactSubmissionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ContactSubmission> CreateSubmissionAsync(SubmissionDto dto)
    {
        // Sanitize all inputs against HTML injection, script execution, and malicious payloads
        var safeName = SecurityHelper.SanitizeText(dto.Name, 100);
        var safeEmail = SecurityHelper.SanitizeText(dto.Email, 100);
        var safeSubject = SecurityHelper.SanitizeText(dto.Subject, 150);
        var safeMessage = SecurityHelper.SanitizeText(dto.Message, 3000);

        var submission = new ContactSubmission
        {
            Name = safeName,
            Email = safeEmail,
            Subject = safeSubject,
            Message = safeMessage,
            SubmittedAt = DateTime.UtcNow,
            IsRead = false
        };

        _context.ContactSubmissions.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    public async Task<IEnumerable<ContactSubmission>> GetSubmissionsAsync()
    {
        return await _context.ContactSubmissions
            .OrderByDescending(s => s.SubmittedAt)
            .ToListAsync();
    }

    public async Task<bool> MarkAsReadAsync(int id)
    {
        var submission = await _context.ContactSubmissions.FindAsync(id);
        if (submission == null)
        {
            return false;
        }

        submission.IsRead = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
