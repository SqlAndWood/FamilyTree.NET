using System;
using System.Collections.Generic;
using System.Drawing;

using Telerik.WinControls.UI;

namespace FamilyTree
{
   public class ListOfImages
   {
      public Bitmap Image { get; set; }
      public string ImageTag { get; set; }
      public int AgeStart { get; set; }  //Not yet implemented. ?
      public int AgeEnd { get; set; }    //Not yet implemented. ?

      public static Bitmap ObtainImage(List<ListOfImages> l, string tagSearch)
      {
         //LINQ Processing for speed while simplifying coding requirements.
         return l.Find(x => x.ImageTag == tagSearch).Image;
      }

      //dont use this. not practical... 
      public static Bitmap ObtainImage(List<ListOfImages> l, string tagSearch, int ageBetween)
      {
         //LINQ Processing for speed while simplifying coding requirements.
         return l.Find(x => x.ImageTag == tagSearch && x.AgeStart >= ageBetween && x.AgeEnd <= ageBetween).Image;

      }

      //dont use this. not practical... Yet. 
      public static Bitmap ObtainImage(List<ListOfImages> l, string tagSearch, DateTime dateToSearch)
      {

         var today = DateTime.Today;

         var age = today.Year - dateToSearch.Year;
         if (dateToSearch > today.AddYears(-age)) age--;

         //LINQ Processing for speed while simplifying coding requirements.
         return l.Find(x => x.ImageTag == tagSearch && x.AgeStart >= age && x.AgeEnd <= age).Image;

      }
   }




   public class FontController
   {
      private FontStyle _fontStyle = FontStyle.Regular;

