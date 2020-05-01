//Required for unzipping I think.

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;


namespace FamilyTree
{
    public partial class Form2 : Form
    {
        private RadTreeNodeController _tn; //= new RadTreeNodeController();

        private readonly bool _applicationTestingMode = Properties.Settings.Default.ApplicationTestingMode;

        private string _strFileName = Properties.Settings.Default.ApplicationEncryptedFilePathAndName_Deprecated;

        private readonly FontController _fontSpecifications = new FontController();

        private readonly DataSet _dsAllData = new DataSet("All");
        private DataTable _dtAllData;

        private DataSet _dsChild = new DataSet("Child");
        private DataTable _dtChild;

        private DataSet _dsCleared = new DataSet("Cleared");
        private DataTable _dtCleared;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            FormConfigurationsOnLoad();

            AddMenuAndMenuItemsToForm();

            //Show the form before the Dialog Box, so that the Form takes focus on the screen.
            Show();

            OpenFileDialogBox();

            ResetForm();
        }

        private void FormConfigurationsOnLoad()
        {

            //icon provided from Mark Van Raalte via Email: 1st September, 2017 @ 2:06 PM
            Icon = new Icon("Resources/" + Properties.Settings.Default.ApplicationIconFileName);

            int formWidth = Properties.Settings.Default.ApplicationFormNormalWidth;
            int formHeight = Properties.Settings.Default.ApplicationFormNormalHeight;
            Size = new Size(formWidth, formHeight);

            Text = Properties.Settings.Default.ApplicationName;
            MaximizeBox = Properties.Settings.Default.ApplicationMaximiseButton;

            lblDepartment.Text = Properties.Settings.Default.ApplicationCreatedByDepartment;
            lblBusinessGroup.Text = Properties.Settings.Default.ApplicationCreatedByBusinessGroup;

            Left = Properties.Settings.Default.FormLocationOnScreenLeft;
            Top = Properties.Settings.Default.FormLocationOnScreenTop;


            pbSecurityClearences.BorderStyle = BorderStyle.None;
            tb_screening_Determination.BorderStyle = BorderStyle.None;

            //Need to implement this still
            _fontSpecifications.DefineFont(
                Properties.Settings.Default.HighlightedFont,
                Properties.Settings.Default.HighlightedFontStyle,
                Properties.Settings.Default.HighlightedFontSize,
                Properties.Settings.Default.HighlightedFontColour,
                Properties.Settings.Default.HighlightedBackColour);

            //Dont show a blue selection by default when the dgv is clicked.
            dgvClearedRelatives.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvClearedRelatives.DefaultCellStyle.SelectionForeColor = Color.Black;
         
            tb_RelationshipTrace.BorderStyle = BorderStyle.FixedSingle;
            tb_address_Line.BorderStyle = BorderStyle.FixedSingle;

            tb_RelationshipTrace.Visible = true;
        }

        private void myMenuItemOpen_Click(object sender, EventArgs e)
        {
            ResetForm();
            OpenFileDialogBox();
        }

        private void OpenFileDialogBox()
        {
            OpenFileDialog fileDialogBox = new OpenFileDialog
            {
                InitialDirectory = Properties.Settings.Default.ApplicationInitialDirectory ,
                Filter = @"zip files (*.zip)|*.zip" , //|csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*",
                //Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1
            };

            fileDialogBox.ShowDialog();

            //What to do when the file has been selected?
            _strFileName = fileDialogBox.FileName;

            // A proof of concept is being performed to identify if an password protected zip file can be opened.

            if (_strFileName == "") return;
            string nameOfDecryptedFile = DecryptFile(fileDialogBox.FileName, fileDialogBox.SafeFileName);
            if (nameOfDecryptedFile == "") return;
            _strFileName = nameOfDecryptedFile;
            //Only access the File once.
            LoadCsvFileIntoDataTable();

            DeleteDecryptedFile(nameOfDecryptedFile);

            FilterAllDataForChild();

            BindMultiColumnComboBoxControl();
            MultiColumnComboBoxHighlighting();
            BindMultiColumnComboBoxFiltering();

            //is this still necesary? I guess so.
            ExpandCollapse(true, Properties.Settings.Default.FamilyTreeControlLevelDepthToDisplay);

            //show a dialog box to say data has been loaded.
            if (_applicationTestingMode == false)
            {
                //place the message box in here when you start to get annoyed with it.
            }

            MessageBox.Show(Properties.Settings.Default.ApplicationDataSuccessfullySelectedNotificationLine1 +
                            @"\n" + Properties.Settings.Default
                                .ApplicationDataSuccessfullySelectedNotificationLine2
                , Properties.Settings.Default.ApplicationName
                , MessageBoxButtons.OK
                , MessageBoxIcon.Information);
        }


