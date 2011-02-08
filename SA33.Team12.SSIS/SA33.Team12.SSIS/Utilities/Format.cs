using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SA33.Team12.SSIS.Utilities
{
    public class Format
    {
        public static void MergeRowBySameValue(GridView gridView, int cellIndex)
        {
            bool odd = true;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                int rowToSpan = 1;
                GridViewRow currentRow = gridView.Rows[i];
                string currentText = string.Empty;
                DataBoundLiteralControl currentCellCtrl = null;
                if (currentRow.Cells[cellIndex].Controls.Count > 0)
                    currentCellCtrl = currentRow.Cells[cellIndex].Controls[0] as DataBoundLiteralControl;
                if (currentCellCtrl != null)
                    currentText = currentCellCtrl.Text.Trim();
                else
                {
                    currentText = currentRow.Cells[cellIndex].Text.Trim();
                }
                for (int j = i; j < gridView.Rows.Count; j++)
                {
                    if (gridView.Rows.Count - 1 > j + 1)
                    {
                        GridViewRow nextRow = gridView.Rows[j + 1];
                        string nextText = string.Empty;
                        DataBoundLiteralControl nextCellCtrl = null;
                        if (nextRow.Cells[cellIndex].Controls.Count > 0)
                            nextCellCtrl = nextRow.Cells[cellIndex].Controls[0] as DataBoundLiteralControl;
                        if (nextCellCtrl != null) nextText = nextCellCtrl.Text.Trim();
                        else
                        {
                            nextText = nextRow.Cells[cellIndex].Text.Trim();
                        }
                        if (currentText == nextText)
                        {
                            rowToSpan++;
                            nextRow.Cells.RemoveAt(cellIndex);
                            if (odd)
                                nextRow.CssClass = "odd";
                            else
                                nextRow.CssClass = "";
                        }
                        else
                        {
                            i = j;
                            break;
                        }
                    }
                }
                if (odd)
                    currentRow.CssClass = "odd";
                else
                    currentRow.CssClass = "";
                odd = !odd;
                currentRow.Cells[cellIndex].RowSpan = rowToSpan;

            }
        }
    }
}