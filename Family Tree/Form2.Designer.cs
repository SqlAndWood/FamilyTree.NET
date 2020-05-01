namespace FamilyTree
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
         System.Windows.Forms.Label lblRelationshipTrace;
         System.Windows.Forms.Label relationship_NameLabel;
         System.Windows.Forms.Label birth_Date_EncryptedLabel;
         System.Windows.Forms.Label gender_CodeLabel;
         System.Windows.Forms.Label business_Telephone_EncryptedLabel;
         System.Windows.Forms.Label personal_Telephone_EncryptedLabel;
         System.Windows.Forms.Label family_Name_EncryptedLabel;
         Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
         this.radTreeView1 = new Telerik.WinControls.UI.RadTreeView();
         this.lblRelationshipTree = new System.Windows.Forms.Label();
         this.lblSelectChild = new System.Windows.Forms.Label();
         this.radMultiColumnComboBox1 = new Telerik.WinControls.UI.RadMultiColumnComboBox();
         this.lblBusinessGroup = new System.Windows.Forms.Label();
         this.lblDepartment = new System.Windows.Forms.Label();
         this.cmdResetSearch = new System.Windows.Forms.Button();
         this.dgvClearedRelatives = new System.Windows.Forms.DataGridView();
         this.lblIdentifiedRelativesWithClearences = new System.Windows.Forms.Label();
         this.tbSelectedGender = new System.Windows.Forms.TextBox();
         this.panel1 = new System.Windows.Forms.Panel();
         this.pbSecurityClearences = new System.Windows.Forms.PictureBox();
         this.pbGender = new System.Windows.Forms.PictureBox();
         this.tb_RelationshipTrace = new System.Windows.Forms.TextBox();
         this.tb_address_Line = new System.Windows.Forms.TextBox();
         this.tb_screening_Determination = new System.Windows.Forms.TextBox();
         this.tb_relationship_Name = new System.Windows.Forms.TextBox();
         this.tb_birth_Date = new System.Windows.Forms.TextBox();
         this.tb_gender_Code = new System.Windows.Forms.TextBox();
         this.tb_family_Name = new System.Windows.Forms.TextBox();
         this.lblSelectedRelation = new System.Windows.Forms.Label();
         this.tb_business_Mobile = new System.Windows.Forms.TextBox();
         this.tb_personal_Mobile = new System.Windows.Forms.TextBox();
         this.tb_business_Telephone = new System.Windows.Forms.TextBox();
         this.tb_personal_Telephone = new System.Windows.Forms.TextBox();
         this.cbShowDuplication = new System.Windows.Forms.CheckBox();
         lblRelationshipTrace = new System.Windows.Forms.Label();
         relationship_NameLabel = new System.Windows.Forms.Label();
         birth_Date_EncryptedLabel = new System.Windows.Forms.Label();
         gender_CodeLabel = new System.Windows.Forms.Label();
         business_Telephone_EncryptedLabel = new System.Windows.Forms.Label();
         personal_Telephone_EncryptedLabel = new System.Windows.Forms.Label();
         family_Name_EncryptedLabel = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1.EditorControl)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1.EditorControl.MasterTemplate)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dgvClearedRelatives)).BeginInit();
         this.panel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pbSecurityClearences)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pbGender)).BeginInit();
         this.SuspendLayout();
         // 
         // lblRelationshipTrace
         // 
         lblRelationshipTrace.AutoSize = true;
         lblRelationshipTrace.Location = new System.Drawing.Point(125, 177);
         lblRelationshipTrace.Name = "lblRelationshipTrace";
         lblRelationshipTrace.Size = new System.Drawing.Size(99, 13);
         lblRelationshipTrace.TabIndex = 91;
         lblRelationshipTrace.Text = "Relationship Trace:";
         // 
         // relationship_NameLabel
         // 
         relationship_NameLabel.AutoSize = true;
         relationship_NameLabel.Location = new System.Drawing.Point(156, 152);
         relationship_NameLabel.Name = "relationship_NameLabel";
         relationship_NameLabel.Size = new System.Drawing.Size(68, 13);
         relationship_NameLabel.TabIndex = 85;
         relationship_NameLabel.Text = "Relationship:";
         // 
         // birth_Date_EncryptedLabel
         // 
         birth_Date_EncryptedLabel.AutoSize = true;
         birth_Date_EncryptedLabel.Location = new System.Drawing.Point(139, 127);
         birth_Date_EncryptedLabel.Name = "birth_Date_EncryptedLabel";
         birth_Date_EncryptedLabel.Size = new System.Drawing.Size(85, 13);
         birth_Date_EncryptedLabel.TabIndex = 83;
         birth_Date_EncryptedLabel.Text = "Birth Date (Age):";
         // 
         // gender_CodeLabel
         // 
         gender_CodeLabel.AutoSize = true;
         gender_CodeLabel.Location = new System.Drawing.Point(179, 102);
         gender_CodeLabel.Name = "gender_CodeLabel";
         gender_CodeLabel.Size = new System.Drawing.Size(45, 13);
         gender_CodeLabel.TabIndex = 81;
         gender_CodeLabel.Text = "Gender:";
         // 
         // business_Telephone_EncryptedLabel
         // 
         business_Telephone_EncryptedLabel.AutoSize = true;
         business_Telephone_EncryptedLabel.Location = new System.Drawing.Point(303, 245);
         business_Telephone_EncryptedLabel.Name = "business_Telephone_EncryptedLabel";
         business_Telephone_EncryptedLabel.Size = new System.Drawing.Size(131, 13);
         business_Telephone_EncryptedLabel.TabIndex = 74;
         business_Telephone_EncryptedLabel.Text = "Business Phone Numbers:";
         // 
         // personal_Telephone_EncryptedLabel
         // 
         personal_Telephone_EncryptedLabel.AutoSize = true;
         personal_Telephone_EncryptedLabel.Location = new System.Drawing.Point(60, 245);
         personal_Telephone_EncryptedLabel.Name = "personal_Telephone_EncryptedLabel";
         personal_Telephone_EncryptedLabel.Size = new System.Drawing.Size(130, 13);
         personal_Telephone_EncryptedLabel.TabIndex = 72;
         personal_Telephone_EncryptedLabel.Text = "Personal Phone Numbers:";
         // 
         // family_Name_EncryptedLabel
         // 
         family_Name_EncryptedLabel.AutoSize = true;
         family_Name_EncryptedLabel.Location = new System.Drawing.Point(167, 77);
         family_Name_EncryptedLabel.Name = "family_Name_EncryptedLabel";
         family_Name_EncryptedLabel.Size = new System.Drawing.Size(57, 13);
         family_Name_EncryptedLabel.TabIndex = 79;
         family_Name_EncryptedLabel.Text = "Full Name:";
         // 
         // radTreeView1
         // 
         this.radTreeView1.DisplayMember = " ";
         this.radTreeView1.HideSelection = false;
         this.radTreeView1.Location = new System.Drawing.Point(669, 29);
         this.radTreeView1.Name = "radTreeView1";
         this.radTreeView1.Size = new System.Drawing.Size(399, 708);
         this.radTreeView1.SpacingBetweenNodes = -1;
         this.radTreeView1.TabIndex = 0;
         this.radTreeView1.Text = "radTreeView1";
         this.radTreeView1.SelectedNodeChanged += new Telerik.WinControls.UI.RadTreeView.RadTreeViewEventHandler(this.radTreeView1_SelectedNodeChanged);
         this.radTreeView1.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.radTreeView1_ToolTipTextNeeded);
         // 
         // lblRelationshipTree
         // 
         this.lblRelationshipTree.AutoSize = true;
         this.lblRelationshipTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblRelationshipTree.Location = new System.Drawing.Point(666, 6);
         this.lblRelationshipTree.Name = "lblRelationshipTree";
         this.lblRelationshipTree.Size = new System.Drawing.Size(190, 13);
         this.lblRelationshipTree.TabIndex = 10;
         this.lblRelationshipTree.Text = "Identified Relationship Structure";
         // 
         // lblSelectChild
         // 
         this.lblSelectChild.AutoSize = true;
         this.lblSelectChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblSelectChild.Location = new System.Drawing.Point(15, 19);
         this.lblSelectChild.Name = "lblSelectChild";
         this.lblSelectChild.Size = new System.Drawing.Size(97, 13);
         this.lblSelectChild.TabIndex = 11;
         this.lblSelectChild.Text = "Search / Select";
         // 
         // radMultiColumnComboBox1
         // 
         this.radMultiColumnComboBox1.AutoSize = true;
         this.radMultiColumnComboBox1.AutoSizeDropDownToBestFit = true;
         // 
         // radMultiColumnComboBox1.NestedRadGridView
         // 
         this.radMultiColumnComboBox1.EditorControl.BackColor = System.Drawing.SystemColors.Window;
         this.radMultiColumnComboBox1.EditorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.radMultiColumnComboBox1.EditorControl.ForeColor = System.Drawing.SystemColors.ControlText;
         this.radMultiColumnComboBox1.EditorControl.Location = new System.Drawing.Point(0, 0);
         // 
         // 
         // 
         this.radMultiColumnComboBox1.EditorControl.MasterTemplate.AllowAddNewRow = false;
         this.radMultiColumnComboBox1.EditorControl.MasterTemplate.AllowCellContextMenu = false;
         this.radMultiColumnComboBox1.EditorControl.MasterTemplate.AllowColumnChooser = false;
         this.radMultiColumnComboBox1.EditorControl.MasterTemplate.EnableGrouping = false;
         this.radMultiColumnComboBox1.EditorControl.MasterTemplate.ShowFilteringRow = false;
         this.radMultiColumnComboBox1.EditorControl.MasterTemplate.ViewDefinition = tableViewDefinition1;
         this.radMultiColumnComboBox1.EditorControl.Name = "NestedRadGridView";
         this.radMultiColumnComboBox1.EditorControl.ReadOnly = true;
         this.radMultiColumnComboBox1.EditorControl.ShowGroupPanel = false;
         this.radMultiColumnComboBox1.EditorControl.Size = new System.Drawing.Size(240, 150);
         this.radMultiColumnComboBox1.EditorControl.TabIndex = 0;
         this.radMultiColumnComboBox1.EditorControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.radMultiColumnComboBox1_EditorControl_MouseDown);
         this.radMultiColumnComboBox1.Location = new System.Drawing.Point(15, 35);
         this.radMultiColumnComboBox1.Name = "radMultiColumnComboBox1";
         this.radMultiColumnComboBox1.Size = new System.Drawing.Size(544, 20);
         this.radMultiColumnComboBox1.TabIndex = 14;
         this.radMultiColumnComboBox1.TabStop = false;
         this.radMultiColumnComboBox1.SelectedValueChanged += new System.EventHandler(this.radMultiColumnComboBox1_EditorControl_Click);
         this.radMultiColumnComboBox1.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.radMultiColumnComboBox1_ToolTipTextNeeded);
         this.radMultiColumnComboBox1.Enter += new System.EventHandler(this.radMultiColumnComboBox1_Enter);
         this.radMultiColumnComboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radMultiColumnComboBox1_KeyDown);
         // 
         // lblBusinessGroup
         // 
         this.lblBusinessGroup.AutoSize = true;
         this.lblBusinessGroup.Location = new System.Drawing.Point(81, 724);
         this.lblBusinessGroup.Name = "lblBusinessGroup";
         this.lblBusinessGroup.Size = new System.Drawing.Size(121, 13);
         this.lblBusinessGroup.TabIndex = 15;
         this.lblBusinessGroup.Text = "Office for Data Analytics";
         // 
         // lblDepartment
         // 
         this.lblDepartment.AutoSize = true;
         this.lblDepartment.Location = new System.Drawing.Point(12, 711);
         this.lblDepartment.Name = "lblDepartment";
         this.lblDepartment.Size = new System.Drawing.Size(190, 13);
         this.lblDepartment.TabIndex = 19;
         this.lblDepartment.Text = "Department of the Premier and Cabinet";
         // 
         // cmdResetSearch
         // 
         this.cmdResetSearch.Location = new System.Drawing.Point(573, 35);
         this.cmdResetSearch.Name = "cmdResetSearch";
         this.cmdResetSearch.Size = new System.Drawing.Size(90, 23);
         this.cmdResetSearch.TabIndex = 21;
         this.cmdResetSearch.Text = "Reset Search";
         this.cmdResetSearch.UseVisualStyleBackColor = true;
         this.cmdResetSearch.Click += new System.EventHandler(this.cmdResetSearch_Click);
         // 
         // dgvClearedRelatives
         // 
         this.dgvClearedRelatives.AllowUserToAddRows = false;
         this.dgvClearedRelatives.AllowUserToDeleteRows = false;
         this.dgvClearedRelatives.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         this.dgvClearedRelatives.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dgvClearedRelatives.Location = new System.Drawing.Point(18, 109);
         this.dgvClearedRelatives.Name = "dgvClearedRelatives";
         this.dgvClearedRelatives.ReadOnly = true;
         this.dgvClearedRelatives.Size = new System.Drawing.Size(648, 208);
         this.dgvClearedRelatives.TabIndex = 22;
         this.dgvClearedRelatives.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClearedRelatives_CellContentClick);
         this.dgvClearedRelatives.Click += new System.EventHandler(this.dgvClearedRelatives_Click);
         // 
         // lblIdentifiedRelativesWithClearences
         // 
         this.lblIdentifiedRelativesWithClearences.AutoSize = true;
         this.lblIdentifiedRelativesWithClearences.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblIdentifiedRelativesWithClearences.Location = new System.Drawing.Point(17, 93);
         this.lblIdentifiedRelativesWithClearences.Name = "lblIdentifiedRelativesWithClearences";
         this.lblIdentifiedRelativesWithClearences.Size = new System.Drawing.Size(211, 13);
         this.lblIdentifiedRelativesWithClearences.TabIndex = 23;
         this.lblIdentifiedRelativesWithClearences.Text = "Identified Relatives with Clearances";
         // 
         // tbSelectedGender
         // 
         this.tbSelectedGender.BackColor = System.Drawing.SystemColors.ControlLightLight;
         this.tbSelectedGender.Enabled = false;
         this.tbSelectedGender.Location = new System.Drawing.Point(15, 60);
         this.tbSelectedGender.Name = "tbSelectedGender";
         this.tbSelectedGender.ReadOnly = true;
         this.tbSelectedGender.Size = new System.Drawing.Size(390, 20);
         this.tbSelectedGender.TabIndex = 28;
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.pbSecurityClearences);
         this.panel1.Controls.Add(lblRelationshipTrace);
         this.panel1.Controls.Add(this.pbGender);
         this.panel1.Controls.Add(this.tb_RelationshipTrace);
         this.panel1.Controls.Add(this.tb_address_Line);
         this.panel1.Controls.Add(this.tb_screening_Determination);
         this.panel1.Controls.Add(relationship_NameLabel);
         this.panel1.Controls.Add(this.tb_relationship_Name);
         this.panel1.Controls.Add(birth_Date_EncryptedLabel);
         this.panel1.Controls.Add(this.tb_birth_Date);
         this.panel1.Controls.Add(gender_CodeLabel);
         this.panel1.Controls.Add(this.tb_gender_Code);
         this.panel1.Controls.Add(family_Name_EncryptedLabel);
         this.panel1.Controls.Add(this.tb_family_Name);
         this.panel1.Controls.Add(this.lblSelectedRelation);
         this.panel1.Controls.Add(this.tb_business_Mobile);
         this.panel1.Controls.Add(this.tb_personal_Mobile);
         this.panel1.Controls.Add(business_Telephone_EncryptedLabel);
         this.panel1.Controls.Add(this.tb_business_Telephone);
         this.panel1.Controls.Add(personal_Telephone_EncryptedLabel);
         this.panel1.Controls.Add(this.tb_personal_Telephone);
         this.panel1.Location = new System.Drawing.Point(108, 340);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(555, 326);
         this.panel1.TabIndex = 72;
         // 
         // pbSecurityClearences
         // 
         this.pbSecurityClearences.Location = new System.Drawing.Point(420, 44);
         this.pbSecurityClearences.Name = "pbSecurityClearences";
         this.pbSecurityClearences.Size = new System.Drawing.Size(15, 20);
         this.pbSecurityClearences.TabIndex = 92;
         this.pbSecurityClearences.TabStop = false;
         // 
         // pbGender
         // 
         this.pbGender.Location = new System.Drawing.Point(113, 73);
         this.pbGender.Name = "pbGender";
         this.pbGender.Size = new System.Drawing.Size(48, 41);
         this.pbGender.TabIndex = 90;
         this.pbGender.TabStop = false;
         // 
         // tb_RelationshipTrace
         // 
         this.tb_RelationshipTrace.Location = new System.Drawing.Point(233, 173);
         this.tb_RelationshipTrace.Name = "tb_RelationshipTrace";
         this.tb_RelationshipTrace.ReadOnly = true;
         this.tb_RelationshipTrace.Size = new System.Drawing.Size(234, 20);
         this.tb_RelationshipTrace.TabIndex = 89;
         // 
         // tb_address_Line
         // 
         this.tb_address_Line.Location = new System.Drawing.Point(128, 198);
         this.tb_address_Line.Name = "tb_address_Line";
         this.tb_address_Line.ReadOnly = true;
         this.tb_address_Line.Size = new System.Drawing.Size(339, 20);
         this.tb_address_Line.TabIndex = 88;
         this.tb_address_Line.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // tb_screening_Determination
         // 
         this.tb_screening_Determination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tb_screening_Determination.Location = new System.Drawing.Point(234, 47);
         this.tb_screening_Determination.Name = "tb_screening_Determination";
         this.tb_screening_Determination.ReadOnly = true;
         this.tb_screening_Determination.Size = new System.Drawing.Size(180, 20);
         this.tb_screening_Determination.TabIndex = 87;
         this.tb_screening_Determination.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // tb_relationship_Name
         // 
         this.tb_relationship_Name.Location = new System.Drawing.Point(233, 148);
         this.tb_relationship_Name.Name = "tb_relationship_Name";
         this.tb_relationship_Name.ReadOnly = true;
         this.tb_relationship_Name.Size = new System.Drawing.Size(234, 20);
         this.tb_relationship_Name.TabIndex = 86;
         // 
         // tb_birth_Date
         // 
         this.tb_birth_Date.Location = new System.Drawing.Point(233, 123);
         this.tb_birth_Date.Name = "tb_birth_Date";
         this.tb_birth_Date.ReadOnly = true;
         this.tb_birth_Date.Size = new System.Drawing.Size(234, 20);
         this.tb_birth_Date.TabIndex = 84;
         // 
         // tb_gender_Code
         // 
         this.tb_gender_Code.Location = new System.Drawing.Point(233, 98);
         this.tb_gender_Code.Name = "tb_gender_Code";
         this.tb_gender_Code.ReadOnly = true;
         this.tb_gender_Code.Size = new System.Drawing.Size(234, 20);
         this.tb_gender_Code.TabIndex = 82;
         // 
         // tb_family_Name
         // 
         this.tb_family_Name.Location = new System.Drawing.Point(233, 73);
         this.tb_family_Name.Name = "tb_family_Name";
         this.tb_family_Name.ReadOnly = true;
         this.tb_family_Name.Size = new System.Drawing.Size(234, 20);
         this.tb_family_Name.TabIndex = 80;
         // 
         // lblSelectedRelation
         // 
         this.lblSelectedRelation.AutoSize = true;
         this.lblSelectedRelation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblSelectedRelation.Location = new System.Drawing.Point(258, 18);
         this.lblSelectedRelation.Name = "lblSelectedRelation";
         this.lblSelectedRelation.Size = new System.Drawing.Size(174, 13);
         this.lblSelectedRelation.TabIndex = 78;
         this.lblSelectedRelation.Text = "Selected Relationship Details";
         // 
         // tb_business_Mobile
         // 
         this.tb_business_Mobile.Location = new System.Drawing.Point(306, 288);
         this.tb_business_Mobile.Name = "tb_business_Mobile";
         this.tb_business_Mobile.ReadOnly = true;
         this.tb_business_Mobile.Size = new System.Drawing.Size(189, 20);
         this.tb_business_Mobile.TabIndex = 77;
         // 
         // tb_personal_Mobile
         // 
         this.tb_personal_Mobile.Location = new System.Drawing.Point(63, 287);
         this.tb_personal_Mobile.Name = "tb_personal_Mobile";
         this.tb_personal_Mobile.ReadOnly = true;
         this.tb_personal_Mobile.Size = new System.Drawing.Size(189, 20);
         this.tb_personal_Mobile.TabIndex = 76;
         // 
         // tb_business_Telephone
         // 
         this.tb_business_Telephone.Location = new System.Drawing.Point(306, 261);
         this.tb_business_Telephone.Name = "tb_business_Telephone";
         this.tb_business_Telephone.ReadOnly = true;
         this.tb_business_Telephone.Size = new System.Drawing.Size(189, 20);
         this.tb_business_Telephone.TabIndex = 75;
         // 
         // tb_personal_Telephone
         // 
         this.tb_personal_Telephone.Location = new System.Drawing.Point(63, 261);
         this.tb_personal_Telephone.Name = "tb_personal_Telephone";
         this.tb_personal_Telephone.ReadOnly = true;
         this.tb_personal_Telephone.Size = new System.Drawing.Size(189, 20);
         this.tb_personal_Telephone.TabIndex = 73;
         // 
         // cbShowDuplication
         // 
         this.cbShowDuplication.AutoSize = true;
         this.cbShowDuplication.Location = new System.Drawing.Point(883, 6);
         this.cbShowDuplication.Name = "cbShowDuplication";
         this.cbShowDuplication.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
         this.cbShowDuplication.Size = new System.Drawing.Size(185, 17);
         this.cbShowDuplication.TabIndex = 73;
         this.cbShowDuplication.Text = "Show Duplicated Family Members";
         this.cbShowDuplication.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.cbShowDuplication.UseVisualStyleBackColor = true;
         this.cbShowDuplication.CheckedChanged += new System.EventHandler(this.cbShowDuplication_CheckedChanged);
         // 
         // Form2
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1075, 754);
         this.Controls.Add(this.cbShowDuplication);
         this.Controls.Add(this.panel1);
         this.Controls.Add(this.tbSelectedGender);
         this.Controls.Add(this.lblIdentifiedRelativesWithClearences);
         this.Controls.Add(this.dgvClearedRelatives);
         this.Controls.Add(this.cmdResetSearch);
         this.Controls.Add(this.lblDepartment);
         this.Controls.Add(this.lblBusinessGroup);
         this.Controls.Add(this.radMultiColumnComboBox1);
         this.Controls.Add(this.lblSelectChild);
         this.Controls.Add(this.lblRelationshipTree);
         this.Controls.Add(this.radTreeView1);
         this.Name = "Form2";
         this.Text = "Family Tree";
         this.Load += new System.EventHandler(this.Form2_Load);
         ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1.EditorControl.MasterTemplate)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1.EditorControl)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.radMultiColumnComboBox1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dgvClearedRelatives)).EndInit();
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pbSecurityClearences)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pbGender)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTreeView radTreeView1;
        private System.Windows.Forms.Label lblRelationshipTree;
        private System.Windows.Forms.Label lblSelectChild;
        private Telerik.WinControls.UI.RadMultiColumnComboBox radMultiColumnComboBox1;
        private System.Windows.Forms.Label lblBusinessGroup;
      private System.Windows.Forms.Label lblDepartment;
      private System.Windows.Forms.Button cmdResetSearch;
      private System.Windows.Forms.DataGridView dgvClearedRelatives;
      private System.Windows.Forms.Label lblIdentifiedRelativesWithClearences;
      private System.Windows.Forms.TextBox tbSelectedGender;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.PictureBox pbSecurityClearences;
      private System.Windows.Forms.PictureBox pbGender;
      private System.Windows.Forms.TextBox tb_RelationshipTrace;
      private System.Windows.Forms.TextBox tb_address_Line;
      private System.Windows.Forms.TextBox tb_screening_Determination;
      private System.Windows.Forms.TextBox tb_relationship_Name;
      private System.Windows.Forms.TextBox tb_birth_Date;
      private System.Windows.Forms.TextBox tb_gender_Code;
      private System.Windows.Forms.Label lblSelectedRelation;
      private System.Windows.Forms.TextBox tb_business_Mobile;
      private System.Windows.Forms.TextBox tb_personal_Mobile;
      private System.Windows.Forms.TextBox tb_business_Telephone;
      private System.Windows.Forms.TextBox tb_personal_Telephone;
      private System.Windows.Forms.TextBox tb_family_Name;
      private System.Windows.Forms.CheckBox cbShowDuplication;
   }
}