        private string DecryptFile(string directory, string fileToDecrypt)
        {
            //Just add the reference "Microsoft Visual Basic .NET Runtime" to your project.
            // import using Microsoft.VisualBasic;

            //May need to create a dedicated form , use this idea: https://msdn.microsoft.com/en-us/library/aa983584(v=vs.71).aspx


            try
            {
                string filenameToReturn = "";

                //This is a POC only. Some work to do to get it to work much better.
                string fileType = fileToDecrypt.Substring(fileToDecrypt.Length - 3);

                // string path = Directory.GetCurrentDirectory();

                if (fileType != "zip") return fileToDecrypt;
                //  string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                //OpenFileDialog fileDialogBox = new OpenFileDialog();
                string dummyFileName = "Extract File Here";

                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = dummyFileName,
                    InitialDirectory = documentPath
                };


                if (sf.ShowDialog() != DialogResult.OK) return filenameToReturn;
                // Now here's our save folder
                string savePath = Path.GetDirectoryName(sf.FileName);

                string basicFileName = fileToDecrypt.Substring(0, fileToDecrypt.Length - 4) + ".csv";

                string fullUnzipLoation = savePath + "\\" + basicFileName;

                string secretSquirrel = Interaction.InputBox(
                    "Please enter a password to decrypt this zip file.", "File requires a password", "", 10,
                    10);

                //https://www.codeproject.com/Articles/997471/Csharp-Password-TextBox-with-preview

                using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(directory))
                {
                    //Dynamic name for file?
                    Ionic.Zip.ZipEntry e = zip[basicFileName];

                    if (secretSquirrel != "")
                    {
                        e.Password = secretSquirrel;
                    }
                    //It would be great if we could skip 
                    e.Extract(savePath, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                    filenameToReturn = fullUnzipLoation;
                }

                return filenameToReturn;
            }

            catch (Ionic.Zip.BadPasswordException)
            {
                Interaction.MsgBox("Wrong password! \n\nFile not decrypted.", MsgBoxStyle.OkOnly,
                    "File requires the correct password");

                return "";
            }

