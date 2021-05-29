using System.Collections.Generic;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExcelUploads.Commands.UploadExcelExamTasks
{
    public class UploadExcelExamTasksRequest : IRequest<List<ExamTaskDto>>
    {
    }
}