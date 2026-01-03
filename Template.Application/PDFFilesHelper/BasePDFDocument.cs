using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Template.Application.PDFFilesHelper;

public abstract class BasePDFDocument : IDocument
{
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(30);
            page.DefaultTextStyle(x => x.FontSize(11));

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    protected abstract void ComposeHeader(IContainer container);
    protected abstract void ComposeContent(IContainer container);

    protected virtual void ComposeFooter(IContainer container)
    {
        container.AlignCenter().Text(text =>
        {
            text.Span("Generated on ");
            text.Span(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"));
        });
    }
}