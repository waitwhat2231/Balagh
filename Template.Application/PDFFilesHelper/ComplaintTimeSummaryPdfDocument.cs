using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using Template.Application.PDFFilesHelper;
using Template.Application.Reports.Dtos;

public class ComplaintTimeBasedSummaryPdfDocument
    : BasePDFDocument
{
    private readonly IReadOnlyList<ComplaintTimeBasedReportDto> _data;

    public ComplaintTimeBasedSummaryPdfDocument(
        IEnumerable<ComplaintTimeBasedReportDto> data)
    {
        _data = data
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToList();
    }

    protected override void ComposeHeader(IContainer container)
    {
        var totalComplaints = _data.Sum(x => x.ComplaintCount);

        container.Column(column =>
        {
            column.Item().Text("Complaint Time-Based Summary Report")
                .FontSize(18)
                .SemiBold();

            column.Item().Text($"Total Complaints: {totalComplaints}")
                .FontSize(10)
                .FontColor(Colors.Grey.Darken1);
        });
    }

    protected override void ComposeContent(IContainer container)
    {
        container.PaddingTop(15).Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(3); // Period
                columns.RelativeColumn(2); // Count
                columns.RelativeColumn(3); // Percentage
            });

            table.Header(header =>
            {
                header.Cell().Text("Period").Bold();
                header.Cell().AlignRight().Text("Complaints").Bold();
                header.Cell().AlignRight().Text("Percentage").Bold();
            });

            foreach (var row in _data)
            {
                table.Cell().Text(FormatPeriod(row.Year, row.Month));
                table.Cell().AlignRight().Text(row.ComplaintCount.ToString());
                table.Cell().AlignRight()
                    .Text($"{row.PercentageOfTotalComplaints:0.##}%");
            }

            // Totals row
            table.Cell().Text("Total").Bold();
            table.Cell().AlignRight()
                .Text(_data.Sum(x => x.ComplaintCount).ToString())
                .Bold();
            table.Cell().AlignRight().Text("100%").Bold();
        });
    }

    private static string FormatPeriod(int year, int month)
    {
        var monthName = CultureInfo.CurrentCulture
            .DateTimeFormat
            .GetMonthName(month);

        return $"{monthName} {year}";
    }
}
