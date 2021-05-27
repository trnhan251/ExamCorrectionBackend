using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetExamTask
{
    public class GetExamTaskRequest : IRequest<ExamTaskDto>
    {
        public int ExamTaskId { get; set; }
        public string UserId { get; set; }
    }
}