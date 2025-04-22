using shared_libraries.DTOs;
using shared_libraries.Models;
using System.Net;

namespace shared_libraries.Interfaces
{
    public interface IStudyRepository : IGenericRepository
    {
        Task<HttpStatusCode> UpdateStudies(List<StudyDto> changes);
    }
}
