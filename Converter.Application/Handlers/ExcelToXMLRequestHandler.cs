using Converter.Application.Requests;
using MediatR;
using OfficeOpenXml;
using System.Xml.Linq;

namespace Converter.Application.Handlers
{
    public class ExcelToXMLRequestHandler : IRequestHandler<ExcelToXMLRequest, string>
    {
        public async Task<string> Handle(ExcelToXMLRequest request, CancellationToken cancellationToken)
        {
            string xml;
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var stream = request.File.OpenReadStream())
            {
                var excelPackage = new ExcelPackage(stream);
                var worksheet = excelPackage.Workbook.Worksheets[0];

                var headers = worksheet.Cells["A1:AE1"]
                    .Select(cell => cell.Text)
                    .ToList();

                var dataRows = worksheet.Cells["A2:AE" + worksheet.Dimension.Rows]
                    .Select(row => row.Select(cell => cell.Text).ToList())
                    .ToList();

                 xml = await Task.Run(() => ConvertToXml(headers, dataRows).ToString());
            }

            return xml;
        }


        private XElement ConvertToXml(List<string> headers, List<List<string>> dataRows)
        {
            XElement auditElement = new XElement("audit");

            // Add the initial elements before the start of the <order> elements
            for (int i = 0; i < 7; i++)
            {
                if(headers[i].ToLower() == "creation_date")
                {
                    DateTime dateVal;
                    
                    DateTime.TryParse(dataRows[i].FirstOrDefault(),out dateVal);
                    auditElement.Add(new XElement(headers[i].ToLower(), dateVal.ToString("yyyy-MM-dd")));
                }
                else 
                {
                    auditElement.Add(new XElement(headers[i].ToLower(), dataRows[i].FirstOrDefault()));
                }
            }

            XElement orderElement = new XElement("order");
            XElement orderEnumElement = null;
            XElement artElement = null;
            XElement artEnumElement = null;

            int realRow = 0;
            for (int rowIndex = 0; rowIndex < dataRows.Count; rowIndex++)
            {
               
                int colIndex = rowIndex;
                if (rowIndex >= headers.Count)
                {
                    colIndex = rowIndex - (headers.Count * realRow);
                }

                if (headers[colIndex].ToLower() == "ord_n")
                {
                    orderEnumElement = new XElement("orderenum");
                    var fieldName = headers[colIndex].ToLower();
                    XElement el = new XElement(fieldName, dataRows[rowIndex][0]);
                    orderEnumElement.Add(el);
                }

                if(headers[colIndex].ToLower() == "ord_d"
                || headers[colIndex].ToLower() == "doc_date")
                {
                    DateTime dateVal;
                    var fieldName = headers[colIndex].ToLower();
                    DateTime.TryParse(dataRows[rowIndex][0], out dateVal);

                    XElement el = new XElement(fieldName, dateVal.ToString("yyyy-MM-dd"));
                    orderEnumElement.Add(el);
                }

                if (headers[colIndex].ToLower() == "doc_n")
                {
                    if(orderEnumElement != null)
                    {
                        var fieldName = headers[colIndex].ToLower();
                        XElement el = new XElement(fieldName, dataRows[rowIndex][0]);
                        orderEnumElement.Add(el);
                    }
             
                }

                if (headers[colIndex].ToLower() == "art_name")
                {
                    artElement = new XElement("art");
                    artEnumElement = new XElement("artenum");
                    
                    var fieldName = headers[colIndex].ToLower();
                    XElement el = new XElement(fieldName, dataRows[rowIndex][0]);
                    artEnumElement.Add(el);
                }

                if (headers[colIndex].ToLower() == "art_quant"
                    || headers[colIndex].ToLower() == "art_price"
                    || headers[colIndex].ToLower() == "art_vat_rate"
                    || headers[colIndex].ToLower() == "art_vat")
                    
                {
                    var fieldName = headers[colIndex].ToLower();
                    XElement el = new XElement(fieldName, dataRows[rowIndex][0]);
                    artEnumElement.Add(el);
                }

                if (headers[colIndex].ToLower() == "art_sum")
                {
                    var fieldName = headers[colIndex].ToLower();
                    XElement el = new XElement(fieldName, dataRows[rowIndex][0]);
                    artEnumElement.Add(el);

                    artElement.Add(artEnumElement);
                    orderEnumElement.Add(artElement);
                }

                if (headers[colIndex].ToLower() == "ord_total1" ||
                    headers[colIndex].ToLower() == "ord_disc" ||
                    headers[colIndex].ToLower() == "ord_vat" ||
                    headers[colIndex].ToLower() == "ord_total2")
                {
                    var fieldName = headers[colIndex].ToLower();
                    XElement el = new XElement(fieldName, dataRows[rowIndex][0]);

                    orderEnumElement.Add(el);
                }

                if (headers[colIndex].ToLower() == "paym")
                {
                    var fieldName = headers[colIndex].ToLower();
                    XElement el = new XElement(fieldName, dataRows[rowIndex][0]);

                    orderEnumElement.Add(el);
                    orderElement.Add(orderEnumElement);
                }

                if (headers[colIndex].ToLower() == "r_total")
                {
                    realRow++;
                }
            }

            for (int i = 0; i < headers.Count; i++)
            {
                if (headers[i].ToLower() == "r_ord" || headers[i].ToLower() == "r_total")
                {
                    var fieldName = headers[i].ToLower();
                    var value = string.IsNullOrEmpty(dataRows[i][0]) ? 0.ToString() : dataRows[i][0].ToString();
                    XElement el = new XElement(fieldName, value);

                    auditElement.Add(el);
                }
            }

            auditElement.Add(orderElement);

            return auditElement;
        }
    }
}