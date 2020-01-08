# ManualRegisterPDFGenerator
## Output a list of peoples names to PDF using iTextSharp5
<br /><br />
Add footer to each page of generated PDF
<br /><br />

Create PDF using following calls to the class library....
<br /><br />
<code> PDF registerpdf = new PDF();</code>
<br /><br />
 <code>Byte[] bpdf = registerpdf.Create(people, eventtitle.Trim(), eventlocation.Trim(), eventtime.Trim(), type, eventlecturer.Trim(), eventmodule.Trim(), "My Title", @"/images/logo.png");</code>
 <br /><br />
 <code>
 Byte[] withfooter = registerpdf.CreateFooter(bpdf, eventtitle.Trim(), "My Sub Title");
</code>
