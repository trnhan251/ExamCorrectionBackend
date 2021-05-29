using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Domain.Entities;

namespace ExamCorrectionBackend.Persistence.Repositories
{
    public class DatasetRepository : BaseRepository<Dataset>, IDatasetRepository
    {
        public DatasetRepository(ExamCorrectionContext context) : base(context)
        {
        }
    }
}