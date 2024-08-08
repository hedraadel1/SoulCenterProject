//-----------------------------------------------------------------------
// <copyright file="soulstudio.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bunifu.UI.WinForms.Helpers.Transitions;

using CefSharp.OffScreen;

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using SoulCenterProject.Helpers;
using SoulCenterProject.Helpers.Gemini.Model;
using SoulCenterProject.Helpers.Utils;
using SoulCenterProject.Models.SoulModels;
using SoulCenterProject.Modules.Ai.CodeGeneration;
using SoulCenterProject.Modules.Ai.Services;
using SoulCenterProject.SoulControls;
using SoulCenterProject.SoulControls.ComponentsTypes;

using Telerik.WinControls.UI;

// ReSharper disable All

namespace SoulCenterProject
{
    //var conn = ConnectionFactory.CreateConnection();
    // var repo = new CategoryRepository(conn);
    public partial class soulstudio : UserControl
    {
        private IDbConnection _conn;

        private Stopwatch _stopwatch;
        private string apiKey = "AIzaSyAOhXQtle2SHm-LC6KcoG_2NOFMbTP9n_w";
        private ChromiumWebBrowser browser; // CefSharp browser instance
        private SoultextEditor deveditor;

        private Soultexteditortelerik teleditor;

        public string connectionString =
            "Server=178.18.251.168;Database=newgyral_erpnew;Uid=newgyral_erpnew;Pwd=Mm102030@@@;";

        private SoulStudioService soulStudioService;


        // Assuming you have a BindingSource called soul_PromptComponentsBindingSource

        ComponentsTypes userControl1 = new ComponentsTypes();
        ComponentsTypes userControl2 = new ComponentsTypes();
        private BackgroundWorker worker; // BackgroundWorker for tasks
        private XtraForm comeform;

        public soulstudio(XtraForm formconForm = null)
        {
            InitializeComponent();
            if (formconForm != null)
            {
                this.comeform = formconForm;
            }

            deveditor = new SoultextEditor();
            teleditor = new Soultexteditortelerik();

            soulStudioService = new SoulStudioService(connectionString);


            // GeminiApi giApi = new GeminiApi();

            // In the constructor, initialize the Stopwatch
            _stopwatch = new Stopwatch();


            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;


            deveditor.Dock = DockStyle.Fill;
            teleditor.Dock = DockStyle.Fill;
            editorpanel.Controls.Add(deveditor);
            editorpanel.Controls.Remove(deveditor);
            editorpanel.Controls.Add(teleditor);
            if (Barbutton_edtiortype.Checked)
            {
                if (CheckButton_displayeditorribon.Checked)
                {
                    teleditor.richTextEditorRibbonBar1.Visible = true;
                }
                else
                {
                    teleditor.richTextEditorRibbonBar1.Visible = false;
                }
            }

            _conn = ConnectionFactory.CreateConnection(); // Initialize connection in constructor


            //_repo = new CategoryRepository(_conn); // Initialize repository with connection

            //sqlDataSource1.FillAsync();

            // sqlDataSource_PromptComponent_Type.Fill();
            Treelist_PromptComponanents.ForceInitialize();

            RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
            buttonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            buttonEdit.Buttons.Add(new EditorButton(ButtonPredefines.SpinUp));
            buttonEdit.ButtonClick += ButtonEdit_ButtonClick;
            buttonEdit.Buttons
                .Add(
                    new EditorButton(
                        ButtonPredefines
                            .Clear));
            buttonEdit.Buttons.Add(new EditorButton(ButtonPredefines.OK));
            buttonEdit.ButtonClick += ButtonEdit_ButtonClick;
            buttonEdit.Buttons.Add(new EditorButton(ButtonPredefines.SpinDown));
            buttonEdit.ButtonClick += ButtonEdit_ButtonClick;

            Treelist_PromptComponanents.Columns["colButtons"].ColumnEdit = buttonEdit;
            Treelist_PromptComponanents.ExpandAll();


            // This line of code is generated by Data Source Configuration Wizard
            // Fill the SqlDataSource asynchronously
            sqlDataSource1.FillAsync();
        }

        private async Task addnewdata()
        {
            string newPromptName = "texttest.Text";
            var newCategory = new SoulCategory { CategoryName = newPromptName };


            // Set the TableType property here with a valid value
            newCategory.TableType = "MyCategoryType"; // Replace with your desired type


            // await _repo.AddCategory(newCategory);
        }

        private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string userMessage = (string)e.Argument;
            string message = await SendRequestAsync();
            e.Result = message; // احفظ النتيجة عشان نستخدمها في RunWorkerCompleted
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DisplayProgress.EditValue = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Hide progress bar and re-enable button
            DisplayProgress.Visible = false;


