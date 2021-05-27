using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamRequest : IRequest<ExamDto>
    {
        public ExamDto ExamDto { get; set; }
        public string UserId { get; set; }
    }
}