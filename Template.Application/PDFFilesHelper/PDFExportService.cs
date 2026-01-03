using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Template.Application.PDFFilesHelper
{

    public interface IPdfExportService
    {
        byte[] Export(IDocument document);
    }

    public class PdfExportService : IPdfExportService
    {
        public byte[] Export(IDocument document)
        {
            return document.GeneratePdf();
        }
    }

}