            catch (NullReferenceException)
            {
                //Possible that the wrong password was provided.
                Interaction.MsgBox("Wrong password! \n\nFile not decrypted.", MsgBoxStyle.OkOnly,
                    "File requires the correct password");
                return "";
            }
        }

        private void DeleteDecryptedFile(string fileToDelete)
        {
            File.Delete(fileToDelete);
        }

        //Load the entire file into a single DataTable / DataSet. 
        // This will then be manipulated to pass to the other DataTables / DataSets.
        private void LoadCsvFileIntoDataTable()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source=" +
                                                       Path.GetDirectoryName(_strFileName) +
                                                       Properties.Settings.Default.ApplicationConnectionString);
            conn.Open();
            string sqlSelectStatment = Properties.Settings.Default.SQL4Application;
            OleDbDataAdapter fullDataAdapter =
                new OleDbDataAdapter(sqlSelectStatment + " " + Path.GetFileName(_strFileName), conn);

            fullDataAdapter.Fill(_dsAllData);

            _dtAllData = _dsAllData.Tables[0].DefaultView.ToTable();

            _dtAllData.DefaultView.Sort = Properties.Settings.Default.FamilyTreeControlSortOrder;
        }

        private void FilterAllDataForChild()
        {
            //If I can copy specific columns, this may imporve the speed of our application

            //Only copy over the fields that we need .  at the moment, there may be more fields here than we need.
            string n = Properties.Settings.Default.ChildComboBoxDataTableColumns;
            string[] array = Array.ConvertAll(n.Split(','), p => p.Trim());

            //Create a temp DataSet and DataTable to stor our desired data into ..  
            var dsTemp = _dsAllData.Copy();

            //Childeren (which we are interested in) are at zero level of the Data.  
            dsTemp.Tables[0].DefaultView.RowFilter = "Relationship_Level=0";

            var dtTemp = dsTemp.Tables[0].DefaultView.ToTable();

            //https://stackoverflow.com/questions/6183621/copying-data-of-only-few-columns-to-one-more-data-table
            // DataTable newTable = dtAllData.DefaultView.ToTable(false, "ColumnName1", "ColumnName2", "ColumnName3", "ColumnName4", "ColumnName5");
            //Use the global Variable and copy only desired information
            _dtChild = dtTemp.DefaultView.ToTable(false, array);

            //This is problematic, as I have copied the entire dataset into here.
            _dsChild = dsTemp.Copy();

            _dtChild.DefaultView.Sort = Properties.Settings.Default.ChildComboBoxSortOrder;

            //For presentation, sort the order of the Combo box.
            string sortOrder = Properties.Settings.Default.ChildComboBoxColumnOrder;
            //string[] array = n.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);  //Old code that did not trim spaces
            string[] aSortOrder = Array.ConvertAll(sortOrder.Split(','), p => p.Trim());

            _dtChild.SetColumnsOrder(aSortOrder);
        }

        private void BindMultiColumnComboBoxControl()
        {
            radMultiColumnComboBox1.EditorControl.DataSource = _dtChild;
            //Format this column as soon as possible. Note that Telerik uses a fancy day / month / year 
            radMultiColumnComboBox1.Columns["DOB"].FormatString = @"{0:d/MM/yyyy}";
            radMultiColumnComboBox1.Columns["Relatives Cleared"].TextAlignment = ContentAlignment.MiddleLeft;

            string n = Properties.Settings.Default.ChildComboBoxShowColumns;
            string[] array = Array.ConvertAll(n.Split(','), p => p.Trim());
            radMultiColumnComboBox1.ShowColumn(array);

            radMultiColumnComboBox1.BestFitColumns();

            int heightOfComboBox = Properties.Settings.Default.ChildComboBoxSelectionHeight;
            radMultiColumnComboBox1.DropDownMinSize =
                new Size(radMultiColumnComboBox1.Size.Width,
                    heightOfComboBox); // First number is Width, Second Number is height

            radMultiColumnComboBox1.DisplayMember = "FullName";
        }

        private void MultiColumnComboBoxHighlighting()
        {
            //Loop radMCCB and highlight when the associated [Relatives Cleared] > 0
            //http://docs.telerik.com/devtools/winforms/gridview/rows/conditional-formatting-rows
            ConditionalFormattingObject obj =
                new ConditionalFormattingObject("Condition [Relatives Cleared] > 0", ConditionTypes.Greater, "0", "",
                    true)
                {
                    RowBackColor = _fontSpecifications.GetHighlightedBackColour,
                    RowFont = _fontSpecifications.GetHighlightedFont,
                    RowForeColor = _fontSpecifications.GetHighlightedFontColour
                };

            radMultiColumnComboBox1.Columns["Relatives Cleared"].ConditionalFormattingObjectList.Add(obj);
        }

        private void BindMultiColumnComboBoxFiltering()
        {
            //http://www.telerik.com/support/kb/winforms/details/use-custom-filtering-to-search-in-radmulticolumncombobox

            radMultiColumnComboBox1.AutoFilter = true;

            FilterDescriptor filter = new FilterDescriptor
            {
                PropertyName = radMultiColumnComboBox1.DisplayMember,
                Operator = FilterOperator.Contains
            };


            radMultiColumnComboBox1.EditorControl.MasterTemplate.FilterDescriptors.Add(filter);
            radMultiColumnComboBox1.MultiColumnComboBoxElement.EditorControl.EnableCustomFiltering = true;

            radMultiColumnComboBox1.MultiColumnComboBoxElement.EditorControl.CustomFiltering +=
                EditorControl_CustomFiltering;
            radMultiColumnComboBox1.KeyDown += radMultiColumnComboBox1_KeyDown;
        }

        //Clean this up!
        private void IdentifyClearedRelatives(int anchorPersonSk)
        {
            _dsCleared = _dsAllData.Copy();

            //Childeren (which we are interested in) are at zero level of the Data.  
            _dsCleared.Tables[0].DefaultView.RowFilter =
                "Screening_Determination='CLEARED' AND Anchor_Person_SK =" + anchorPersonSk;

            _dtCleared = _dsCleared.Tables[0].DefaultView.ToTable();

            _dtCleared.DefaultView.Sort = Properties.Settings.Default.ChildComboBoxSortOrder;

            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            foreach (DataRow drow in _dtCleared.Rows)
            {
                if (hTable.Contains(drow[0]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[0], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
            {
                _dtCleared.Rows.Remove(dRow);
            }
        }

        private void OrganiseClearedDataTable()
        {
            string nn = Properties.Settings.Default.ClearedRelativesColumnOrder;
            string[] narray = nn.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            _dtCleared.SetColumnsOrder(narray);

            dgvClearedRelatives.DataSource = _dtCleared;

            ////For presentation, sort the order of the Combo box.
            string n = Properties.Settings.Default.ClearedRelativesShowColumns;
            string[] array = n.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);

            dgvClearedRelatives.ShowColumn(array);

            dgvClearedRelatives.AutoResizeColumns();
            dgvClearedRelatives.RowHeadersVisible = false;


            if (dgvClearedRelatives.RowCount > 0)
            {
                lblIdentifiedRelativesWithClearences.Text =
                    Properties.Settings.Default.FormLabelRelativesWithClearences + @" (" + dgvClearedRelatives.RowCount +
                    @")";
            }
            else
            {
                lblIdentifiedRelativesWithClearences.Text =
                    Properties.Settings.Default.FormLabelRelativesWithClearences;
            }
        }


      //private void radMultiColumnComboBox1_EditorControl_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
      //{

      //    //if (FormInitailLoadFlag == false)
      //    //{
      //    //    GridViewDataRowInfo selectedRow = (GridViewDataRowInfo)radMultiColumnComboBox1.SelectedItem;

      //    //    // ResetForm();
      //    //    // ChildSlectedDisplayOnForm();
      //    //}

      //}

      //private void radMultiColumnComboBox1_EditorControl_Enter(object sender, EventArgs e)
      //{
      //   // ChildSlectedDisplayOnForm();
      // }

      private void radMultiColumnComboBox1_Enter(object sender, EventArgs e)
      {

         GridViewDataRowInfo selectedRow = (GridViewDataRowInfo)radMultiColumnComboBox1.SelectedItem;

         ChildSlectedDisplayOnForm(selectedRow);
      }

      private void radMultiColumnComboBox1_EditorControl_MouseDown(object sender, MouseEventArgs e)
        {
            GridCellElement cell =
               radMultiColumnComboBox1.EditorControl.ElementTree.GetElementAtPoint(e.Location) as GridCellElement;


          if (cell != null)  //ie dont do anything if we are scrolling
           {


         //  ChildSlectedDisplayOnForm();
         // if (cell.RowInfo.IsSelected)
         //{
         //    //you click an already selected row
         //}
         //else
         //{
         //     //you click a row that is not selected yet
            //GridViewDataRowInfo selectedRow = (GridViewDataRowInfo)radMultiColumnComboBox1.SelectedItem;

 
            GridViewDataRowInfo s = (GridViewDataRowInfo)cell.RowInfo;

            ChildSlectedDisplayOnForm(s);

         // }
          }
      }

      private void radMultiColumnComboBox1_EditorControl_Click(object sender, EventArgs e)
        {

        }


        private void ChildSlectedDisplayOnForm(GridViewDataRowInfo sR)
        {
         //the Telirik Multi Coluumn Combo Box takes a little work to obtin data from.
         GridViewDataRowInfo selectedRow = sR;// (GridViewDataRowInfo) radMultiColumnComboBox1.SelectedItem;

            //Protect the control when a new Child is selected.
            if (selectedRow is null)
            {
                return;
            }

            //Regardless of who we pick, identify the record via the Anchor_Person_SK.
            int anchorPersonSk = (int) selectedRow.Cells["Anchor_Person_SK"].Value;

            //This control allows the End User to see a little bit more about the Child they have selected.
            DisplayChildSelectedByEndUser(anchorPersonSk);

            //This control allows the End user to review all the Family Relationships which we are aware of currently.
            DisplayKnownFamilyRelationships(anchorPersonSk);

            IdentifyClearedRelatives(anchorPersonSk); //dtCleared

            OrganiseClearedDataTable();

            //Change of business rules may mean we do not care about 'ExpandCollapse' ?
            int associatedChildScreeningClearenceCount = (int) selectedRow.Cells["Relatives Cleared"].Value;
            if (associatedChildScreeningClearenceCount > 0)
            {
                //Only do this if the child has associated Screening.
                ExpandCollapse(true, Properties.Settings.Default.FamilyTreeControlLevelDepthToDisplay);
            }
            else
            {
                //Only do this if the child has associated Screening.
                // ExpandCollapse(false, Properties.Settings.Default.FamilyTreeControlLevelDepthToDisplay);
                OrderTreeView(Properties.Settings.Default.FamilyTreeControlLevelDepthToDisplay, false);
            }
        }

        private void DisplayChildSelectedByEndUser(int anchorPersonSk)
        {
            //Now that a Child has been selected, identify the full record to display to the End User.
            _dsChild.Tables[0].DefaultView.RowFilter = "Relationship_Level=0 AND Anchor_Person_SK =" + anchorPersonSk;
            _dtChild = _dsChild.Tables[0].DefaultView.ToTable();

            // Make the controls presentable to the End User. Sort the L->R order of the Columns then Hide what they dont need to know.
            string n = Properties.Settings.Default.ChildSelectedDataGridViewColumnOrder;
            string[] array = n.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            _dtChild.SetColumnsOrder(array);

            string dt = BusinessLayer.DfObjectToString(_dtChild.Rows[0]["DOB"]);
            DateTime oDate = DateTime.Parse(dt);

            int currentAge = BusinessLayer.ObtainAge(oDate);

            tbSelectedGender.Text = BusinessLayer.DfObjectToString(_dtChild.Rows[0]["Gender"])
                                    + @"; (DOB) " + BusinessLayer.DfObjectToString(oDate.ToString("dd/MM/yyyy")) +
                                    @"; (Age) " + currentAge;
        }

        private void DisplayKnownFamilyRelationships(int anchorPersonSk)
        {
            radTreeView1.DataSource = "";

            //Filter this control on the known related ID.
            _dsAllData.Tables[0].DefaultView.RowFilter = "Anchor_Person_SK =" + anchorPersonSk;
            _dtAllData = _dsAllData.Tables[0].DefaultView.ToTable();

            radTreeView1.DataSource = _dtAllData;

            radTreeView1.DisplayMember = "FullNameWithRelationship";

            radTreeView1.ParentMember = "Related_Person_SK_Tree_ID";
            radTreeView1.ChildMember = "Person_SK_Tree_ID";
            radTreeView1.ValueMember =
                "Person_SK_Tree_ID"; //"Person_SK"; //If this node is selected, what value do you want to return?
            radTreeView1.Tag = "Person_SK";

            //http://docs.telerik.com/devtools/winforms/treeview/working-with-nodes/custom-nodes
            radTreeView1.TreeViewElement.AutoSizeItems = true;

            radTreeView1.TreeViewElement.ItemHeight = 30;
        }

        //Only perform this on first load, not every time, or else the displays do not light up with user selection.
        private void OrderTreeView(int levelToExpandTo, bool showDuplicatedFamilyMembers)
        {
            // only need to do this once on load
            radTreeView1.CollapseAll();

            _tn = new RadTreeNodeController();

            //Note that AgeStart and AgeEnd are not acutally implemented yet. 
            List<ListOfImages> lom = new List<ListOfImages>
            {
                new ListOfImages
                {
                    Image = Properties.Resources.BabyMale,
                    ImageTag = "BabyMale",
                    AgeStart = 0,
                    AgeEnd = 2
                },
                new ListOfImages
                {
                    Image = Properties.Resources.BabyFemale,
                    ImageTag = "BabyFemale",
                    AgeStart = 0,
                    AgeEnd = 2
                },

                new ListOfImages
                {
                    Image = Properties.Resources.ChildMale,
                    ImageTag = "ChildMale",
                    AgeStart = 3,
                    AgeEnd = 10
                },
                new ListOfImages
                {
                    Image = Properties.Resources.ChildFemale,
                    ImageTag = "ChildFemale",
                    AgeStart = 3,
                    AgeEnd = 10
                },

                new ListOfImages
                {
                    Image = Properties.Resources.TeenagerMale,
                    ImageTag = "TeenagerMale",
                    AgeStart = 11,
                    AgeEnd = 17
                },
                new ListOfImages
                {
                    Image = Properties.Resources.TeenagerFemale,
                    ImageTag = "TeenagerFemale",
                    AgeStart = 11,
                    AgeEnd = 17
                },

                new ListOfImages
                {
                    Image = Properties.Resources.AdultMale,
                    ImageTag = "AdultMale",
                    AgeStart = 18,
                    AgeEnd = 64
                },
                new ListOfImages
                {
                    Image = Properties.Resources.AdultFemale,
                    ImageTag = "AdultFemale",
                    AgeStart = 18,
                    AgeEnd = 64
                },

                new ListOfImages
                {
                    Image = Properties.Resources.SeniorMale,
                    ImageTag = "SeniorMale",
                    AgeStart = 65,
                    AgeEnd = 400
                },
                new ListOfImages
                {
                    Image = Properties.Resources.SeniorFemale,
                    ImageTag = "SeniorFemale",
                    AgeStart = 65,
                    AgeEnd = 400
                },

                new ListOfImages
                {
                    Image = Properties.Resources.QuestionMark,
                    ImageTag = "Unknown",
                    AgeStart = 0,
                    AgeEnd = 1000
                }
            };
            //This class loops the Nodes, and defines how the Control will look.
            _tn.TreeNodeControllerDefinition(radTreeView1, levelToExpandTo, _fontSpecifications, lom, showDuplicatedFamilyMembers);
            
           //This will replace the -1 above.
           var t = Properties.Settings.Default.FamilyTreeControlMinimumAgeToDisplay;

        }

      private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            RadTreeNode selectedNode = radTreeView1.SelectedNode;

            //Place elsewhere
            dgvClearedRelatives.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvClearedRelatives.DefaultCellStyle.SelectionForeColor = Color.Black;

            //Protect the control when a new Child is selected.
            if (selectedNode is null)
            {
                return;
            }

            //set the Dataset and DataTable to the associated Record.
            int personSkTreeId = (int) selectedNode.Value;

            _dsAllData.Tables[0].DefaultView.RowFilter = "Person_SK_Tree_ID =" + personSkTreeId;

            _dtAllData = _dsAllData.Tables[0].DefaultView.ToTable();

            //DisplayRelationships(dtAllData);
            DisplayRelationships();

            //I do not believe this portion is adding value.
            HighlightAssociatedDataGridView(selectedNode);
        }


        //Suggestion: pass into this a dataTable, not the selectedNode.
        // private void DisplayRelationships(DataTable selectedNode)
        private void DisplayRelationships()
        {
            try
            {
                //TODO: A few of these sections were added as quick ideas. EACH OF these should have their own method.
                string screening = BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["Screening_Determination"])
                    .Replace("NULL", "NO DCSI CLEARANCE");

                //if (Screening == "") { Screening = "NO DCSI CLEARANCE"; };
                tb_screening_Determination.Text = screening;


                Icon secClearence;

                switch (screening)
                {
                    case "CLEARED":
                        secClearence =
                            new Icon("Resources/" +
                                     Properties.Settings.Default
                                         .ApplicationSecurityClearenceCleared); // Properties.Settings.Default.ApplicationSecurityClearenceCleared;
                        tb_screening_Determination.Font = new Font(tb_screening_Determination.Font, FontStyle.Regular);
                        break;
                    case "NO DCSI CLEARANCE":
                        secClearence =
                            new Icon("Resources/" + Properties.Settings.Default.ApplicationSecurityClearenceWarning);
                        tb_screening_Determination.Font = new Font(tb_screening_Determination.Font, FontStyle.Bold);
                        break;

                    default:
                        secClearence =
                            new Icon("Resources/" +
                                     Properties.Settings.Default
                                         .ApplicationSecurityClearenceCleared); // Properties.Settings.Default.ApplicationSecurityClearenceCleared;
                        tb_screening_Determination.Font = new Font(tb_screening_Determination.Font, FontStyle.Regular);
                        break;
                }

                pbSecurityClearences.Image = secClearence.ToBitmap();
                pbSecurityClearences.Refresh();
                tb_screening_Determination.Refresh(); //Id like the app to appear quick.  
                pbSecurityClearences.Visible = true;
                tb_screening_Determination.Visible = true;

                //Default the icon to a Male (or femal).
                Icon newIcon;
                if (BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["gender_Code"]) == "M")
                {
                    tb_gender_Code.Text = @"Male";
                    newIcon = new Icon("Resources/" + Properties.Settings.Default.ApplicationMaleGenderIcon);
                }
                else
                {
                    tb_gender_Code.Text = @"Female";
                    newIcon = new Icon("Resources/" + Properties.Settings.Default.ApplicationFemaleGenderIcon);
                }

                tb_family_Name.Text = BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["Family_Name"]) + @", " +
                                      BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["First_Given_Name"]) + @" " +
                                      BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["Second_Given_Name"]);

                //Not convinced that the Male / Female icon is the way to go.
                pbGender.Image = newIcon.ToBitmap();
                pbGender.Refresh();
                pbGender.Visible = false;

                int currentAge = BusinessLayer.ObtainAge(_dtAllData.Rows[0]["DOB"].ToString());

                tb_birth_Date.Text = BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["DOB"]).Substring(0, 10) +
                                     @"; (Age) " + currentAge;

                tb_relationship_Name.Text = BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["Relationship_Name"]);

                tb_address_Line.Text =BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["Address"]); //+ " " + BusinessLayer.dfObjectToString(dtAllData.Rows[0]["Suburb"]);

                tb_RelationshipTrace.Visible = true;
                //tb_AddressLineTwo.Text = BusinessLayer.dfObjectToString(dtAllData.Rows[0]["Address 2"]);

                string relationshipTrace = BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["Relationship_Name_Tree"]);
                if (relationshipTrace.Length == 0)
                {
                    relationshipTrace = "Selected Child";
                }
                else
                {
                    relationshipTrace =
                        relationshipTrace.Substring(relationshipTrace.Length - (relationshipTrace.Length - 1));
                    relationshipTrace = relationshipTrace.Replace(".", "'s ");
                }
                tb_RelationshipTrace.Text = relationshipTrace;

                tb_personal_Telephone.Text =
                    BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["Personal_Telephone"].ToString()
                        .Replace("NULL", ""));
                tb_business_Telephone.Text =
                    BusinessLayer.DfObjectToString(_dtAllData.Rows[0]["Business_Telephone"].ToString()
                        .Replace("NULL", ""));
                tb_personal_Mobile.Text =
                    BusinessLayer.DfObjectToString(
                        _dtAllData.Rows[0]["Personal_Mobile"].ToString().Replace("NULL", ""));
                tb_business_Mobile.Text =
                    BusinessLayer.DfObjectToString(
                        _dtAllData.Rows[0]["Business_Mobile"].ToString().Replace("NULL", ""));
            }

            catch (InvalidCastException)
            {
                //TODO: This is not really helpful when an error occurs. Smooth this over a little more.
                //throw ex;
            }
        }



        private void ClearRelationshipDetails()
        {
            tb_screening_Determination.Text = "";

            tb_family_Name.Text = "";

            tb_gender_Code.Text = "";

            tb_birth_Date.Text = "";
            tb_relationship_Name.Text = "";

            tb_address_Line.Text = "";
            tb_RelationshipTrace.Text = "";
            tb_personal_Telephone.Text = "";
            tb_business_Telephone.Text = "";
            tb_personal_Mobile.Text = "";
            tb_business_Mobile.Text = "";
        }

        public void AddMenuAndMenuItemsToForm()
        {
            MainMenu mnuFileMenu = new MainMenu();

            MenuItem myMenuItemFile = new MenuItem("&File");
            mnuFileMenu.MenuItems.Add(myMenuItemFile);

            MenuItem myMenuItemOpen = new MenuItem("&Open");
            myMenuItemFile.MenuItems.Add(myMenuItemOpen);

            myMenuItemFile.MenuItems.Add("-");

            MenuItem myMenuItemExit = new MenuItem("&Exit " + Properties.Settings.Default.ApplicationCurrentVersion);
            myMenuItemFile.MenuItems.Add(myMenuItemExit);

            myMenuItemOpen.Click += myMenuItemOpen_Click;

            myMenuItemExit.Click += myMenuItemExit_Click;

            Menu = mnuFileMenu;
        }

        private void myMenuItemExit_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(1);
            }
        }

        //http://www.telerik.com/support/kb/winforms/details/use-custom-filtering-to-search-in-radmulticolumncombobox
        private void EditorControl_CustomFiltering(object sender, GridViewCustomFilteringEventArgs e)
        {
            RadMultiColumnComboBoxElement element = radMultiColumnComboBox1.MultiColumnComboBoxElement;

            string textToSearch = radMultiColumnComboBox1.Text;
            // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
            if (AutoCompleteMode.Append == (AutoCompleteMode.Append & element.AutoCompleteMode))
            {
                if (element.SelectionLength > 0 && element.SelectionStart > 0)
                {
                    textToSearch = radMultiColumnComboBox1.Text.Substring(0, element.SelectionStart);
                }
            }
            if (string.IsNullOrEmpty(textToSearch))
            {
                e.Visible = true;

                for (int i = 0; i < element.EditorControl.ColumnCount; i++)
                {
                    e.Row.Cells[i].Style.Reset();
                }
                e.Row.InvalidateRow();
                return;
            }

            e.Visible = false;
            for (int i = 0; i < element.EditorControl.ColumnCount; i++)
            {
                string text = e.Row.Cells[i].Value.ToString();
                if (text.IndexOf(textToSearch, 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    e.Visible = true;
                    e.Row.Cells[i].Style.CustomizeFill = true;
                    e.Row.Cells[i].Style.DrawFill = true;
                    e.Row.Cells[i].Style.BackColor = Color.FromArgb(201, 252, 254);
                }
                else
                {
                    e.Row.Cells[i].Style.Reset();
                }
            }
            e.Row.InvalidateRow();
        }

        //http://www.telerik.com/support/kb/winforms/details/use-custom-filtering-to-search-in-radmulticolumncombobox
        private void radMultiColumnComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter) return;
                if (radMultiColumnComboBox1.ValueMember != "")
                {
                    radMultiColumnComboBox1.SelectedValue = radMultiColumnComboBox1.EditorControl.CurrentRow
                        .Cells[radMultiColumnComboBox1.ValueMember].Value;
                }
                else
                {
                    radMultiColumnComboBox1.SelectedValue = radMultiColumnComboBox1.EditorControl.CurrentRow
                        .Cells[radMultiColumnComboBox1.DisplayMember].Value;
                }

                radMultiColumnComboBox1.Text = radMultiColumnComboBox1.EditorControl.CurrentRow
                    .Cells[radMultiColumnComboBox1.DisplayMember].Value.ToString();
                radMultiColumnComboBox1.MultiColumnComboBoxElement.ClosePopup();
                radMultiColumnComboBox1.MultiColumnComboBoxElement.TextBoxElement.TextBoxItem.SelectAll();
            }
            catch (NullReferenceException)
            {
            }
        }

        private void ExpandCollapse(bool action, int expandToThisLevel)
        {
            if (radTreeView1.Nodes.Count == 0) return;
            if (action)
            {
                OrderTreeView(expandToThisLevel, true);
            }
            else
            {
                radTreeView1.CollapseAll();
            }
        }


        private void radMultiColumnComboBox1_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = Properties.Settings.Default.ChildComboBoxToolTip;
        }

        private void radTreeView1_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = Properties.Settings.Default.FamilyTreeControlToolTip;
        }

        private void cmdResetSearch_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            radTreeView1.Nodes.Clear();

            dgvClearedRelatives.DataSource = null;

            ClearRelationshipDetails();

            tbSelectedGender.Text = "";

            lblSelectChild.Text = Properties.Settings.Default.FormLabelSelectChild;
            lblSelectedRelation.Text = Properties.Settings.Default.FormLabelSelectedRelationships;
            lblRelationshipTree.Text = Properties.Settings.Default.FormLabelRelationshipTree;
            lblIdentifiedRelativesWithClearences.Text = Properties.Settings.Default.FormLabelRelativesWithClearences;


            pbSecurityClearences.Visible = false;
            tb_screening_Determination.Visible = false;

            radMultiColumnComboBox1.Focus();
            radMultiColumnComboBox1.EditorControl.SelectAll();
            SendKeys.Send("{BACKSPACE}");
        }


        private void HighlightAssociatedDataGridView(RadTreeNode selectedNode)
        {
            if (selectedNode == null) return;
            string fullName = BusinessLayer.DfObjectToString(selectedNode.Name);
            HighlightDataGridView(fullName);
        }

        private void HighlightDataGridView(string fullName)
        {
            int index = fullName.IndexOf("(", StringComparison.Ordinal);

            if (index > 0) fullName = fullName.Substring(0, index).Trim();

            var ds = dgvClearedRelatives.DataSource;
            dgvClearedRelatives.DataSource = null;
            dgvClearedRelatives.DataSource = ds;

            foreach (DataGridViewRow row in dgvClearedRelatives.Rows)
            {
                if ((string) row.Cells["FullName"].Value != fullName) continue;
                row.DefaultCellStyle.BackColor = _fontSpecifications.GetHighlightedBackColour;
                row.DefaultCellStyle.Font = _fontSpecifications.GetHighlightedFont;
                row.DefaultCellStyle.ForeColor = _fontSpecifications.GetHighlightedFontColour;
            }

            OrganiseClearedDataTable();
        }


        private void dgvClearedRelatives_Click(object sender, EventArgs e)
        {
            dgvClearedRelatives.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvClearedRelatives.DefaultCellStyle.SelectionForeColor = Color.Black;


            if (dgvClearedRelatives.CurrentCell == null) return;
            int i = dgvClearedRelatives.CurrentCell.RowIndex;
            if (i < 0) return;
            DataGridViewRow row = dgvClearedRelatives.Rows[i];
            LocateClearedIndividualThenDisplayRelationships(row);

            dgvClearedRelatives.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            dgvClearedRelatives.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void dgvClearedRelatives_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvClearedRelatives.Rows[e.RowIndex];
            LocateClearedIndividualThenDisplayRelationships(row);

            //I suggest we make the row higlighted... 
            dgvClearedRelatives.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            dgvClearedRelatives.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void LocateClearedIndividualThenDisplayRelationships(DataGridViewRow row)
        {
            //perform this search now, as we seem to loose access to the collumns quickly.
            int personSkTreeId = (int) row.Cells["Person_SK_Tree_ID"].Value;
            int personSk = (int) row.Cells["Person_SK"].Value;

            _dsAllData.Tables[0].DefaultView.RowFilter = "Person_SK_Tree_ID =" + personSkTreeId;

            _dtAllData = _dsAllData.Tables[0].DefaultView.ToTable();

            // DisplayRelationships(dtAllData);
            DisplayRelationships();

            string fullName = (string) row.Cells["FullName"].Value;
            HighlightDataGridView(fullName);

            //What font colour shoudl this be?
            _tn.SelectSpecificNodeInTree(personSk);
        }

        private void cbShowDuplication_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowDuplication.Checked)
            {
                // yes, I coud have used De Morgans Law, but I wanted to keep this simple. 
                OrderTreeView(Properties.Settings.Default.FamilyTreeControlLevelDepthToDisplay, false);
            }
            if (cbShowDuplication.Checked == false)
            {
                OrderTreeView(Properties.Settings.Default.FamilyTreeControlLevelDepthToDisplay, true);
            }
        }





   } //End Class


    //private void SelectDataGridView()
    //{
    //   int Person_SK_Tree_ID = (int)(row.Cells["Person_SK_Tree_ID"]).Value;

    //   dsAllData.Tables[0].DefaultView.RowFilter = "Person_SK_Tree_ID =" + Person_SK_Tree_ID;

    //   dtAllData = (dsAllData.Tables[0].DefaultView).ToTable();

    //   DisplayRelationships(dtAllData);


    //   //I do not believe this portion is adding value.
    //   HighlightAssociatedDataGridView(selectedNode);
    //}


    //private void DisplaySurrogateKeyInformation(GridViewDataRowInfo Row)

    //{

    //   gbTestDisplays.Visible = ApplicationTestingMode;

    //   if (ApplicationTestingMode == true)
    //   {

    //      if (Row == null)
    //      {
    //         tbAnchorPersonSK.Text = "";
    //         tbPerson_SK.Text = "";
    //         tbRelated_Person_SK.Text = "";
    //         tbRelatedPersonSKTree.Text = "";
    //         tbPersonSKTree.Text = "";
    //      }
    //      else
    //      {

    //         tbAnchorPersonSK.Text = BusinessLayer.dfObjectToString(Row.Cells["Anchor_Person_SK"].Value).Replace("NULL", "");

    //         tbPerson_SK.Text = BusinessLayer.dfObjectToString(Row.Cells["Person_SK"].Value).Replace("NULL", "");
    //         tbRelated_Person_SK.Text = BusinessLayer.dfObjectToString(Row.Cells["Related_Person_SK"].Value).Replace("NULL", "");

    //         tbPersonSKTree.Text = BusinessLayer.dfObjectToString(Row.Cells["Person_SK_Tree_ID"].Value).Replace("NULL", "");
    //         tbRelatedPersonSKTree.Text = BusinessLayer.dfObjectToString(Row.Cells["Related_Person_SK_Tree_ID"].Value).Replace("NULL", "");

    //      }
    //   }
    //}


    //private void lblBusinessGroup_DoubleClick(object sender, EventArgs e)
    //{
    // //  SetFormSize();
    //}

    //private void SetFormSize()
    //{
    //   //Onlhy do this if we have testing Mode enabled.
    //   if (ApplicationTestingMode == true)
    //   {
    //      int FormNormalWidth = Properties.Settings.Default.ApplicationFormNormalWidth;
    //      int FormNormalHeight = Properties.Settings.Default.ApplicationFormNormalHeight;

    //      int FormExtendedHeight = Properties.Settings.Default.ApplicationFormExtendedHeight;
    //      int FormExtendedWidth = Properties.Settings.Default.ApplicationFormExtendedWidth;

    //      if (this.Text == Properties.Settings.Default.ApplicationName)
    //      {
    //         this.Size = new Size(FormExtendedWidth, FormExtendedHeight);
    //         this.Text = "Dev Mode";
    //      }
    //      else
    //      {
    //         this.Size = new Size(FormNormalWidth, FormNormalHeight);
    //         this.Text = Properties.Settings.Default.ApplicationName;
    //         this.StartPosition = FormStartPosition.CenterScreen;
    //      }
    //   }

    //}
} //End Namespace