            if (e.Error != null)
            {
                // Handle errors (e.g., display a message box)
                MessageBox.Show("An error occurred: " + e.Error.Message);
            }
            else
            {
                string message = (string)e.Result;
                AddMessageToPanel(message); // حدث واجهة المستخدم بالنتيجة 
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DisplayProgress.EditValue = e.ProgressPercentage;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Hide progress bar and re-enable button
            DisplayProgress.Visible = false;


            if (e.Error != null)
            {
                // Handle errors (e.g., display a message box)
                MessageBox.Show("An error occurred: " + e.Error.Message);
            }
            else
            {
                ResponseContainer_Text.ResumeLayout();
            }
        }

        private void BarButton_Browser_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void Barbutton_edtiortype_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (Barbutton_edtiortype.Checked == false)
            {
                editorpanel.Controls.Remove(deveditor);
                editorpanel.Controls.Add(teleditor);
            }
            else
            {
                editorpanel.Controls.Remove(deveditor);
                editorpanel.Controls.Add(teleditor);
            }

            if (Barbutton_edtiortype.Checked)
            {
                if (CheckButton_displayeditorribon.Checked)
                {
                    teleditor.richTextEditorRibbonBar1.Visible = true;
                }
                else
                {
                    teleditor.richTextEditorRibbonBar1.Visible = false;
                }
            }
        }

        private void BarButton_ShowjsonInput_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void BarButton_ShowjsonOutput_ItemClick(object sender, ItemClickEventArgs e)
        {
            SoulLog log = new SoulLog
            {
                LogAddress = "Some Log Address",
                LogDetails = "Function processing data...",
                Step = OperationStep.Working
            };

            ReportSoulLog(log);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
        }

