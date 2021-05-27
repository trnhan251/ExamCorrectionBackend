using System.Collections.Generic;
using System.Threading.Tasks;
using ExamCorrectionBackend.Domain.Entities;

namespace ExamCorrectionBackend.Application.Contracts.Persistence
{
    public interface IStudentSolutionRepository : IBaseRepository<StudentSolution>
    {
        Task<List<StudentSolution>> GetAllFromExamTaskId(int examTaskId);
        Task<StudentSolution> GetByIdIncludedExamTask(int id);
    }
}