using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Layout.Properties;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Geom;

namespace xls_Domain.Extensions.Files
{
    public class ExtensionPDF
    {
        public class TextFooterEventHandler : IEventHandler
        {
            protected Document doc;
            protected PdfFont font;
            protected bool showAdsPanutrir = true;

            public TextFooterEventHandler(Document doc, PdfFont font)
            {
                this.doc = doc;
                this.font = font;
                //this.showAdsPanutrir = showAds360;
            }

            public void HandleEvent(Event currentEvent)
            {
                if (showAdsPanutrir)
                {
                    PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                    Rectangle pageSize = docEvent.GetPage().GetPageSize();

                    float coordX = ((pageSize.GetLeft() + doc.GetLeftMargin()) + (pageSize.GetRight() - doc.GetRightMargin())) / 2;
                    float headerY = pageSize.GetTop() - doc.GetTopMargin() + 10;
                    float footerY = doc.GetBottomMargin();
                    Canvas canvas = new Canvas(docEvent.GetPage(), pageSize);
                    canvas

                        // If the exception has been thrown, the font variable is not initialized.
                        // Therefore null will be set and iText will use the default font - Helvetica
                        .SetFont(font)
                        .SetFontSize(5)
                        .SetFontColor(ColorConstants.GRAY)
                        .ShowTextAligned($"GERADO EM {DateTime.Now.FormatDatePtBRs(true)} PELA EMPRESA - CONTATO (XX) XXXX-XXXX - https://panutrir.com.br", coordX, footerY - 20, TextAlignment.CENTER)
                        .Close();
                }
            }
        }
    }
}

