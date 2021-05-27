using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Commands.CreateExamTask
{
    public class CreateExamTaskRequest : IRequest<ExamTaskDto>
    {
        public ExamTaskDto ExamTaskDto { get; set; }
        public string UserId { get; set; }
    }
}