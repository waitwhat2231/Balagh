using MediatR;
using Template.Application.PDFFilesHelper;
using Template.Application.Reports.Services;

namespace Template.Application.Reports.Queries.PDF.GovermentalEntity
{
    class GenerateGovermentalEntityReportAsPdfQueryHandler(IReportService reportService, IPdfExportService pdfService) : IRequestHandler<GenerateGovermentalEntityReportAsPdfQuery, byte[]>
    {
        public async Task<byte[]> Handle(GenerateGovermentalEntityReportAsPdfQuery request, CancellationToken cancellationToken)
        {
            var report = await reportService.GetComplaintReportForGovermentalEntities(request.From, request.To, request.Status, request.Location);
            var pdfContents = new ComplaintGovermentalEntitySummaryPdfDocument(report);
            var result = pdfService.Export(pdfContents);
            return result;
        }
    }
}
