using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamCorrectionBackend.Persistence.Repositories
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        private readonly ExamCorrectionContext _context;

        public ExamRepository(ExamCorrectionContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Exam>> GetAllByOwnerId(string ownerId)
        {
            var results = await _context.Exams.Include(x => x.Course).Where(x => x.Course.OwnerId.Equals(ownerId))
                .ToListAsync();
            return results;
        }

        public async Task<Exam> GetByIdIncludedCourse(int id)
        {
            var result = await _context.Exams.Include(x => x.Course).Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }
    }
}