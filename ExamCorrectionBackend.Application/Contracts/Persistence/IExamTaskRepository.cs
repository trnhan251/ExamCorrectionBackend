using System.Collections.Generic;
using System.Threading.Tasks;
using ExamCorrectionBackend.Domain.Entities;

namespace ExamCorrectionBackend.Application.Contracts.Persistence
{
    public interface IExamTaskRepository : IBaseRepository<ExamTask>
    {
        Task<List<ExamTask>> GetExamTasksFromExamId(int examId);
        Task<ExamTask> GetByIdIncludedExam(int id);
    }
}