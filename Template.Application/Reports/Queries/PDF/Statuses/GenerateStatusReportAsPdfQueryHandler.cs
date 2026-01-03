using MediatR;
using Template.Application.PDFFilesHelper;
using Template.Application.Reports.Services;

namespace Template.Application.Reports.Queries.PDF.Statuses
{
    class GenerateStatusReportAsPdfQueryHandler(IReportService reportService, IPdfExportService pdfService) : IRequestHandler<GenerateStatusReportAsPdfQuery, byte[]>
    {
        public async Task<byte[]> Handle(GenerateStatusReportAsPdfQuery request, CancellationToken cancellationToken)
        {
            var report = await reportService.GetComplaintReportForStatuses(request.From, request.To, request.GovermentalEntityId, request.Location);
            var pdfContents = new ComplaintStatusSummaryPdfDocument(report);
            var result = pdfService.Export(pdfContents);
            return result;

        }
    }
}
