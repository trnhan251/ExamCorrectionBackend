using System.Collections.Generic;
using System.Threading.Tasks;
using ExamCorrectionBackend.Domain.Entities;

namespace ExamCorrectionBackend.Application.Contracts.Persistence
{
    public interface IExamRepository : IBaseRepository<Exam>
    {
        Task<List<Exam>> GetAllByOwnerId(string ownerId);
        Task<Exam> GetByIdIncludedCourse(int id);
    }
}