       public void DefineFont(string fontType, string fontStyle, float fontSize, string fontColour, string backColour)
      {

         _fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), fontStyle, true);
         GetHighlightedFontColour = Color.FromName(fontColour);
         GetHighlightedBackColour = Color.FromName(backColour);
         GetHighlightedFont = new Font(fontType, fontSize, _fontStyle);

      }

      public Font GetHighlightedFont { get; private set; }

       public Color GetHighlightedFontColour { get; private set; } = Color.Black;

       public Color GetHighlightedBackColour { get; private set; } = Color.Black;
   }
   
   /*
     Use this class to fomat the radTreeView1 control.  
      
      Example Usage: 
      Create new object :   TreeNodes tn = new TreeNodes();
  
   */
    internal class RadTreeNodeController
   {
      //https://stackoverflow.com/questions/4702051/get-a-list-of-all-tree-nodes-in-all-levels-in-treeview-controls

      private List<RadTreeNode> _aUniqueListOfNodes = new List<RadTreeNode>();

      private List<ListOfImages> _imageCollection = new List<ListOfImages>();

      private int _nodeToExpandPos;

      private FontController _fontSpecifications = new FontController();


      public void TreeNodeControllerDefinition(RadTreeView rtv, int levelToExpandTo, FontController fc, List<ListOfImages> imageCollection, bool showDuplicatedFamilyMembers)
      {
         //Becasuse the array is base 0 while we like to think in Base 1.
         _nodeToExpandPos = levelToExpandTo - 1;

         _fontSpecifications = fc;

         _imageCollection = imageCollection;

         foreach (RadTreeNode node in rtv.Nodes)
         {

            //Add the node into a Node Object. May prove useful later on.
            GetRadTreeNodes.Add(node);

            TestNodeExpansion(node);
            FormatNode(node);

            TraverseChildNodes(node);

         }

         //http://www.telerik.com/forums/nullreferenceexception-in-radtreeview-when-checking-or-unchecking-node
         GetRadTreeNodeCollection.Add(rtv.Nodes);

         if (showDuplicatedFamilyMembers)
         {
            HideDuplicateFamilyMembersInTheTree(GetRadTreeNodes);
         
         }
         else
         {
            EnsureAllNodesVisible(GetRadTreeNodes);
         }

       

      }



      public void SelectSpecificNodeInTree(int toSelect)
      {
          if (_aUniqueListOfNodes.Count == 0) return;
          System.Data.DataColumnCollection columnHeadingNames = ((System.Data.DataRowView)_aUniqueListOfNodes[0].DataBoundItem).Row.Table.Columns;
          //The Person_SK is our column of interest for all RadTreeNodes
          int posOfColumnHeading = columnHeadingNames.IndexOf("Person_SK");

          foreach (RadTreeNode node in _aUniqueListOfNodes)
          {
              string curentListedNodePSk = ((System.Data.DataRowView)node.DataBoundItem).Row.ItemArray[posOfColumnHeading].ToString();

              int clnpSk = Convert.ToInt32(curentListedNodePSk);

              if (clnpSk == toSelect)
              {
                  node.Selected = true;

              }


          }
      }


      //There must be a better way to perform this! 
      //  However, this produces an output we desire and 
      //private List<RadTreeNode> HideDuplicateFamilyMembersInTheTree(List<RadTreeNode> ListOfNodes)
       private void HideDuplicateFamilyMembersInTheTree(List<RadTreeNode> listOfNodes)
        {

         //This RadTreeNode collection will be supplied back to the calling routine.
         List<RadTreeNode> aUniqueListOfNodes = new List<RadTreeNode>();

         List<RadTreeNode> nodesToRemove = new List<RadTreeNode>();

         if (GetRadTreeNodes.Count != 0 ) {

            System.Data.DataColumnCollection columnHeadingNames = ((System.Data.DataRowView)GetRadTreeNodes[0].DataBoundItem).Row.Table.Columns;
         //The Person_SK is our column of interest for all RadTreeNodes
         int posOfColumnHeading = columnHeadingNames.IndexOf("Person_SK");     

         var posOfDateOfBirth = columnHeadingNames.IndexOf("DOB");
            
         foreach (RadTreeNode node in listOfNodes)
         {
             //reset this on each Loop
             bool doesNodeAlreadyExist = false;

            // Always add the first node regardless of any Business Rule Requirement.
            if (aUniqueListOfNodes.Count == 0)
            {
               aUniqueListOfNodes.Add(node);
            }
            else
            {
             
               string curentListedNodePSk = ((System.Data.DataRowView)node.DataBoundItem).Row.ItemArray[posOfColumnHeading].ToString();

               var ageOfIndividual = BusinessLayer.ObtainAge(((System.Data.DataRowView)node.DataBoundItem).Row.ItemArray[posOfDateOfBirth].ToString());

               int currentListedNodePSkLevel = node.Level;

               int startUniqueListOfNodeCount = aUniqueListOfNodes.Count;

               //bizaare if aListToReturn grows, then this number does too? (dynamically in loop)
               for (int i = 0; i < startUniqueListOfNodeCount; i++)
               {
                  //For this particular Node in the Unique List of Nodes, identify the Person_SK 
                  string uniqueNodePSk = ((System.Data.DataRowView)aUniqueListOfNodes[i].DataBoundItem).Row.ItemArray[posOfColumnHeading].ToString();

                  //When the node.PersonSK equals the UniqueNode.PersonSK, we need to perform more matches, else loop to the next person..
                   if (curentListedNodePSk != uniqueNodePSk) continue;
                   //Each Person_SK node can actually have different levels. we are interested in the Level that is closest to zero.
                   int uniqueNodePSkLevel = aUniqueListOfNodes[i].Level;
                   doesNodeAlreadyExist = true;

                   if (currentListedNodePSkLevel < uniqueNodePSkLevel )
                     {
                       //This means we already have a node in this UniqueListOfNodes that needs to be removed
                       //But you are not allowed to remove nodes while iterating through them! 
                       //Add the found node to a list for removal once the iteration is complete.
                       nodesToRemove.Add(aUniqueListOfNodes[i]);
                       //aUniqueListOfNodes.Remove(node);

                       //while our new node needs to be added.
                       aUniqueListOfNodes.Add(node);

                   }
                   
                   //We have achieved our objective and found this node in the list.
                   //rather than looping more nodes that wont match our subset, exit the For loop
                   break;
               }

               if (doesNodeAlreadyExist == false)
               {
                  //The node doesnt exist in our dataset, add it in.
                  aUniqueListOfNodes.Add(node);
               }

            } //End For


         }


         } // If Treenode Count != 0

         //Now that we know what Nodes need to be removed...
         foreach (RadTreeNode ntR in nodesToRemove)
         {
            aUniqueListOfNodes.Remove(ntR);
         }


         //Hide all nodes  NB Not allowed to perform this task while within the node.  
         foreach (RadTreeNode uniqueNodes in listOfNodes)
         {
            uniqueNodes.Visible = false;
         }

         //only show nodes we care about.
         foreach (RadTreeNode uniqueNodes in aUniqueListOfNodes)
         {
            uniqueNodes.Visible = true;
         }


         _aUniqueListOfNodes = aUniqueListOfNodes;
         //returning the List is not required, but may allow for futher use in later programming
         //return aUniqueListOfNodes;

      }

     
      //There may be a better way to do this too.  
   private void EnsureAllNodesVisible(List<RadTreeNode> listOfNodes)
      {

         foreach (RadTreeNode uniqueNodes in listOfNodes)
         {
            uniqueNodes.Visible = true;
         }

      }



      private void TraverseChildNodes(RadTreeNode node)
      {

         foreach (RadTreeNode childNode in node.Nodes)
         {

            //Add the node into a Node Object. May prove useful later on.
            GetRadTreeNodes.Add(childNode);
            GetRadTreeNodeCollection.Add(node.Nodes);

            TestNodeExpansion(node);
            FormatNode(childNode);

            TraverseChildNodes(childNode);

         }

      }

      //http://www.telerik.com/forums/show-hide-nodes

      private void TestNodeExpansion(RadTreeNode node)
      {
         if (node.Level < _nodeToExpandPos)
         {
            node.Expand();
         }
         else
         {
            node.Collapse();
         }
      }

      //Can this be improved, so 'Text' is not used at this level?
      private void FormatNode(RadTreeNode node)
      {
         FormatScreeningClearence(node, "Screening_Determination", "CLEARED");
         FormatGender(node, "Gender_Code", "DOB");
      }

      private void FormatScreeningClearence(RadTreeNode node, string columnToLocate, string textToIdentify)
      {

         System.Data.DataColumnCollection columnHeadingNames = ((System.Data.DataRowView)node.DataBoundItem).Row.Table.Columns;
         int pos = columnHeadingNames.IndexOf(columnToLocate);

         string screened = ((System.Data.DataRowView)node.DataBoundItem).Row.ItemArray[pos].ToString();

          if (screened != textToIdentify) return;
          node.Font = _fontSpecifications.GetHighlightedFont; //_ClearedRelationshipsFont;
          node.ForeColor = _fontSpecifications.GetHighlightedFontColour; // _ClearedRelationshipsColour;
          node.BackColor = _fontSpecifications.GetHighlightedBackColour;
          node.EnsureVisible();
          node.Expand();
          // node.Selected = true;  //Decided not to do this, as it does not add value to the application.
      }

      //This uses some simple business rules, which I would like to declare much earlier in the application. (Have not decided what methodology to use yet).
      private void FormatGender(RadTreeNode node, string genderColumn, string dobColumn)
      {

         // var today = DateTime.Today;

         System.Data.DataColumnCollection columnHeadingNames = ((System.Data.DataRowView)node.DataBoundItem).Row.Table.Columns;
         int genderColumnPos = columnHeadingNames.IndexOf(genderColumn);

         string gender = ((System.Data.DataRowView)node.DataBoundItem).Row.ItemArray[genderColumnPos].ToString();

         int dobColumnPos = columnHeadingNames.IndexOf(dobColumn);

        // var DOB = (string)(((System.Data.DataRowView)node.DataBoundItem).Row).ItemArray[DOBColumnPos].ToString();

         DateTime dtDob = Convert.ToDateTime(((System.Data.DataRowView)node.DataBoundItem).Row.ItemArray[dobColumnPos].ToString());
         var age = DateTime.Today.Year - dtDob.Year;
         // if (dtDOB > today.AddYears(-age)) age--; //Subtract one year in event of Leap Year saga.  Seemed to cause issues and really, near enough is good enough for simple pics.

         /*
          For testing, I will just hard code the Numbers and files in.
          */

         switch (gender)
         {
             case "F" when age >= 0 && age <= 2:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "BabyFemale");
                 break;
             case "M" when age >= 0 && age <= 2:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "BabyMale");
                 break;
             case "F" when age >= 3 && age <= 10:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "ChildFemale");
                 break;
             case "M" when age >= 3 && age <= 10:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "ChildMale");
                 break;
             case "F" when age >= 11 && age <= 17:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "TeenagerFemale");
                 break;
             case "M" when age >= 11 && age <= 17:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "TeenagerMale");
                 break;
             case "F" when age >= 18 && age <= 64:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "AdultFemale");
                 break;
             case "M" when age >= 18 && age <= 64:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "AdultMale");
                 break;
             case "F" when age >= 65 && age <= 200:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "SeniorFemale");
                 break;
             case "M" when age >= 65 && age <= 200:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "SeniorMale");
                 break;
             default:
                 node.Image = ListOfImages.ObtainImage(_imageCollection, "Unknown");
                 break;
         }

      }


      //Return this list via: List<RadTreeNode>  rtn =  tn.getRadTreeNodes;    
      public List<RadTreeNode> GetRadTreeNodes { get; } = new List<RadTreeNode>();

       //https://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.treenode.childnodes(v=vs.110).aspx
      //Return this list via: List<RadTreeNodeCollection> rtnc = tn.getRadTreeNodeCollection;
      public List<RadTreeNodeCollection> GetRadTreeNodeCollection { get; } = new List<RadTreeNodeCollection>();
   }
}

