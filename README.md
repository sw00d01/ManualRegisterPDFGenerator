# ManualRegisterPDFGenerator
Output a list of peoples names to PDF using iTextSharp5
Add footer to each page of generated PDF


Create PDF using following calls to the class library....

 PDF registerpdf = new PDF();

 Byte[] bpdf = registerpdf.Create(people, eventtitle.Trim(), eventlocation.Trim(), eventtime.Trim(), type, eventlecturer.Trim(), eventmodule.Trim(), "My Title", @"/images/logo.png");
 Byte[] withfooter = registerpdf.CreateFooter(bpdf, eventtitle.Trim(), "My Sub Title");
