namespace SimpleFileExplorer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            listViewFiles = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            contextMenu = new ContextMenuStrip(components);
            copyPathToolStripMenuItem = new ToolStripMenuItem();
            setDirectoryPathToolStripMenuItem = new ToolStripMenuItem();
            fileOperationsToolStripMenuItem = new ToolStripMenuItem();
            renameToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            executeToolStripMenuItem = new ToolStripMenuItem();
            windowsExplorerToolStripMenuItem = new ToolStripMenuItem();
            windowsCMDToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            listViewSessionShortcuts = new ListView();
            contextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // listViewFiles
            // 
            listViewFiles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listViewFiles.BackColor = Color.Black;
            listViewFiles.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listViewFiles.ContextMenuStrip = contextMenu;
            listViewFiles.Font = new Font("Consolas", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listViewFiles.ForeColor = Color.Lime;
            listViewFiles.Location = new Point(9, 27);
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(759, 417);
            listViewFiles.TabIndex = 0;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.View = View.Details;
            listViewFiles.KeyDown += listViewFiles_KeyDown;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 500;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Type";
            columnHeader2.Width = 150;
            // 
            // contextMenu
            // 
            contextMenu.BackColor = Color.Black;
            contextMenu.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            contextMenu.Items.AddRange(new ToolStripItem[] { copyPathToolStripMenuItem, setDirectoryPathToolStripMenuItem, fileOperationsToolStripMenuItem, openToolStripMenuItem, executeToolStripMenuItem, windowsExplorerToolStripMenuItem, windowsCMDToolStripMenuItem });
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(215, 158);
            // 
            // copyPathToolStripMenuItem
            // 
            copyPathToolStripMenuItem.ForeColor = Color.Lime;
            copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
            copyPathToolStripMenuItem.Size = new Size(214, 22);
            copyPathToolStripMenuItem.Text = "Copy Path";
            copyPathToolStripMenuItem.Click += Copy_Path_Mode;
            // 
            // setDirectoryPathToolStripMenuItem
            // 
            setDirectoryPathToolStripMenuItem.ForeColor = Color.Lime;
            setDirectoryPathToolStripMenuItem.Name = "setDirectoryPathToolStripMenuItem";
            setDirectoryPathToolStripMenuItem.Size = new Size(214, 22);
            setDirectoryPathToolStripMenuItem.Text = "Set Directory (Path)";
            setDirectoryPathToolStripMenuItem.Click += GoToDir;
            // 
            // fileOperationsToolStripMenuItem
            // 
            fileOperationsToolStripMenuItem.BackColor = Color.Black;
            fileOperationsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { renameToolStripMenuItem, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, deleteToolStripMenuItem });
            fileOperationsToolStripMenuItem.ForeColor = Color.Lime;
            fileOperationsToolStripMenuItem.Name = "fileOperationsToolStripMenuItem";
            fileOperationsToolStripMenuItem.Size = new Size(214, 22);
            fileOperationsToolStripMenuItem.Text = "File Operations";
            // 
            // renameToolStripMenuItem
            // 
            renameToolStripMenuItem.BackColor = Color.Black;
            renameToolStripMenuItem.ForeColor = Color.Lime;
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Size = new Size(116, 22);
            renameToolStripMenuItem.Text = "Rename";
            renameToolStripMenuItem.Click += Rename_Mode;
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.BackColor = Color.Black;
            cutToolStripMenuItem.ForeColor = Color.Lime;
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(116, 22);
            cutToolStripMenuItem.Text = "Cut";
            cutToolStripMenuItem.Click += Cut_Mode;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.BackColor = Color.Black;
            copyToolStripMenuItem.ForeColor = Color.Lime;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(116, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += Copy_Mode;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.BackColor = Color.Black;
            pasteToolStripMenuItem.ForeColor = Color.Lime;
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(116, 22);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += Paste_Mode;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.BackColor = Color.Black;
            deleteToolStripMenuItem.ForeColor = Color.Lime;
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(116, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += Delete_Mode;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.ForeColor = Color.Lime;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(214, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += Open_Mode;
            // 
            // executeToolStripMenuItem
            // 
            executeToolStripMenuItem.ForeColor = Color.Lime;
            executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            executeToolStripMenuItem.Size = new Size(214, 22);
            executeToolStripMenuItem.Text = "Execute";
            executeToolStripMenuItem.Click += Execute_Mode;
            // 
            // windowsExplorerToolStripMenuItem
            // 
            windowsExplorerToolStripMenuItem.ForeColor = Color.Lime;
            windowsExplorerToolStripMenuItem.Name = "windowsExplorerToolStripMenuItem";
            windowsExplorerToolStripMenuItem.Size = new Size(214, 22);
            windowsExplorerToolStripMenuItem.Text = "Windows Explorer";
            windowsExplorerToolStripMenuItem.Click += Open_Windows_Explorer;
            // 
            // windowsCMDToolStripMenuItem
            // 
            windowsCMDToolStripMenuItem.ForeColor = Color.Lime;
            windowsCMDToolStripMenuItem.Name = "windowsCMDToolStripMenuItem";
            windowsCMDToolStripMenuItem.Size = new Size(214, 22);
            windowsCMDToolStripMenuItem.Text = "Windows CMD";
            windowsCMDToolStripMenuItem.Click += Open_CMD;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 1;
            label1.Text = "Copy Path";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(88, 9);
            label2.Name = "label2";
            label2.Size = new Size(35, 14);
            label2.TabIndex = 2;
            label2.Text = " ...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 447);
            label3.Name = "label3";
            label3.Size = new Size(28, 14);
            label3.TabIndex = 3;
            label3.Text = "...";
            // 
            // listViewSessionShortcuts
            // 
            listViewSessionShortcuts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewSessionShortcuts.BackColor = Color.Black;
            listViewSessionShortcuts.Font = new Font("Consolas", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            listViewSessionShortcuts.Location = new Point(9, 464);
            listViewSessionShortcuts.Name = "listViewSessionShortcuts";
            listViewSessionShortcuts.Size = new Size(759, 81);
            listViewSessionShortcuts.TabIndex = 4;
            listViewSessionShortcuts.UseCompatibleStateImageBehavior = false;
            listViewSessionShortcuts.View = View.List;
            listViewSessionShortcuts.KeyDown += listViewSessionShortcuts_KeyDown;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(780, 557);
            Controls.Add(listViewSessionShortcuts);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listViewFiles);
            ForeColor = Color.Lime;
            Name = "Form1";
            Text = "Form1";
            contextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listViewFiles;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Label label1;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem copyPathToolStripMenuItem;
        private ToolStripMenuItem executeToolStripMenuItem;
        private Label label2;
        private ToolStripMenuItem setDirectoryPathToolStripMenuItem;
        private ToolStripMenuItem fileOperationsToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Label label3;
        private ToolStripMenuItem windowsExplorerToolStripMenuItem;
        private ToolStripMenuItem windowsCMDToolStripMenuItem;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ListView listViewSessionShortcuts;
    }
}
