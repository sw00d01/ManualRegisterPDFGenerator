using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace AttendanceManagement.PDFGenerator
{
    public class PDF
    {
        
        public Byte[] Create(List<string> people, string eventtitle, string eventlocation, string eventtime, string eventtype, string eventlecturer, string eventmodule, string pdftitle, string imagepath)
        {                        
            
            Document doc = new Document(PageSize.A4, 40, 40, 40, 60);

            using (MemoryStream output = new MemoryStream())
            {

                PdfWriter wri = PdfWriter.GetInstance(doc, output);
                doc.Open();

                BaseFont bfHelv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
                Font helvTitle = new Font(bfHelv, 14, Font.NORMAL, new iTextSharp.text.BaseColor(79, 129, 189));

                Paragraph pLong = new Paragraph(pdftitle, helvTitle);
                pLong.Alignment = Element.ALIGN_RIGHT;
                doc.Add(pLong);

                Font helv = new Font(bfHelv, 12, Font.NORMAL);
                Font helvAnswer = new Font(bfHelv, 10, Font.NORMAL);

                var appDomain = System.AppDomain.CurrentDomain;
                var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
              
                iTextSharp.text.Image Image = iTextSharp.text.Image.GetInstance(Path.Combine(basePath, "images", "logo.png"));
       
                Image.ScalePercent(100.0F);
                doc.Add(Image);

                PdfPTable table = new PdfPTable(2);
                // actual width of table in points
                table.TotalWidth = 500.0F;
                // fix the absolute width of the table
                table.LockedWidth = true;

                // relative col widths in proportions - 1/3 And 2/3
                float[] widths = new float[] { 1.0F, 2.0F };
                table.SetWidths(widths);
                table.HorizontalAlignment = 0;
                // leave a gap before And after the table
                table.SpacingBefore = 20.0F;

                PdfPCell tablecell1 = new PdfPCell(new Paragraph("Event", helv));
                PdfPCell tablecell2 = new PdfPCell(new Paragraph("Time", helv));
                PdfPCell tablecell3 = new PdfPCell(new Paragraph("Location", helv));
                PdfPCell tablecell4 = new PdfPCell(new Paragraph("Type", helv));
                PdfPCell tablecell5 = new PdfPCell(new Paragraph("Lecturer", helv));
                PdfPCell tablecell6 = new PdfPCell(new Paragraph("Module", helv));

                ApplyPadding(tablecell1);
                ApplyPadding(tablecell2);
                ApplyPadding(tablecell3);
                ApplyPadding(tablecell4);
                ApplyPadding(tablecell5);
                ApplyPadding(tablecell6);

                tablecell1.BackgroundColor = new iTextSharp.text.BaseColor(222, 222, 238);
                tablecell2.BackgroundColor = new iTextSharp.text.BaseColor(222, 222, 238);
                tablecell3.BackgroundColor = new iTextSharp.text.BaseColor(222, 222, 238);
                tablecell4.BackgroundColor = new iTextSharp.text.BaseColor(222, 222, 238);
                tablecell5.BackgroundColor = new iTextSharp.text.BaseColor(222, 222, 238);
                tablecell6.BackgroundColor = new iTextSharp.text.BaseColor(222, 222, 238);

                //Event
                table.AddCell(tablecell1);
                PdfPCell tablecell1a = new PdfPCell(new Paragraph(eventtitle, helvAnswer));
                ApplyPadding(tablecell1a);
                table.AddCell(tablecell1a);

                //Time
                table.AddCell(tablecell2);

                PdfPCell tablecell2a;
                tablecell2a = eventtime.Trim() == "" ? new PdfPCell(new Paragraph("-", helvAnswer)) : new PdfPCell(new Paragraph(eventtime, helvAnswer));

                ApplyPadding(tablecell2a);
                table.AddCell(tablecell2a);

                //location
                table.AddCell(tablecell3);

                PdfPCell tablecell3a;
                tablecell3a = eventlocation.Trim() == "" ? new PdfPCell(new Paragraph("-", helvAnswer)) : new PdfPCell(new Paragraph(eventlocation, helvAnswer));

                ApplyPadding(tablecell3a);
                table.AddCell(tablecell3a);

                //Type
                table.AddCell(tablecell4);

                PdfPCell tablecell4a;
                tablecell4a = eventtype.Trim() == "" ? new PdfPCell(new Paragraph("-", helvAnswer)) : new PdfPCell(new Paragraph(eventtype, helvAnswer));

                ApplyPadding(tablecell4a);
                table.AddCell(tablecell4a);

                //Lecturer
                table.AddCell(tablecell5);

                PdfPCell tablecell5a;
                tablecell5a = eventlecturer.Trim() == "" ? new PdfPCell(new Paragraph("-", helvAnswer)) : new PdfPCell(new Paragraph(eventlecturer, helvAnswer));

                ApplyPadding(tablecell5a);
                table.AddCell(tablecell5a);

                //Module
                table.AddCell(tablecell6);

                PdfPCell tablecell6a;
                tablecell6a = eventmodule.Trim() == "" ? new PdfPCell(new Paragraph("-", helvAnswer)) : new PdfPCell(new Paragraph(eventmodule, helvAnswer));

                ApplyPadding(tablecell6a);
                table.AddCell(tablecell6a);
                
                doc.Add(table);

                doc.Add(Chunk.NEWLINE);

                StringBuilder sb = new StringBuilder();

                foreach (string person in people)
                {
                    sb.Append(person);
                    sb.AppendLine();
                    sb.AppendLine();
                }

                doc.Add(new Paragraph(sb.ToString(), helvAnswer));


                doc.Close();

                return output.ToArray();
            }                       

        }
        
        public Byte[] CreateFooter(Byte[] b, string eventtitle, string footersub)
        {
            using (PdfReader reader = new PdfReader(b))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Using fs As New FileStream(filenamefirstpdf, FileMode.Create, FileAccess.Write, FileShare.None)
                    using (PdfStamper stamper = new PdfStamper(reader, ms))
                    {
                        int PageCount = reader.NumberOfPages;
                        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);

                        for (int i = 1; i <= PageCount; i++)
                        {
                            string sss = String.Format("Page {0} of {1}", i, PageCount);
                            PdfContentByte over = stamper.GetOverContent(i);

                            over.SetColorStroke(new iTextSharp.text.BaseColor(79, 129, 189));

                            over.MoveTo(40, 45);
                            over.LineTo(555, 45);
                            over.Stroke();

                            over.BeginText();
                            over.SetFontAndSize(bf, 8);
                            over.SetTextMatrix(40, 30);

                            // Make sure title length isnt too big for the page
                            string strTitle = eventtitle;

                            if ((strTitle.Length > 80))
                            {
                                strTitle = strTitle.Substring(0, 80);                               
                                strTitle = strTitle + "...";                                
                            }
                           
                            over.ShowText(strTitle + " | " + footersub);

                            over.SetTextMatrix(40, 18);
                            
                            over.SetTextMatrix(500, 30);                           
                           
                            over.ShowText(sss);
                            over.EndText();
                        }
                    }
                    return ms.ToArray();
                }
            }
        }

        private PdfPCell ApplyPadding(PdfPCell cell)
        {
            cell.PaddingLeft = 5.0F;
            cell.PaddingRight = 5.0F;
            cell.PaddingTop = 5.0F;
            cell.PaddingBottom = 5.0F;

            return cell;
        }

    }
}
