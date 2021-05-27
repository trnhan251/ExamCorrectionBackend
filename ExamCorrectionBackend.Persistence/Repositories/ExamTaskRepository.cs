using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamCorrectionBackend.Persistence.Repositories
{
    public class ExamTaskRepository : BaseRepository<ExamTask>, IExamTaskRepository
    {
        private readonly ExamCorrectionContext _context;

        public ExamTaskRepository(ExamCorrectionContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ExamTask>> GetExamTasksFromExamId(int examId)
        {
            var results = await _context.ExamTasks.Where(x => x.ExamId == examId).ToListAsync();
            return results;
        }

        public async Task<ExamTask> GetByIdIncludedExam(int id)
        {
            var result = await _context.ExamTasks.Include(x => x.Exam).Include(x => x.Exam.Course)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }
    }
}