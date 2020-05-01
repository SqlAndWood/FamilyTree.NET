using System;

namespace FamilyTree
{
   public static class BusinessLayer
   {

      //BusinessLayer.dfObjectToString 
      //If z is null, then return a string datatype
      public static string DfObjectToString(object z)
      {
          if (z is DBNull)
         {
            return string.Empty;
         }
          return z.ToString();
      }


      public static DateTime DfObjectToDateTime(object z)
      {
         if (z is DBNull)
         {
            return DateTime.Today;
         }
          var dt = Convert.ToDateTime(z);
          return dt;
      }

      //TODO: Conver to a BusinessLayer method
      public static int ObtainAge(object dateToSearch)
      {
         var nDts = DfObjectToDateTime(dateToSearch);

         var today = DateTime.Today;

         var age = today.Year - nDts.Year;
         if (nDts > today.AddYears(-age)) age--;

         return age;
      }


   } // End Class

} //End Namespace