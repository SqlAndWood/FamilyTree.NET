using System;
using System.Data;
using System.Windows.Forms;

namespace FamilyTree
{

   public static class ControlExtentions { }
   public static class DataGridViewExtentions
   {

      public static void HideColumn(this DataGridView dgv, params string[] columnNames)
      {
          if (dgv == null) return;
          foreach (var columnName in columnNames)
          {
              // ReSharper disable once PossibleNullReferenceException
              dgv.Columns[columnName.Trim()].Visible = false;
          }
      }


      public static void ShowColumn(this DataGridView dgv, params string[] columnNames)
      {
            
         foreach (DataGridViewColumn column in dgv.Columns)
         {
            column.Visible = false;
         }

         foreach (var columnName in columnNames)
         {
             // ReSharper disable once PossibleNullReferenceException
             dgv.Columns[columnName.Trim()].Visible = true;
         }
      }


   }
}

namespace FamilyTree
{
   public static class MultiColumnComboBoxExtentions
   {
      public static void HideColumn(this Telerik.WinControls.UI.RadMultiColumnComboBox rmccb, params string[] columnNames)
      {
         foreach (var columnName in columnNames)
         {
            rmccb.EditorControl.Columns[columnName.Trim()].IsVisible = false;
         }
      }


      public static void ShowColumn(this Telerik.WinControls.UI.RadMultiColumnComboBox rmccb, params string[] columnNames)
      {


         foreach (var colName in rmccb.EditorControl.Columns)
         {
            colName.IsVisible = false;
         }

         foreach (var columnName in columnNames)
         {
            rmccb.EditorControl.Columns[columnName.Trim()].IsVisible = true;
         }
      }


   }
}

namespace FamilyTree
{
   public static class DataTableExtentions
   {
      public static void SetColumnsOrder(this DataTable dt, params string[] columnNames)
      {
         int columnIndex = 0;
         foreach (var columnName in columnNames)
         {
            dt.Columns[columnName.Trim()].SetOrdinal(columnIndex);
            columnIndex++;
         }
      }
   }
}

namespace FamilyTree
{
   public static class DataColumnExtentions
   {
      //This loops each and every field in the dataset. not really feasable.  Not used in 'Family Tree' application.
      // dtChild.Columns["DOB"].Convert(  val => DateTime.Parse(val.ToString()).ToString("dd/MMM/yyyy"));
      public static void Convert<T>(this DataColumn column, Func<object, T> conversion)
      {
         foreach (DataRow row in column.Table.Rows)
         {
            row[column] = conversion(row[column]);
         }
      }
   }
}

