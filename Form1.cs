using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Specialized; // Necessário para StringCollection
using Microsoft.VisualBasic;
using System.Diagnostics; // Para Process.Start

namespace SimpleFileExplorer {
    public partial class Form1 : Form
    {
        private string parent_dir;
        private string current_dir;
        private StringCollection sel_files;
        private StringCollection del_files;
		private bool isForMove;
        public Form1()
        {
            InitializeComponent();
            parent_dir = "C:\\";
            current_dir = "C:\\";
            sel_files = new StringCollection();
            del_files = new StringCollection();
            LoadDirectory("C:\\"); // Inicia no diretório raiz (ajuste conforme necessário)
            listViewFiles.Focus();
            //this.BackColor = System.Drawing.Color.Black; // Cor que será transparente
            //this.TransparencyKey = this.BackColor; // Define a cor transparente
            this.Opacity = 0.8;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.TopMost = true;
        }
        private void LoadDirectory(string path)
        {
            try
            {
                listViewFiles.Items.Clear(); // Limpa o ListView

                // Adiciona opção de voltar ao diretório pai
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (dirInfo.Parent != null)
                {
                    ListViewItem parentItem = new ListViewItem("..");
                    parentItem.SubItems.Add("Directory");
                    parentItem.Tag = dirInfo.Parent.FullName; // Armazena o caminho
                    parentItem.ForeColor = System.Drawing.Color.Lime; // Verde para o diretório pai
                    parent_dir = dirInfo.Parent.FullName;
                    listViewFiles.Items.Add(parentItem);
                }

                // Lista os diretórios
                foreach (var dir in Directory.GetDirectories(path))
                {
                    DirectoryInfo directory = new DirectoryInfo(dir);
                    ListViewItem item = new ListViewItem(directory.Name);
                    item.SubItems.Add("Directory");
                    item.Tag = directory.FullName; // Armazena o caminho completo
                    item.ForeColor = System.Drawing.Color.Lime; // Verde para diretórios

                    // >>
                    // Destaca se estiver em sel_files ou del_files
                    if (del_files.Contains(directory.FullName))
                    {
                        item.ForeColor = System.Drawing.Color.Magenta;
                        item.BackColor = System.Drawing.Color.DarkRed;
                    }
                    else if (sel_files.Contains(directory.FullName))
                    {
                        item.ForeColor = System.Drawing.Color.Yellow;
                        item.BackColor = System.Drawing.Color.DarkBlue;
                    }
                    // <<

                    listViewFiles.Items.Add(item);
                }

                // Lista os arquivos
                foreach (var file in Directory.GetFiles(path))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    ListViewItem item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add("File");
                    item.Tag = fileInfo.FullName; // Armazena o caminho completo

                    // Cores para Diferentes Tipos de Arquivo 
                    if (fileInfo.Extension.ToLower() == ".exe")
                    {
                        item.ForeColor = System.Drawing.Color.Yellow;
                    }
                    else if (fileInfo.Extension.ToLower() == ".txt")
                    {
                        item.ForeColor = System.Drawing.Color.Magenta;
                    }
                    else if (fileInfo.Extension.ToLower() == ".pdf")
                    {
                        item.ForeColor = System.Drawing.Color.Magenta;
                    }
                    else if (fileInfo.Extension.ToLower() == ".ini")
                    {
                        item.ForeColor = System.Drawing.Color.Cyan;
                    }
                    else
                    {
                        item.ForeColor = System.Drawing.Color.White;
                    }

                    // Adicione estas linhas para destacar arquivos em del_files

                    if (del_files.Contains(fileInfo.FullName))
                    {
                        item.ForeColor = System.Drawing.Color.Magenta;
                        item.BackColor = System.Drawing.Color.DarkRed;
                    }
                    else if (sel_files.Contains(fileInfo.FullName))
                    {
                        item.ForeColor = System.Drawing.Color.Yellow;
                        item.BackColor = System.Drawing.Color.DarkBlue;
                    }

                    listViewFiles.Items.Add(item);
                }

                this.Text = "Simple File Explorer - " + path; // Atualiza o título da janela
                current_dir = path;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access Denied.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void listViewFiles_KeyDown(object sender, KeyEventArgs e)
        {
            // Movimenta a janela com Alt + Setas
            if (e.Alt) // Verifica se Alt está pressionado
            {
                int step = 10; // Incremento de movimento em pixels
                switch (e.KeyCode)
                {
                    case Keys.Up: // Alt + Seta para cima
                        this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y - step);
                        return;
                    case Keys.Down: // Alt + Seta para baixo
                        this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + step);
                        return;
                    case Keys.Left: // Alt + Seta para esquerda
                        this.Location = new System.Drawing.Point(this.Location.X - step, this.Location.Y);
                        return;
                    case Keys.Right: // Alt + Seta para direita
                        this.Location = new System.Drawing.Point(this.Location.X + step, this.Location.Y);
                        return;
                }
            }
            // -- 
            if (e.KeyCode == Keys.Right) // Entra no diretório 
            {
                if (label1.Text == "Delete") return;
                label1.Text = "Copy Path";
                label1.ForeColor = System.Drawing.Color.Lime; // Verde para diretórios
                if (listViewFiles.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = listViewFiles.SelectedItems[0];
                    string path = selectedItem.Tag.ToString();

                    // Verifica se é um diretório
                    if (selectedItem.SubItems[1].Text == "Directory")
                    {
                        LoadDirectory(path); // Navega para o diretório
                    }
                }
                return;
            }
            if (e.KeyCode == Keys.Left)
            {
                if (label1.Text == "Delete") return;
                label1.Text = "Copy Path";
				label1.ForeColor = System.Drawing.Color.Lime; // Verde para diretórios
                if (listViewFiles.SelectedItems.Count > 0)
                {
                    LoadDirectory(parent_dir);
                }
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (sel_files.Count > 0 || del_files.Count > 0)
                {
                    sel_files.Clear();
                    del_files.Clear();
                    if (label1.Text == "Copy" || label1.Text == "Paste" || label1.Text == "Delete")
                    {
                        Clipboard.Clear();
                        label2.Text = "...";
                    }
                    LoadDirectory(current_dir);
                }
                else
                {
                    this.Close();
                }
            }
            // Abre o menu de contexto com Espaço
            if (e.KeyCode == Keys.Space)
            {
                if (listViewFiles.ContextMenuStrip != null)
                {
                    // Se houver um item selecionado, abre o menu na posição do item
                    if (listViewFiles.SelectedItems.Count > 0)
                    {
                        ListViewItem selectedItem = listViewFiles.SelectedItems[0];
                        Rectangle itemBounds = listViewFiles.GetItemRect(selectedItem.Index);
                        listViewFiles.ContextMenuStrip.Show(listViewFiles, itemBounds.Left, itemBounds.Bottom);
                    }
                    else
                    {
                        // Se nenhum item estiver selecionado, abre no canto superior esquerdo do ListView
                        listViewFiles.ContextMenuStrip.Show(listViewFiles, new System.Drawing.Point(0, 0));
                    }
                }
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (listViewFiles.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = listViewFiles.SelectedItems[0];
                    string path = selectedItem.Tag.ToString();
                    if (label1.Text == "Copy Path")
                    {
                        Clipboard.SetText(path);
                        label2.Text = path;
                        return;
                    }
                    else if (label1.Text == "Delete")
                    {
                        foreach (ListViewItem item in listViewFiles.SelectedItems)
                        {
                            string item_path = item.Tag.ToString();
                            if (del_files.Contains(item_path))
                            {
                                del_files.Remove(item_path);
                                item.ForeColor = System.Drawing.Color.White;
                                item.BackColor = System.Drawing.Color.Black;
                            }
                            else
                            {
                                del_files.Add(item_path);
                                item.ForeColor = System.Drawing.Color.Magenta;
                                item.BackColor = System.Drawing.Color.DarkRed;
                            }
                        }
                        if (del_files.Count > 1)
                        {
                            label2.Text = "(" + del_files.Count.ToString() + ")" + path;
                        }
                        else
                        {
                            label2.Text = path;
                        }
                        return;
                    }
                    else if (label1.Text == "Copy")
                    {
						foreach (ListViewItem item in listViewFiles.SelectedItems)
						{
							string item_path = item.Tag.ToString();
							if (sel_files.Contains(item_path))
							{
								sel_files.Remove(item_path);
								item.ForeColor = System.Drawing.Color.White;
								item.BackColor = System.Drawing.Color.Black;
							}
							else
							{
								sel_files.Add(item_path);
								item.ForeColor = System.Drawing.Color.Yellow;
								item.BackColor = System.Drawing.Color.DarkBlue;
							}
						}
						if (sel_files.Count > 1) { label2.Text = "(" + sel_files.Count.ToString() + ")" + path; }
						else
						{
							label2.Text = path;
						}
						
						Clipboard.SetFileDropList(sel_files);
						isForMove = false;
						return;
						/*
                        foreach (ListViewItem item in listViewFiles.SelectedItems)
                        {
                            string item_path = item.Tag.ToString();
                            if (sel_files.Contains(item_path))
                            {
                                sel_files.Remove(item_path);
                                item.ForeColor = System.Drawing.Color.White;
                                item.BackColor = System.Drawing.Color.Black;
                            }
                            else
                            {
                                sel_files.Add(item_path);
                                item.ForeColor = System.Drawing.Color.Yellow;
                                item.BackColor = System.Drawing.Color.DarkBlue;
                            }
                        }
                        if (sel_files.Count > 1) { label2.Text = "(" + sel_files.Count.ToString() + ")" + path; }
                        else
                        {
                            label2.Text = path;
                        }
                        return;
						*/
                    }
					else if (label1.Text == "Cut")
					{
						foreach (ListViewItem item in listViewFiles.SelectedItems)
						{
							string item_path = item.Tag.ToString();
							if (sel_files.Contains(item_path))
							{
								sel_files.Remove(item_path);
								item.ForeColor = System.Drawing.Color.White;
								item.BackColor = System.Drawing.Color.Black;
							}
							else
							{
								sel_files.Add(item_path);
								item.ForeColor = System.Drawing.Color.Yellow;
								item.BackColor = System.Drawing.Color.DarkBlue;
							}
						}
						if (sel_files.Count > 1) { label2.Text = "(" + sel_files.Count.ToString() + ")" + path; }
						else
						{
							label2.Text = path;
						}
						
						Clipboard.SetFileDropList(sel_files);
						isForMove = true;
						return;
					}
                    else if (label1.Text == "Paste")
                    {
						// >>
						Clipboard.SetFileDropList(sel_files);
						if (!Clipboard.ContainsFileDropList()) return;
						string action = isForMove ? "Move" : "Paste";
						if (MessageBox.Show($"{action} the Files and Folders?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No) return;

						try
						{
							if (Clipboard.ContainsFileDropList())
							{
								StringCollection filesToProcess = Clipboard.GetFileDropList();
								int totalItems = 0;
								int itemsProcessed = 0;

								// Calcula o total de itens
								foreach (string path_it in filesToProcess)
								{
									if (File.Exists(path_it))
									{
										totalItems++;
									}
									else if (Directory.Exists(path_it))
									{
										totalItems += CountItemsInDirectory(path_it);
									}
								}

								label3.Text = $"{action}ing: 0/{totalItems}";
								Application.DoEvents();

								string[] pathsToProcess = new string[filesToProcess.Count];
								filesToProcess.CopyTo(pathsToProcess, 0);

								foreach (string sourcePath in pathsToProcess)
								{
									string itemName = Path.GetFileName(sourcePath);
									string destPath = Path.Combine(current_dir, itemName);

									if (File.Exists(sourcePath)) // Arquivo
									{
										if (File.Exists(destPath))
										{
											DialogResult result = MessageBox.Show($"The '{itemName}' already exists. Overwrite it?",
												"Confirmation", MessageBoxButtons.YesNo);
											if (result == DialogResult.No) continue;
											if (isForMove && File.Exists(destPath)) File.Delete(destPath); // Remove destino existente antes de mover
										}
										if (isForMove)
										{
											File.Move(sourcePath, destPath); // Move diretamente
										}
										else
										{
											File.Copy(sourcePath, destPath, true); // Copia com sobrescrita
										}
										itemsProcessed++;
										label3.Text = $"{action}ing: {itemsProcessed}/{totalItems}";
										Application.DoEvents();
									}
									else if (Directory.Exists(sourcePath)) // Diretório
									{
										if (Directory.Exists(destPath))
										{
											DialogResult result = MessageBox.Show($"The '{itemName}' already exists. Overwrite it?",
												"Confirmation", MessageBoxButtons.YesNo);
											if (result == DialogResult.No) continue;
											if (isForMove && Directory.Exists(destPath)) Directory.Delete(destPath, true); // Remove destino existente
										}
										if (isForMove)
										{
											Directory.Move(sourcePath, destPath); // Move diretamente
										}
										else
										{
											CopyDirectoryWithProgress(sourcePath, destPath, ref itemsProcessed, totalItems); // Copia recursivamente
										}
										if (isForMove) itemsProcessed += CountItemsInDirectory(destPath); // Atualiza contador para diretórios movidos
										label3.Text = $"{action}ing: {itemsProcessed}/{totalItems}";
										Application.DoEvents();
									}
								}

								if (itemsProcessed > 0)
								{
									MessageBox.Show($"{action}d {itemsProcessed} item(s) in {current_dir}");
									LoadDirectory(current_dir);
									if (isForMove) sel_files.Clear(); // Limpa sel_files após mover
								}
								else
								{
									MessageBox.Show("Aborted.");
								}
								label3.Text = "...";
							}
							else
							{
								MessageBox.Show("No files or folders in Clipboard!");
							}
						}
						catch (UnauthorizedAccessException)
						{
							MessageBox.Show($"Access Denied while {action.ToLower()}ing.");
							label3.Text = "Error: Access Denied";
						}
						catch (Exception ex)
						{
							MessageBox.Show($"Error while {action.ToLower()}ing: " + ex.Message);
							label3.Text = "Error: " + ex.Message;
						}
						return;
						
						// <<
						
                        /*
                        Clipboard.SetFileDropList(sel_files); // Copia para a área de transferência
                        if (!Clipboard.ContainsFileDropList()) return;
                        if (MessageBox.Show("Paste the Files and Folders?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No) return;

                        try
                        {
                            if (Clipboard.ContainsFileDropList())
                            {
                                StringCollection filesToCopy = Clipboard.GetFileDropList();
                                int totalItems = 0;
                                int itemsCopied = 0;

                                // Calcula o total de itens a serem copiados
                                foreach (string path_it in filesToCopy)
                                {
                                    if (File.Exists(path_it))
                                    {
                                        totalItems++;
                                    }
                                    else if (Directory.Exists(path_it))
                                    {
                                        totalItems += CountItemsInDirectory(path_it); // Usa o mesmo método de contagem
                                    }
                                }

                                label3.Text = $"Copying: 0/{totalItems}";
                                Application.DoEvents(); // Atualiza a interface imediatamente

                                string[] pathsToCopy = new string[filesToCopy.Count];
                                filesToCopy.CopyTo(pathsToCopy, 0);

                                foreach (string sourcePath in pathsToCopy)
                                {
                                    string itemName = Path.GetFileName(sourcePath);
                                    string destPath = Path.Combine(current_dir, itemName);

                                    if (File.Exists(sourcePath)) // Se for um arquivo
                                    {
                                        if (File.Exists(destPath))
                                        {
                                            DialogResult result = MessageBox.Show($"The '{itemName}' already exists. Overwrite it?",
                                                "Confirmation", MessageBoxButtons.YesNo);
                                            if (result == DialogResult.No) continue;
                                        }
                                        File.Copy(sourcePath, destPath, true);
                                        itemsCopied++;
                                        label3.Text = $"Copying: {itemsCopied}/{totalItems}";
                                        Application.DoEvents();
                                    }
                                    else if (Directory.Exists(sourcePath)) // Se for um diretório
                                    {
                                        CopyDirectoryWithProgress(sourcePath, destPath, ref itemsCopied, totalItems);
                                    }
                                }

                                if (itemsCopied > 0)
                                {
                                    MessageBox.Show($"Pasted {itemsCopied} item(s) in {current_dir}");
                                    LoadDirectory(current_dir);
                                }
                                else
                                {
                                    MessageBox.Show("Aborted.");
                                }
                                label3.Text = "..."; // Reseta o Label
                            }
                            else
                            {
                                MessageBox.Show("No files or folders in Clipboard!");
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("Access Denied while pasting.");
                            label3.Text = "Error: Access Denied";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while pasting: " + ex.Message);
                            label3.Text = "Error: " + ex.Message;
                        }
                        return;
                        */

                    }
                    else if (label1.Text == "Execute")
                    {
                        // >>
                        try
                        {
                            string target_file;
                            if (listViewFiles.SelectedItems.Count > 0)
                            {
                                ListViewItem selectedItem1 = listViewFiles.SelectedItems[0];
                                // Se for um arquivo, usa o diretório pai; se for diretório, usa o próprio caminho
                                if (selectedItem1.SubItems[1].Text == "File")
                                {

                                    target_file = selectedItem1.Tag.ToString();

                                    if (GetFileExtension(target_file) == ".exe")
                                    {
                                        // Inicia o CMD no diretório especificado
                                        ProcessStartInfo processInfo = new ProcessStartInfo
                                        {
                                            FileName = target_file,
                                            Arguments = "",
                                            UseShellExecute = false // Permite abrir a janela do CMD
                                        };
                                        Process.Start(processInfo);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Executing Program: " + ex.Message);
                        }
                        // <<
                        label1.Text = "Copy Path";
                        label1.ForeColor = System.Drawing.Color.Lime; // Verde para diretórios
                    }
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (label1.Text == "Delete" && del_files.Count > 0)
                {
                    if (MessageBox.Show($"Are you sure you want to delete {del_files.Count} file(s)?",
                                        "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // >>
                        int totalItems = 0;
                        int itemsDeleted = 0;

                        // Calcula o total de itens a serem deletados (arquivos + subitens em diretórios)
                        foreach (string path in del_files)
                        {
                            if (File.Exists(path))
                            {
                                totalItems++;
                            }
                            else if (Directory.Exists(path))
                            {
                                totalItems += CountItemsInDirectory(path); // Conta arquivos e subdiretórios
                            }
                        }
                        label3.Text = $"Deleting: 0/{totalItems}";
                        Application.DoEvents(); // Atualiza a interface imediatamente
                                                // <<
                        try
                        {
                            // >>
                            string[] array1 = new string[del_files.Count];
                            del_files.CopyTo(array1, 0);
                            foreach (string path in array1) // Usa ToArray para evitar modificação durante iteração
                            {
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                    itemsDeleted++;
                                    label3.Text = $"Deleting: {itemsDeleted}/{totalItems}";
                                    Application.DoEvents(); // Atualiza o Label em tempo real
                                }
                                else if (Directory.Exists(path))
                                {
                                    DeleteDirectoryWithProgress(path, ref itemsDeleted, totalItems);
                                }
                            }
                            del_files.Clear(); // Limpa a coleção após a exclusão
                            MessageBox.Show($"Deleted {itemsDeleted} item(s).");
                            LoadDirectory(current_dir); // Atualiza o ListView
                            label3.Text = "..."; // Reseta o Label
                                                 // <<

                            /*
                            foreach (string filePath in del_files)
                            {
                                if (File.Exists(filePath))
                                {
                                    File.Delete(filePath);
                                    filesDeleted++;
                                }
                                else if (Directory.Exists(filePath)) // Se for um diretório
                                {
                                    Directory.Delete(filePath, true); // true para deletar pastas não vazias
                                    filesDeleted++;
                                }
                            }
                            del_files.Clear(); // Limpa a coleção após a exclusão
                            MessageBox.Show($"Deleted {filesDeleted} file(s).");
                            LoadDirectory(current_dir); // Atualiza o ListView
							*/
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("Access Denied while deleting files.");
                            label3.Text = "Error: Access Denied";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while deleting files: " + ex.Message);
                            label3.Text = "Error: " + ex.Message;
                        }
                    }
                }
                label1.Text = "Copy Path";
                label1.ForeColor = System.Drawing.Color.Lime; // Verde para diretórios
                return;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Copy_Path_Mode(object sender, EventArgs e)
        {
            label1.Text = "Copy Path";
            label1.ForeColor = System.Drawing.Color.Lime; // Verde para diretórios
        }

        private void Edit_Mode(object sender, EventArgs e)
        {
            label1.Text = "Edit/Read";
            label1.ForeColor = System.Drawing.Color.Magenta; // Verde para diretórios
        }

        private void Copy_Mode(object sender, EventArgs e)
        {
            label1.Text = "Copy";
            label1.ForeColor = System.Drawing.Color.Cyan; // Verde para diretórios
        }

        private void Paste_Mode(object sender, EventArgs e)
        {
            label1.Text = "Paste";
            label1.ForeColor = System.Drawing.Color.Red; // Verde para diretórios
        }

        private void Execute_Mode(object sender, EventArgs e)
        {
            label1.Text = "Execute";
            label1.ForeColor = System.Drawing.Color.Yellow; // Verde para diretórios
        }

        private void Delete_Mode(object sender, EventArgs e)
        {
            label1.Text = "Delete";
            label1.ForeColor = System.Drawing.Color.Red; // Verde para diretórios
        }

        private void GoToDir(object sender, EventArgs e)
        {
            //this.TopMost = false;
            string input = Interaction.InputBox(
                "Enter a diretory path:", // Mensagem do pop-up
                "Set Directory (path)",            // Título do pop-up
                "C:\\"          // Texto padrão no campo
            );
            //this.TopMost = true;
            if (Directory.Exists(input) == false) return;
            current_dir = input;
            parent_dir = input;
            LoadDirectory(input);
        }

        private void Open_Windows_Explorer(object sender, EventArgs e)
        {
            try
            {
                if (listViewFiles.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = listViewFiles.SelectedItems[0];
                    string path = selectedItem.Tag.ToString();

                    // Abre o Explorer no diretório pai se for um arquivo, ou diretamente se for um diretório
                    if (selectedItem.SubItems[1].Text == "File")
                    {
                        Process.Start("explorer.exe", $"/select,\"{path}\""); // Abre com o arquivo selecionado
                    }
                    else if (selectedItem.SubItems[1].Text == "Directory")
                    {
                        Process.Start("explorer.exe", $"\"{path}\""); // Abre o diretório
                    }
                }
                else
                {
                    // Abre o diretório atual se nada estiver selecionado
                    Process.Start("explorer.exe", $"\"{current_dir}\"");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Explorer: " + ex.Message);
            }
        }

        private void Open_CMD(object sender, EventArgs e)
        {
            // >>
            try
            {
                string targetDir;
                if (listViewFiles.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = listViewFiles.SelectedItems[0];
                    string path = selectedItem.Tag.ToString();

                    // Se for um arquivo, usa o diretório pai; se for diretório, usa o próprio caminho
                    targetDir = (selectedItem.SubItems[1].Text == "File")
                        ? Path.GetDirectoryName(path)
                        : path;
                }
                else
                {
                    targetDir = current_dir; // Usa o diretório atual se nada estiver selecionado
                }

                // Inicia o CMD no diretório especificado
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/K cd /d \"{targetDir}\"", // /K mantém o CMD aberto após o comando
                    UseShellExecute = true // Permite abrir a janela do CMD
                };
                Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening CMD: " + ex.Message);
            }
            // <<
        }

        private int CountItemsInDirectory(string directoryPath)
        {
            int count = 0;
            try
            {
                count += Directory.GetFiles(directoryPath).Length; // Conta arquivos diretamente no diretório
                foreach (string subDir in Directory.GetDirectories(directoryPath))
                {
                    count++; // Conta o próprio subdiretório
                    count += CountItemsInDirectory(subDir); // Conta recursivamente os itens dentro dele
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Ignora erros de acesso, continua contando outros itens
            }
            return count;
        }

        private void DeleteDirectoryWithProgress(string directoryPath, ref int itemsDeleted, int totalItems)
        {
            try
            {
                // Deleta arquivos no diretório atual
                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    File.Delete(file);
                    itemsDeleted++;
                    label3.Text = $"Deleting: {itemsDeleted}/{totalItems}";
                    Application.DoEvents(); // Atualiza o Label em tempo real
                }

                // Deleta subdiretórios recursivamente
                foreach (string subDir in Directory.GetDirectories(directoryPath))
                {
                    DeleteDirectoryWithProgress(subDir, ref itemsDeleted, totalItems);
                }

                // Deleta o diretório vazio
                Directory.Delete(directoryPath);
                itemsDeleted++;
                label3.Text = $"Deleting: {itemsDeleted}/{totalItems}";
                Application.DoEvents();
            }
            catch (UnauthorizedAccessException)
            {
                throw; // Propaga o erro para o try-catch principal
            }
        }

        private void CopyDirectoryWithProgress(string sourceDir, string destDir, ref int itemsCopied, int totalItems)
        {
            try
            {
                // Cria o diretório de destino se não existir
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                    itemsCopied++; // Conta o diretório criado
                    label3.Text = $"Copying: {itemsCopied}/{totalItems}";
                    Application.DoEvents();
                }

                // Copia os arquivos do diretório atual
                foreach (string file in Directory.GetFiles(sourceDir))
                {
                    string destFile = Path.Combine(destDir, Path.GetFileName(file));
                    if (File.Exists(destFile))
                    {
                        DialogResult result = MessageBox.Show($"The '{Path.GetFileName(file)}' already exists. Overwrite it?",
                            "Confirmation", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No) continue;
                    }
                    File.Copy(file, destFile, true);
                    itemsCopied++;
                    label3.Text = $"Copying: {itemsCopied}/{totalItems}";
                    Application.DoEvents();
                }

                // Copia subdiretórios recursivamente
                foreach (string subDir in Directory.GetDirectories(sourceDir))
                {
                    string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                    CopyDirectoryWithProgress(subDir, destSubDir, ref itemsCopied, totalItems);
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw; // Propaga o erro para o try-catch principal
            }
        }

        private string GetFileExtension(string path)
        {
            return Path.GetExtension(path);
        }

        private void Cut_Mode(object sender, EventArgs e)
        {
			label1.Text = "Cut";
			label1.ForeColor = System.Drawing.Color.Orange; // Cor diferente para distinguir
        }
    }
}