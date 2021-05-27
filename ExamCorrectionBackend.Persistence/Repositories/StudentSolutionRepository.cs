using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamCorrectionBackend.Persistence.Repositories
{
    public class StudentSolutionRepository : BaseRepository<StudentSolution>, IStudentSolutionRepository
    {
        private readonly ExamCorrectionContext _context;

        public StudentSolutionRepository(ExamCorrectionContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<StudentSolution>> GetAllFromExamTaskId(int examTaskId)
        {
            var entities = await _context.StudentSolutions.Where(x => x.TaskId == examTaskId).ToListAsync();
            return entities;
        }

        public async Task<StudentSolution> GetByIdIncludedExamTask(int id)
        {
            var entity = await _context.StudentSolutions
                .Include(x => x.Task.Exam.Course)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return entity;
        }
    }
}