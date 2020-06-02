using AbstractTravelCompanyBusinessLogic.HelperModels;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AbstractTravelCompanyBusinessLogic.BusinessLogics
{
    public static class SaveToWord
    {
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument =
           WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                DocumentFormat.OpenXml.Wordprocessing.Body docBody = mainPart.Document.AppendChild(
                    new DocumentFormat.OpenXml.Wordprocessing.Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<StringWord> { new StringWord { Content = info.Title, Bold = true } },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                Table table = new Table();
                TableProperties props = new TableProperties(
                    new TableBorders(
                        new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }
                ));
                table.AppendChild(props);
                foreach (var store in info.Stores)
                {
                    var tableRow = new TableRow();
                    var tableCell = new TableCell();
                    tableCell.Append(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<StringWord> { new StringWord { Content = store.Name, Bold = false } },
                        TextProperties = new WordParagraphProperties
                        {
                            Bold = false,
                            Size = "24",
                            JustificationValues = JustificationValues.Both
                        }
                    }));
                    tableRow.AppendChild(tableCell);
                    table.AppendChild(tableRow);
                }
                docBody.AppendChild(table);
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }
        /// <summary>
        /// Настройки страницы
        /// </summary>
        /// <returns></returns>
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize
                    {
                        Val = paragraph.TextProperties.Size
                    });
                    if (run.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Content + " ",
                        Space = SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }
        /// <summary>
        /// Задание форматирования для абзаца
        /// </summary>
        /// <param name="paragraphProperties"></param>
        /// <returns></returns>
        private static ParagraphProperties CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new
               ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val =
                   paragraphProperties.Size
                    });
                }
                if (paragraphProperties.Bold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}
