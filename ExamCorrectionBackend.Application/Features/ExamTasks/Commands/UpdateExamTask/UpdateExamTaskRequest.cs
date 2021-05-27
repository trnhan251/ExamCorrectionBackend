using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Commands.UpdateExamTask
{
    public class UpdateExamTaskRequest : IRequest<ExamTaskDto>
    {
        public ExamTaskDto ExamTaskDto { get; set; }
        public string UserId { get; set; }
    }
}