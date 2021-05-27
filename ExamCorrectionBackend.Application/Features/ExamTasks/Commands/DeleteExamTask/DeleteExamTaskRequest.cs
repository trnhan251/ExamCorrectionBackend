using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Commands.DeleteExamTask
{
    public class DeleteExamTaskRequest : IRequest<ExamTaskDto>
    {
        public int ExamTaskId { get; set; }
        public string UserId { get; set; }
    }
}