using SahinMetal.Api.DTOs.Contact;
using SahinMetal.Api.Entities;

namespace SahinMetal.Api.Interfaces;

public interface IContactSubmissionService
{
    Task<ContactSubmission> CreateSubmissionAsync(SubmissionDto dto);
    Task<IEnumerable<ContactSubmission>> GetSubmissionsAsync();
    Task<bool> MarkAsReadAsync(int id);
}
