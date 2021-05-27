using System.Collections.Generic;
using System.Threading.Tasks;
using ExamCorrectionBackend.Domain.Entities;

namespace ExamCorrectionBackend.Application.Contracts.Persistence
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<List<Course>> GetAllByOwnerId(string ownerId);
    }
}