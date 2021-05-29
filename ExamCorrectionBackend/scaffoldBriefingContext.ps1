dotnet ef dbcontext scaffold `
    --namespace "ExamCorrectionBackend.Domain.Entities" `
    -o ..\ExamCorrectionBackend.Domain\Entities `
    -c ExamCorrectionContext `
    --context-dir ..\ExamCorrectionBackend.Persistence `
    --context-namespace "ExamCorrectionBackend.Persistence" `
    -t Courses `
    -t Exams `
    -t ExamTasks `
    -t StudentSolutions `
    -t Dataset `
    -v  `
    -f  `
  Name=ConnectionStrings.ExamCorrection `
  Microsoft.EntityFrameworkCore.SqlServer

