using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamCorrectionBackend.Persistence.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private readonly ExamCorrectionContext _context;

        public CourseRepository(ExamCorrectionContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllByOwnerId(string ownerId)
        {
            var results = await _context.Courses.Where(x => x.OwnerId.Equals(ownerId)).ToListAsync();
            return results;
        }
    }
}