        private void Button_CloseBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception exception)
            {
            }
        }

        private void ResetMaxOutput()
        {
            Text_ModelMaxOutputTokens.Text = "0000";
            Text_RemainModelMaxOutputTokens.Text = "0000";
            Text_SelectedModelMaxOutputTokens.Text = "0000";

            Progressbar_OutputTokens.Value1 = 0;
            Progressbar_OutputTokens.Value2 = 0;
            Progressbar_OutputTokens.Maximum = 100;

            SpinEditor_SelectedModelMaxOutputTokens.Value = 0;
            SpinEditor_SelectedModelMaxOutputTokens.Maximum = 0;
            TrackBar_SelectedModelMaxOutputTokens.Value = 0;
            TrackBar_SelectedModelMaxOutputTokens.Properties.Maximum = 100;
        }

        private void ResetMaxInput()
        {
            Text_ModelMaxInputTokens.Text = "0000";
            Text_RemainModelMaxInputTokens.Text = "0000";
            Text_SelectedModelMaxInputTokens.Text = "0000";

            Progressbar_InputTokens.Value1 = 0;
            Progressbar_InputTokens.Value2 = 0;
            Progressbar_InputTokens.Maximum = 100;

            SpinEditor_SelectedModelMaxInputTokens.Value = 0;
            SpinEditor_SelectedModelMaxInputTokens.Maximum = 0;
            TrackBar_SelectedModelMaxInputTokens.Value = 0;
            TrackBar_SelectedModelMaxInputTokens.Properties.Maximum = 100;
        }

        private void Button_ExitBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void Button_getstructure_Click(object sender, EventArgs e)
        {
            //DisplayProgress.Visible = true;

            //Button_TestEdit.Enabled = false;
            //string url = Textbox_Url.Text;

            //worker.RunWorkerAsync(new Tuple<OperationType, string>(OperationType.GetStructure, url));
        }

        private void Button_MinBarButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            comeform.WindowState = FormWindowState.Minimized;


            //   MinimizeForm?.Invoke(this, EventArgs.Empty);
        }

        private async void Button_SendToAiModel_Click(object sender, EventArgs e)
        {
            //  backgroundWorker1.RunWorkerAsync(teleditor.radRichTextEditor1.Text);


            await SendmessagetoaimodelAsync();


            //string apiKey = "AIzaSyAOhXQtle2SHm-LC6KcoG_2NOFMbTP9n_w"; // Replace with your actual key
            //var geminiService = new GeminiService(apiKey);

            //string userMessage = teleditor.radRichTextEditor1.Text;
            //string conversationHistory = "I'm doing well, thanks for asking!"; // Optional
            //AddMessageToPanel(userMessage);
            //try
            //{
            //    string generatedText = await GenerateTextAsync(userMessage, conversationHistory);
            //    ResponseContainer_Text.Text = generatedText;
            //    //   Console.WriteLine("Generated Text: " + generatedText);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error generating text: " + ex.Message);
            //}
        }

        private void Button_SendToAiModel_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
        }

        private async void Button_Testdelete_Click(object sender, EventArgs e)
        {
            await DeletePrompt();
        }

        private void Button_TestEdit_Click(object sender, EventArgs e)
        {
            // Show progress bar and disable button
            DisplayProgress.Visible = true;


            // Kick off the background worker
            backgroundWorker1.RunWorkerAsync(ResponseContainer_Text.Text); // Pass HTML as argument


            //await EditPrompt();
            //  string htmlCode = txtHtml.Text;  // Grab HTML from TextBox
        }

        private void ButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.SpinUp)
            {
                MoveNodeVertically(false); // Move down
            }

            if (e.Button.Kind == ButtonPredefines.Clear)
            {
                TreeListNode node = Treelist_PromptComponanents.FocusedNode;
                if (node != null)
                {
                    node.Remove();
                }
            }

            if (e.Button.Kind == ButtonPredefines.OK)
            {
                TreeListNode node = Treelist_PromptComponanents.FocusedNode;
                if (node != null)
                {
                    MessageBox.Show(
                        Treelist_PromptComponanents.FocusedNode
                            .GetValue(Treelist_PromptComponanents.Columns["treeListColumn1"])
                            .ToString());
                }
            }

            if (e.Button.Kind == ButtonPredefines.SpinDown)
            {
                //  MoveNodeVertically(true); // Move down
                Treelist_PromptComponanents.MoveNext();
            }
        }

        private void CheckButton_displayeditorribon_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (Barbutton_edtiortype.Checked)
            {
                if (CheckButton_displayeditorribon.Checked)
                {
                    teleditor.richTextEditorRibbonBar1.Visible = true;
                }
                else
                {
                    teleditor.richTextEditorRibbonBar1.Visible = false;
                }
            }
        }

        private void Combobox_Prompt_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void componenttypeusercontrols()
        {
            //    userControl1.DataSource = soul_PromptComponentsBindingSource;
            userControl1.ActiveFilter = "[ComponentTypeName] = 'Topic' And [isActive] = True And [isEnabled] = True";


            //   userControl2.DataSource = soul_PromptComponentsBindingSource; // Same data source can be used
            userControl2.ActiveFilter = "[ComponentTypeName] = 'Prompt' And [isActive] = True";
            TableLayout_PromptDesign.Controls.Add(userControl1, 0, 2);
            TableLayout_PromptDesign.Controls.Add(userControl1, 0, 3);
        }

        private void CreateAndAddSeparator()
        {
            var separatorControl = new DevExpress.XtraEditors.SeparatorControl
            {
                BackColor = System.Drawing.Color.Transparent,
                Dock = DockStyle.Top,
                LineAlignment = DevExpress.XtraEditors.Alignment.Center,
                LineColor = System.Drawing.Color.White,
                LineStyle = System.Drawing.Drawing2D.DashStyle.Custom,
                LineThickness = 2,
                Location = new System.Drawing.Point(33, 245),
                LookAndFeel =
                {
                    UseDefaultLookAndFeel = false, SkinMaskColor = System.Drawing.Color.Black,
                    SkinMaskColor2 = System.Drawing.Color.Black
                },
                Name = "separatorControl4",
                Padding = new System.Windows.Forms.Padding(9, 3, 9, 3),
                Size = new System.Drawing.Size(644, 23),
                TabIndex = 26
            };

            gradientPanelExt1.Controls.Add(separatorControl);
        }

        private async Task DeletePrompt()
        {
            //var selectedCategory = (SoulCategory)Combobox_Prompt.SelectedItem;
            //if (selectedCategory == null)
            //{
            //    MessageBox.Show("itesnull");
            //    return;
            //}

            //int categoryIdToDelete = selectedCategory.CategoryID;

            //await _repo.DeleteCategory(categoryIdToDelete);
            //await PopulateComboboxAsync(); // Refresh combobox after deletion
        }

        private async Task EditPrompt()
        {
            //var newCategory = new SoulCategory();

            //var selecteditemdetails = Combobox_Prompt.get.;

            //newCategory.CategoryID = Convert.ToInt32(selecteditemdetails.c);


            //// Modify CategoryName (example)
            //newCategory.CategoryName = "testName";

            //await _repo.UpdateCategory(newCategory);
            //await PopulateComboboxAsync(); // Refresh combobox after update
        }

        private void gridLookUpEdit1View_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }

                GridView view = sender as GridView;
                view.DeleteRow(view.FocusedRowHandle);
            }
        }

        private void gridLookUpEdit1View_RowUpdated(object sender, RowObjectEventArgs e)
        {
        }

        private bool IsServiceColumn(LookUpEdit edit, string fieldName)
        {
            return fieldName == edit.Properties.ValueMember || fieldName == edit.Properties.DisplayMember;
        }

        private async void ListBox_Sitestructure_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(ListBox_Sitestructure.SelectedItem != null)
            //{
            //    string selectedItem = ListBox_Sitestructure.SelectedItem.ToString();
            //    string[] parts = selectedItem.Split(new[] { ' ' }, 2);
            //    string elementName = parts[0];
            //    string className = parts[1].Trim('(', ')');
            //    string url = Textbox_Url.Text;


            //    worker.RunWorkerAsync(
            //        new Tuple<OperationType, string, string, string>(
            //            OperationType.ExtractContent,
            //            url,
            //            elementName,
            //            className));
            //}
        }

        private void lookUpEdit1_CustomDrawCell(object sender, LookUpCustomDrawCellArgs e)
        {
            LookUpEdit edit = sender as LookUpEdit;
            if (IsServiceColumn(edit, e.Column.FieldName))
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void lookUpEdit1_CustomDrawHeader(object sender, LookUpCustomDrawHeaderArgs e)
        {
            LookUpEdit edit = sender as LookUpEdit;
            if (IsServiceColumn(edit, e.Header.Column.FieldName))
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 55, 0)), e.Bounds);
                e.Graphics.DrawString(e.Header.Caption, e.Header.Appearance.Font, Brushes.White, e.Header.CaptionRect);
                e.Handled = true;
            }
        }

        private void lookUpEdit1_CustomDrawRow(object sender, LookUpCustomDrawRowArgs e)
        {
            if (e.IsSelected)
            {
                using (HatchBrush brush = new HatchBrush(
                           HatchStyle.Weave,
                           Color.FromArgb(30, Color.FromArgb(247, 5, 255)),
                           Color.FromArgb(120, Color.White)))
                {
                    e.DefaultDraw();
                    e.Cache.FillRectangle(brush, e.Bounds);
                    e.Handled = true;
                }
            }
        }

        private void lookUpEdit1_Properties_CustomDrawHeader(object sender, LookUpCustomDrawHeaderArgs e)
        {
            LookUpEdit edit = sender as LookUpEdit;
            if (IsServiceColumn(edit, e.Header.Column.FieldName))
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 255, 8)), e.Bounds);
                e.Graphics.DrawString(e.Header.Caption, e.Header.Appearance.Font, Brushes.White, e.Header.CaptionRect);
                e.Handled = true;
            }
        }

        private void Messages_DocumentTabStrip_TextChanged(object sender, EventArgs e)
        {
        }

        private void MoveNodeVertically(bool isDown)
        {
            TreeListNode node = Treelist_PromptComponanents.FocusedNode;
            if (node != null)
            {
                int currentIndex = Treelist_PromptComponanents.GetNodeIndex(node);
                int newIndex = currentIndex + (isDown ? 1 : -1);


                // Check if new index is within bounds
                if (newIndex >= 0 && newIndex < Treelist_PromptComponanents.Nodes.Count)
                {
                    Treelist_PromptComponanents.SetNodeIndex(node, newIndex);
                }
            }
        }

        private void radButton36_Click_1(object sender, EventArgs e)
        {
        }

        private async void radButton4_Click(object sender, EventArgs e)
        {
        }

        private async void radButton5_Click(object sender, EventArgs e)
        {
            await addnewdata();
        }

        private async Task SendmessagetoaimodelAsync()
        {
            ////////////////////////////////////////////////
            SoulLog log = new SoulLog
            {
                LogAddress = "Sending User Prompt",
                LogDetails = teleditor.radRichTextEditor1.Text,
                Step = OperationStep.Working
            };

            ReportSoulLog(log);
            ///////////////////////////////////////////////
            var message = await SendRequestAsync();

            AddMessageToPanel(message);


            //this.backgroundWorker1.RunWorkerAsync();
        }

        #region SendRequestAsync

        private async Task<string> SendRequestAsync()
        {
            /// <summary>
            /// Sends the user's message to the AI model and returns the generated response.
            /// </summary>
            /// <param name="userMessage">The message entered by the user.</param>
            /// <param name="conversationHistory">Any previous conversation history.</param>
            /// <returns>The generated response from the AI model.</returns>
            /// <exception cref="Exception">Thrown if there is an error sending the request or processing the response.</exception>
            GeminiAiService gemini = new GeminiAiService();
            try
            {
                // The code is responsible for retrieving the user's input message from a RichTextEditor control (teleditor.radRichTextEditor1.Text). 
                string userMessage = teleditor.radRichTextEditor1.Text;


                // we will write the conversationHistory code later , this is just for the test perpose
                string conversationHistory = "I'm doing well, thanks for asking!";
                SoulLog log = new SoulLog
                {
                    LogAddress = "SendRequestAsync() method",
                    LogDetails = "Sends the user's message to the AI model and returns the generated response.",
                    Step = OperationStep.Working
                };

                ReportSoulLog(log);


                // Add MessagesContainer with user message
                MessagesContainer userMessageContainer = CreateNewMessageAddToPanel();
                userMessageContainer.StartNewMessage(MessagesContainer.MessageRole.User, "Hedra");
                userMessageContainer.MessageOutput = userMessage;
                userMessageContainer.MessageReceived();
                SoulLog log2 = new SoulLog
                {
                    LogAddress = "SendRequestAsync() method 588",
                    LogDetails =
                        "userMessageContainer,userMessageContainer.MessageOutput = userMessage,userMessageContainer.MessageReceived()",
                    Step = OperationStep.Working
                };

                ReportSoulLog(log2);


                // Add empty MessagesContainer for AI response
                MessagesContainer aiResponseContainer = AddEmptyMessageContainer();
                aiResponseContainer.StartNewMessage(MessagesContainer.MessageRole.Model, "Souly");
                SoulLog log3 = new SoulLog
                {
                    LogAddress = "SendRequestAsync() method 599",
                    LogDetails =
                        "aiResponseContainer,aiResponseContainer.StartNewMessage(MessagesContainer.MessageRole.Model,Souly",
                    Step = OperationStep.Working
                };

                ReportSoulLog(log3);


                // Generate AI response asynchronously
                string generatedText = await gemini.GenerateTextAsync(userMessage, conversationHistory);

                if (aiResponseContainer.InvokeRequired)
                {
                    aiResponseContainer.Invoke((Action)(() =>
                    {
                        aiResponseContainer.MessageReceived();
                        aiResponseContainer.MessageOutput = generatedText;
                    }));
                }
                else
                {
                    aiResponseContainer.MessageReceived();
                    aiResponseContainer.MessageOutput = generatedText;
                }

                return generatedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return null;
            }
        }

        private MessagesContainer AddEmptyMessageContainer()
        {
            MessagesContainer ms = new MessagesContainer();
            ms.Dock = DockStyle.Top;

            gradientPanelExt1.Controls.Add(ms);
            CreateAndAddSeparator();
            return ms;
        }

        #endregion

        private async Task<string> SendRequestAsync2()
        {
            /// <summary>
            /// Sends the user's message to the AI model and returns the generated response.
            /// </summary>
            /// <param name="userMessage">The message entered by the user.</param>
            /// <param name="conversationHistory">Any previous conversation history.</param>
            /// <returns>The generated response from the AI model.</returns>
            /// <exception cref="Exception">Thrown if there is an error sending the request or processing the response.</exception>

            GeminiAiService gemini = new GeminiAiService();
            try
            {
                // Retrieve user message
                string userMessage = teleditor.radRichTextEditor1.Text;


                // Placeholder conversation history
                string conversationHistory = "I'm doing well, thanks for asking!";


                // Add MessagesContainer with user message
                MessagesContainer userMessageContainer = CreateNewMessageAddToPanel();
                userMessageContainer.StartNewMessage(MessagesContainer.MessageRole.User, "Hedra");
                userMessageContainer.MessageOutput = userMessage;
                userMessageContainer.MessageReceived();


                // Add empty MessagesContainer for AI response
                MessagesContainer aiResponseContainer = AddEmptyMessageContainer();
                aiResponseContainer.StartNewMessage(MessagesContainer.MessageRole.Model, "Souly");


                // Generate AI response asynchronously
                string generatedText = await gemini.GenerateTextAsync(userMessage, conversationHistory);

                if (aiResponseContainer.InvokeRequired)
                {
                    aiResponseContainer.Invoke((Action)(() =>
                    {
                        aiResponseContainer.MessageReceived();
                        aiResponseContainer.MessageOutput = generatedText;
                    }));
                }
                else
                {
                    aiResponseContainer.MessageReceived();
                    aiResponseContainer.MessageOutput = generatedText;
                }

                return generatedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return null;
            }
        }

        #region SendRequestAsync Commented

        /*             private async Task<string> SendRequestAsync()
                    {
                        try
                        {
                            _stopwatch.Restart();

                            // GeminiAiService gemi = new GeminiService(apiKey);
                            // actual gemini apikey
                            string apiKey = "AIzaSyAOhXQtle2SHm-LC6KcoG_2NOFMbTP9n_w";
                            GeminiAiService geminiService = new GeminiAiService(apiKey);

                            string userMessage = teleditor.radRichTextEditor1.Text;
                            // Optional
                            string conversationHistory = "I'm doing well, thanks for asking!";

                            AddMessageToPanel(userMessage);

                            string generatedText = await GenerateTextAsync(userMessage, conversationHistory);


                            return generatedText;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}");
                            return null;
                        }
                    } */

        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // Adding a root node with data "Root Node"
            Treelist_PromptComponanents.Nodes.Add(new object[] { "Root Node" });
            Treelist_PromptComponanents.Nodes.Add(new object[] { "Root Node 2 " });
            Treelist_PromptComponanents.Nodes.Add(new object[] { "Root Node 3 " });
        }

