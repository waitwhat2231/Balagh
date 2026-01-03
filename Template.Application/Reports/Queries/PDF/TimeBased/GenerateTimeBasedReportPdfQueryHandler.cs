using MediatR;
using Template.Application.PDFFilesHelper;
using Template.Application.Reports.Services;

namespace Template.Application.Reports.Queries.PDF.TimeBased
{
    class GenerateTimeBasedReportPdfQueryHandler(IReportService reportService, IPdfExportService pdfService) : IRequestHandler<GenerateTimeBasedReportPdfQuery, byte[]>
    {
        public async Task<byte[]> Handle(GenerateTimeBasedReportPdfQuery request, CancellationToken cancellationToken)
        {
            var report = await reportService.GetComplaintReportBasedOnTime(request.GovermentalEntityId, request.Status, request.Location);
            var pdfContents = new ComplaintTimeBasedSummaryPdfDocument(report);
            var result = pdfService.Export(pdfContents);
            return result;
        }
    }
}
