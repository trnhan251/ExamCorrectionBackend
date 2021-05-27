using System.Collections.Generic;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetAllExamTasksFromExam
{
    public class GetAllExamTasksFromExamRequest : IRequest<List<ExamTaskDto>>
    {
        public int ExamId { get; set; }
        public string UserId { get; set; }
    }
}