#pragma warning disable IDE1006 // Naming Styles
        private void simpleButton1_MouseMove(object sender, MouseEventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            if (e.Button == MouseButtons.Left)
            {
                simpleButton1.DoDragDrop(sender as SimpleButton, DragDropEffects.All);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Treelist_PromptComponanents.Nodes.Add(new object[] { "Root Node 666666666666666" });
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            // StringBuilder for efficient string concatenation
            StringBuilder allRowsData = new StringBuilder();


            // Iterate through each root node
            foreach (TreeListNode node in Treelist_PromptComponanents.Nodes)
            {
                // Recursive function to traverse child nodes 
                GetAllChildNodesData(node, allRowsData);
            }


            // Get all child nodes data recursively
            void GetAllChildNodesData(TreeListNode node, StringBuilder data)
            {
                // Append node data to the StringBuilder
                data.Append(node.GetValue(0)); // Assuming data is in the first column
                data.Append(node.GetValue(0) + Environment.NewLine); // Add new line after each node's data


                // Recursively call for child nodes
                foreach (TreeListNode childNode in node.Nodes)
                {
                    GetAllChildNodesData(childNode, data);
                }
            }


            // The final string containing data from all rows
            string combinedData = allRowsData.ToString();
            MessageBox.Show(combinedData);
        }

        private void soulstudio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && e.KeyCode == Keys.Alt)
            {
                SendmessagetoaimodelAsync();
            }
        }

        private async void soulstudio_Load(object sender, EventArgs e)
        {
            //componenttypeusercontrols();
            ResetMaxOutput();
            ResetMaxInput();
        }

        private void Treelist_PromptComponanents_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
        }

        private void Treelist_PromptComponanents_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
        }

