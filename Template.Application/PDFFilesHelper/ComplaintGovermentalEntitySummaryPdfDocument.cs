using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Template.Application.Reports.Dtos;

namespace Template.Application.PDFFilesHelper
{
    public class ComplaintGovermentalEntitySummaryPdfDocument : BasePDFDocument
    {
        private readonly IReadOnlyList<ComplaintGovermentalEntitiesReportDto> _data;

        public ComplaintGovermentalEntitySummaryPdfDocument(
            IEnumerable<ComplaintGovermentalEntitiesReportDto> data)
        {
            _data = data
                .OrderBy(x => x.GovermentalEntityId)
                .ToList();
        }

        protected override void ComposeHeader(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Text("Complaint GovermentalEntities Report")
                    .FontSize(18)
                    .SemiBold();

                column.Item().Text($"Total Complaints: {_data.Sum(x => x.ComplaintCount)}")
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
                    columns.RelativeColumn(3); // Status
                    columns.RelativeColumn(2); // Count
                    columns.RelativeColumn(3); // Percentage
                });

                table.Header(header =>
                {
                    header.Cell().Text("GovermentalEntityName").Bold();
                    header.Cell().AlignRight().Text("Count").Bold();
                    header.Cell().AlignRight().Text("Percentage").Bold();
                });

                foreach (var row in _data)
                {
                    table.Cell().Text(row.GovermentalEntityName.ToString());
                    table.Cell().AlignRight().Text(row.ComplaintCount.ToString());
                    table.Cell().AlignRight()
                        .Text($"{row.PercentageOfTotalComplaints:0.##}%");
                }

                // Footer row (Totals)
                table.Cell().Text("Total").Bold();
                table.Cell().AlignRight()
                    .Text(_data.Sum(x => x.ComplaintCount).ToString())
                    .Bold();
                table.Cell().AlignRight().Text("100%").Bold();
            });
        }
    }
}
