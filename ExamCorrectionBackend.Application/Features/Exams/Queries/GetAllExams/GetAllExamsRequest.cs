using System.Collections.Generic;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Queries.GetAllExams
{
    public class GetAllExamsRequest : IRequest<List<ExamDto>>
    {
        public string UserId { get; set; }
    }
}