#pragma warning disable IDE1006 // Naming Styles
        private void treeList1_DragDrop(object sender, DragEventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            if (e.Data.GetDataPresent(typeof(SimpleButton)))
            {
                SimpleButton button = e.Data.GetData(typeof(SimpleButton)) as SimpleButton;
                Treelist_PromptComponanents.AppendNode(new object[] { button.Text }, -1);
            }
        }

        private void treeList1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void txtHtml_TextChanged(object sender, EventArgs e)
        {
            int charsCount = ResponseContainer_Text.Document.GetStatisticsInfo().CharactersCount;

            if (charsCount > 5000) // Adjust threshold as needed
            {
                // Show progress bar and disable button
                DisplayProgress.Visible = true;

                ResponseContainer_Text.SuspendLayout();


                // Start background worker
                backgroundWorker2.RunWorkerAsync(ResponseContainer_Text.Text);
            }
        }

        private void txtHtml_Validating(object sender, CancelEventArgs e)
        {
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DisplayProgress.EditValue = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //// Re-enable button and hide progress bar
            //Button_getstructure.Enabled = true;
            //DisplayProgress.Visible = false;

            //if(e.Error != null)
            //{
            //    // ... (Handle errors)
            //} else
            //{
            //}
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Space))
            {
                // Call your function here
                SendmessagetoaimodelAsync();

                return true; // Indicate that the key combination was handled
            }

            return base.ProcessCmdKey(ref msg, keyData); // Let the base class handle other key combinations
        }

        public MessagesContainer CreateNewMessageAddToPanel()
        {
            MessagesContainer ms = new MessagesContainer();
            ms.Dock = DockStyle.Top;

            gradientPanelExt1.Controls.Add(ms);
            CreateAndAddSeparator();
            return ms;
        }

        public void AddMessageToPanel(string message)
        {
            //    CreateNewMessageAddToPanel().MessageOutput = message; 
        }

        public async Task<string> GenerateTextAsyncWithComments(string userMessage, string conversationHistory = "")
        {
            try
            {
                // Create a new HttpClient instance for this specific request
                using (var client = new HttpClient())
                {
                    // Construct the request Uri with API endpoint, model name, and API key
                    var requestUri = new Uri(
                        $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-pro-latest:generateContent?key={apiKey}");


                    // Define the request body structure as an anonymous object
                    var requestBody = JsonConvert.SerializeObject(
                        new
                        {
                            contents = new List<object>
                            {
                                // Include user message and optional conversation history in parts
                                new
                                {
                                    parts = new List<object>
                                    {
                                        new { text = userMessage },
                                        new { text = conversationHistory }
                                    }
                                }
                            }
                        });


                    // Create StringContent object with UTF-8 encoding and application/json content type
                    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");


                    // Send the POST request asynchronously and await the response
                    var response = await client.PostAsync(requestUri, content);


                    // Ensure successful status code (2xx range) or throw an exception
                    response.EnsureSuccessStatusCode();


                    // Read the response content as a string and return it
                    var responseString = await response.Content.ReadAsStringAsync();
                    return responseString;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to generate text from Gemini API.", ex);
            }
        }

        public void ReportSoulLog(SoulLog logEntry)
        {
            // WOW Improvement 1: Add timestamp for better tracking
            logEntry.OpStart = DateTime.Now; // Capture current time


            // WOW Improvement 2: Use formatted string for readability
            string logText = $"{logEntry.OpStart:HH:mm:ss} - {logEntry.Step}: {logEntry.LogDetails}";


            // Add new item with check box pre-checked (optional)
            var newItem = new RadListDataItem(logText, true);


            // WOW Improvement 3: Highlight new entries for visual cue
            newItem.ForeColor = Color.Green; // Or any preferred color
            if (Listview_Log.InvokeRequired)
            {
                Listview_Log.Invoke((Action)(() =>
                {
                    Listview_Log.Items.Add(newItem);
                    Listview_Log.Items.Add(logText);
                }));
            }
            else
            {
                Listview_Log.Items.Add(newItem);
                Listview_Log.Items.Add(logText);
            }
        }

        public async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (Tuple<OperationType, string, string, string>)e.Argument;
            OperationType operation = args.Item1;
            string url = args.Item2;

            if (operation == OperationType.GetStructure)
            {
                // Get HTML structure using AngleSharp
                List<Tuple<string, string>> elementsAndClasses =
                    CodeProccessingHelper.ExtractElementsAndClassesAngelcharp(url);
                e.Result = elementsAndClasses; // Store result for RunWorkerCompleted
            }
            else if (operation == OperationType.ExtractContent)
            {
                string elementName = args.Item3;
                string className = args.Item4;


                // Extract content using CefSharp
                List<Tuple<string, string>> elementsAndClasses =
                    CodeProccessingHelper.ExtractElementsAndClassesAngelcharp(url);
            }
        }

        enum OperationType
        {
            GetStructure,
            ExtractContent
        }

        public class MyDataItem
        {
            public string Column1 { get; set; }

            public string Column2 { get; set; }

            public string Column3 { get; set; }
        }

        public class SoulLog
        {
            public string LogAddress { get; set; }

            public string LogDetails { get; set; }

            public DateTime OpStart { get; set; }

            public OperationStep Step { get; set; } // Using enum for better type safety
        }

        public enum OperationStep
        {
            Start,
            Working,
            Finished
        }

        private void barLargeButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
        }


        // This event is generated by Data Source Configuration Wizard
        void unboundSource2_ValueNeeded(object sender, DevExpress.Data.UnboundSourceValueNeededEventArgs e)
        {
            // Handle this event to obtain data from your data source
            // e.Value = something /* TODO: Assign the real data here.*/
        }


        // This event is generated by Data Source Configuration Wizard
        void unboundSource2_ValuePushed(object sender, DevExpress.Data.UnboundSourceValuePushedEventArgs e)
        {
            // Handle this event to save modified data back to your data source
            // something = e.Value; /* TODO: Propagate the value into the storage.*/
        }

        private void GridLookUpEdit_Converstions_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //this.gridView20.ActiveFilterString = "[ConversationID] = 1";
                this.gridView20.ActiveFilterString = "[ConversationID] = \'" +
                                                     GridLookUpEdit_Converstions.EditValue.ToString() + "\'";
                MessageBox.Show(GridLookUpEdit_Converstions.EditValue.ToString());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void GridLookUpEdit_Agent_EditValueChanged(object sender, EventArgs e)
        {
            int agentId = (int)GridLookUpEdit_Agent.EditValue;

            AgentModel agentData = soulStudioService.GetAgentDataByAgentId(agentId);

            string apiKeyId = agentData.AgentApiID;
            GridLookUpEdit_API.EditValue = apiKeyId;
            GridLookUpEdit_Model.EditValue = agentData.AgentModelID;

            try
            {
                //this.gridView20.ActiveFilterString = "[ConversationID] = 1";
                this.gridLookUpEdit6View.ActiveFilterString = "[AgentID] = \'" +
                                                              GridLookUpEdit_Agent.EditValue.ToString() + "\'";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }


            //GridLookUpEdit_API.RefreshEditValue();
        }

        private void GridLookUpEdit_Model_EditValueChanged(object sender, EventArgs e)
        {
            string Modelname = GridLookUpEdit_Model.EditValue.ToString();

            SoulModelInfo modelinfoData = soulStudioService.GetModelinfotDataByname(Modelname);
            Invoke((MethodInvoker)delegate
            {
                int MaxOutput = modelinfoData.OutputTokenLimit;
                int trackbarstepvalue = MaxOutput / 100;

                int MaxInput = modelinfoData.InputTokenLimit;
                int Inputtrackbarstepvalue = MaxInput / 100;

                ////Output Tokens////
                Text_ModelMaxOutputTokens.Text = MaxOutput.ToString();
                Text_RemainModelMaxOutputTokens.Text = MaxOutput.ToString();

                Progressbar_OutputTokens.Maximum = MaxOutput;
                Progressbar_OutputTokens.Value1 = MaxOutput;

                SpinEditor_SelectedModelMaxOutputTokens.Maximum = MaxOutput;
                SpinEditor_SelectedModelMaxOutputTokens.Value = MaxOutput;

                TrackBar_SelectedModelMaxOutputTokens.Properties.Maximum = MaxOutput;
                TrackBar_SelectedModelMaxOutputTokens.Value = MaxOutput;

                TrackBar_SelectedModelMaxOutputTokens.Properties.TickFrequency = trackbarstepvalue;


                /// Output Tokens ///


                /// ///////////////////


                /// ////Input Tokens////
                Text_ModelMaxInputTokens.Text = MaxInput.ToString();
                Text_RemainModelMaxInputTokens.Text = MaxInput.ToString();
                Progressbar_InputTokens.Maximum = MaxInput;
                Progressbar_InputTokens.Value1 = MaxInput;

                SpinEditor_SelectedModelMaxInputTokens.Maximum = MaxInput;
                SpinEditor_SelectedModelMaxInputTokens.Value = MaxInput;

                TrackBar_SelectedModelMaxInputTokens.Properties.Maximum = MaxInput;
                TrackBar_SelectedModelMaxInputTokens.Value = MaxInput;

                TrackBar_SelectedModelMaxInputTokens.Properties.TickFrequency = Inputtrackbarstepvalue;


                /// Input Tokens ///
            });
        }

        private void TrackBar_SelectedModelMaxInputTokens_ValueChanged(object sender, EventArgs e)
        {
            int TrackValue = (int)TrackBar_SelectedModelMaxInputTokens.Value;
            int MaxValueFromTrackBar = (int)TrackBar_SelectedModelMaxInputTokens.Properties.Maximum;
            int RemainValue = MaxValueFromTrackBar - TrackValue;
            SpinEditor_SelectedModelMaxInputTokens.Value = TrackValue;
            Text_SelectedModelMaxInputTokens.Text = TrackValue.ToString();
            Text_RemainModelMaxInputTokens.Text = RemainValue.ToString();
            Progressbar_InputTokens.Value1 = TrackValue;
        }

        private void TrackBar_SelectedModelMaxOutputTokens_ValueChanged(object sender, EventArgs e)
        {
            int TrackValue = (int)TrackBar_SelectedModelMaxOutputTokens.Value;
            int MaxValueFromTrackBar = (int)TrackBar_SelectedModelMaxOutputTokens.Properties.Maximum;
            int RemainValue = MaxValueFromTrackBar - TrackValue;
            SpinEditor_SelectedModelMaxOutputTokens.Value = TrackValue;
            Text_SelectedModelMaxOutputTokens.Text = TrackValue.ToString();
            Text_RemainModelMaxOutputTokens.Text = RemainValue.ToString();
            Progressbar_OutputTokens.Value1 = TrackValue;
        }

        private void GroupControlO_PromptDesign_Prompt_Paint(object sender, PaintEventArgs e)
        {
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddComponent adc = new AddComponent();

            if (XtraDialog.Show(adc, "Add New Component", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                /*
                 * string login = myControl.Login;
                 * string password = myControl.Password;
                 */
            }
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoginUserControl myControl = new LoginUserControl();
            XtraDialog.Show(myControl, "Sign in", MessageBoxButtons.OKCancel);
        }

        private void GroupControlO_PromptDesign_Rules_CustomButtonClick(object sender,
            DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            AddComponent adc = new AddComponent();
            if (e.Button.Properties.Caption == "AddRule")
            {
                // Get ComponentTypeID or insert if not found
                int componentTypeID = soulStudioService.GetComponentTypeID("Rules");
                if (componentTypeID == -1)
                {
                    componentTypeID = soulStudioService.InsertSoulPromptType("Rules");
                }


                // Get CategoryID or insert if not found
                int categoryID = soulStudioService.GetCategoryID("General Category");
                if (categoryID == -1)
                {
                    categoryID = soulStudioService.InsertSoulPromptCategory("General Category");
                }


                adc.ComponentTypeNameSearchLookUpEdit.EditValue = componentTypeID;
                adc.CategoryNameLookUpEdit.EditValue = categoryID;

                if (XtraDialog.Show(adc, "Add New Component", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    adc.dataLayoutControl1.DataSource = null;
                    adc.dataLayoutControl1.DataMember = null;
                    var newPromptComponent = new SoulPromptComponents()
                    {
                        //ComponentID = Convert.ToInt32(adc.ComponentNameTextEdit.Text),
                        ComponentName = adc.ComponentNameTextEdit.Text,
                        ComponentValue = adc.ComponentValueTextEdit.Text,
                        ComponentTypeID = Convert.ToInt32(adc.ComponentTypeNameSearchLookUpEdit.EditValue),
                        Custom1 = adc.Custom1TextEdit.Text,
                        Custom2 = adc.Custom2TextEdit.Text,
                        isEnabled = adc.isEnabledCheckEdit.Checked,
                        isActive = adc.isActiveCheckEdit.Checked,
                        CategoryID = Convert.ToInt32(adc.CategoryNameLookUpEdit.EditValue)
                    };
                    soulStudioService.InsertSoulPromptComponent(newPromptComponent);
                }
            }
        }
    }

    public class LoginUserControl : XtraUserControl
    {
        public LoginUserControl()
        {
            LayoutControl lc = new LayoutControl();
            lc.Dock = DockStyle.Fill;
            TextEdit teLogin = new TextEdit();
            TextEdit tePassword = new TextEdit();
            CheckEdit ceKeep = new CheckEdit() { Text = "Keep me signed in" };
            lc.AddItem(String.Empty, teLogin).TextVisible = false;
            lc.AddItem(String.Empty, tePassword).TextVisible = false;
            lc.AddItem(String.Empty, ceKeep);
            this.Controls.Add(lc);
            this.Dock = DockStyle.Fill;
        }
    }
}