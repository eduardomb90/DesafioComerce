using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.Domain.Interfaces.Services;
using Application.Web.UI.Models;
using ClosedXML.Excel;

namespace Application.Web.UI.Tools
{
    public static class GenerateExcel
    {
        public static bool Create(string filePath, IEnumerable<SupplierViewModel> list)
        {
            var exported = false;
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Suppliers");
                var currentRow = 1;

                #region Header
                ws.Cell(currentRow, 1).Value = "SupplierId";
                ws.Cell(currentRow, 2).Value = "Fantasy Name";
                ws.Cell(currentRow, 3).Value = "Products";
                #endregion


                #region Body
                foreach (var supplier in list.ToList())
                {
                    currentRow++;
                    ws.Cell(currentRow, 1).Value = supplier.Id;
                    ws.Cell(currentRow, 2).Value = supplier.FantasyName;

                    if (supplier.Products.Count > 0)
                    {
                        var currentCell = 3;
                        foreach (var product in supplier.Products.ToList())
                        {
                            ws.Cell(currentRow, currentCell).Value = product.Name;
                            currentCell++;
                        }
                    }
                }
                #endregion

                ws.Columns().AdjustToContents();

                wb.SaveAs(filePath);
                exported = true;
            }

            return exported;
        }
    }
}
