using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using SoulCenterProject.Helpers.CodeGeneration;
using SoulCenterProject.Models.Soul_Models;
using SoulCenterProject.Modules.Ai.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.Utils;

namespace SoulCenterProject.Modules.Ai.Views
{
    public partial class AiMessage : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ConversationManagerService conv;
        public string connectionString =
           "Server=178.18.251.168;Database=newgyral_erpnew;Uid=newgyral_erpnew;Pwd=Mm102030@@@;";
        public AiMessage()
        {

            InitializeComponent();
            conv = new ConversationManagerService(connectionString);
            sqlDataSource1.FillAsync();
            sqlDataSource3.FillAsync();

        }
        public void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }


        private void tileView1_Click(object sender, EventArgs e)
        {

        }

        private void AiMessage_Load(object sender, EventArgs e)
        {

            //Display currently selected rows in a separate form
            SimpleButton b = new SimpleButton { Parent = gridControl1.Parent, Text = "Obtain selected rows", Dock = DockStyle.Top };
            b.Click += (s, sa) =>
            {
                BindingList<Soul_Message> selectedRows = new BindingList<Soul_Message>();

                foreach (int rowHandle in gridView1.GetSelectedRows())
                    selectedRows.Add(gridView1.GetRow(rowHandle) as Soul_Message);

                XtraForm frm = new XtraForm { StartPosition = FormStartPosition.CenterParent };
                GridView frmGridView = new GridView();
                GridControl frmGrid = new GridControl
                {
                    MainView = frmGridView,
                    Parent = frm,
                    DataSource = selectedRows,
                    Dock = DockStyle.Fill
                };
                frm.ShowDialog(gridControl1.FindForm());
            };

        }

        private void barButtonItem_Left_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl1.RightToLeft = RightToLeft.No;

        }

        private void barButtonItem_Right_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl1.RightToLeft = RightToLeft.Yes;
        }

        private int SelectedMessageID;
        private void TileView1_Click_1(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            MouseEventArgs mouseArgs = (e as MouseEventArgs);
            TileViewHitInfo hitInfo = tileView1.CalcHitInfo(mouseArgs.Location);
            object SelectedValueMessageID = tileView1.GetRowCellValue(hitInfo.RowHandle, "MessageID");
            object SelectedValue = tileView1.GetRowCellValue(hitInfo.RowHandle, "MessageContent");
            SelectedMessageID = Convert.ToInt32(SelectedValueMessageID);
            Textbox_Columnname.EditValue = SelectedMessageID;
            switch (me.Button)
            {

                case MouseButtons.Left:
                    if (hitInfo.InItem)
                    {
                        MessageBox.Show(SelectedValue.ToString());
                    }
                    break;

                case MouseButtons.Right:
                    MessageBox.Show(SelectedValueMessageID.ToString());

                    break;
            }
        }

        private async void barButtonItem_SendToHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            await ProcessSelectedMessages(MessageAction.SendToHistory);
        }

        private async void barButtonItem_DontSendToHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            await ProcessSelectedMessages(MessageAction.DontSendToHistory);
        }

        private async void barButtonItem_Pin_ItemClick(object sender, ItemClickEventArgs e)
        {
            await ProcessSelectedMessages(MessageAction.Pin);
        }

        private async void barButtonItem_DontPin_ItemClick(object sender, ItemClickEventArgs e)
        {
            await ProcessSelectedMessages(MessageAction.Unpin);
        }
        private enum MessageAction
        {
            SendToHistory,
            DontSendToHistory,
            Pin,
            Unpin
        }
        private async Task ProcessSelectedMessages(MessageAction action)
        {
            try
            {
                int[] selectedIDs = GetSelectedMessageIDs();

                if (selectedIDs.Length == 0)
                {
                    MessageBox.Show("No messages selected.");
                    return;
                }

                // Get confirmation message based on the action
                string confirmationMessage = GetConfirmationMessage(action);

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to {confirmationMessage} the selected messages?",
                    $"Confirm Action",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    foreach (int messageID in selectedIDs)
                    {
                        // Perform the action based on the selected enum value
                        switch (action)
                        {
                            case MessageAction.SendToHistory:
                                await conv.ChangeSendToAiHistoryAsync(messageID, true);
                                break;
                            case MessageAction.DontSendToHistory:
                                await conv.ChangeSendToAiHistoryAsync(messageID, false);
                                break;
                            case MessageAction.Pin:
                                await conv.ChangePinTheMessageAsync(messageID, true);
                                break;
                            case MessageAction.Unpin:
                                await conv.ChangePinTheMessageAsync(messageID, false);
                                break;
                        }
                    }

                    // Refresh the grid view and show a success message
                    gridView1.RefreshData();
                    MessageBox.Show("Action completed successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during processing: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper function to get confirmation messages based on action
        private string GetConfirmationMessage(MessageAction action)
        {
            switch (action)
            {
                case MessageAction.SendToHistory:
                    return "send to history";
                case MessageAction.DontSendToHistory:
                    return "remove from history";
                case MessageAction.Pin:
                    return "pin";
                case MessageAction.Unpin:
                    return "unpin";
                default:
                    return "perform the action on"; // Default message
            }
        }
        private void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private async void barButtonItem_Delete_ItemClick(object sender, ItemClickEventArgs e)
        {

            //  رسالة التأكيد
            DialogResult result = MessageBox.Show(SelectedMessageID.ToString() + "الرقم التعريفيف : " + "هل أنت متأكد من حذف هذه الرسالة؟", "تأكيد الحذف", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            //  التأكد من اختيار المستخدم لـ "موافق"
            if (result == DialogResult.OK)
            {
                try
                {
                    //  افترضنا هنا إنّ عندك  `messageId`  متخزّن في متغيّر
                    int messageIdToDelete = Convert.ToInt32(SelectedMessageID); //  جيب قيمة  `messageId`  من المكان المناسب

                    //  استدعاء  `DeleteMessageAsync`  لحذف الرسالة
                    await conv.DeleteMessageAsync(messageIdToDelete);

                    //  إجراءات إضافية بعد حذف الرسالة (مثلاً: تحديث واجهة المستخدم)
                    MessageBox.Show("تمّ حذف الرسالة بنجاح!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ أثناء حذف الرسالة: {ex.Message}");
                }
            }
        }

        private void barButtonItem_gridview_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl1.MainView = gridView1;
        }

        private void barButtonItem_winExplorerView_ItemClick(object sender, ItemClickEventArgs e)
        {
            DisplayColumnValues("MessageSenderName");
        }

        private void barButtonItem_layoutView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        private int[] GetSelectedMessageIDs()
        {
            try
            {
                int[] selectedRowHandles = gridView1.GetSelectedRows();
                int[] selectedMessageIDs = new int[selectedRowHandles.Length]; //  مصفوفة جديدة بنفس حجم الصفوف المحددة

                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                    {
                        if (int.TryParse(gridView1.GetRowCellValue(selectedRowHandle, "MessageID")?.ToString(), out int messageID))
                        {
                            selectedMessageIDs[i] = messageID;  //  حط  قيمة  الـ  MessageID  في  المصفوفة  الجديدة


                        }

                    }
                }

                return selectedMessageIDs;  //  رجع  المصفوفة  اللي  فيها  كل  الـ  MessageIDs
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new int[0];  //  رجع  مصفوفة  فاضية  في  حالة  وجود  خطأ
            }
        }

        private void DisplaySelectedMessageIDs()
        {
            try
            {
                int[] selectedIDs = GetSelectedMessageIDs();

                //  التأكد من وجود عناصر في المصفوفة قبل عرضها
                if (selectedIDs.Length > 0)
                {
                    foreach (int messageID in selectedIDs)
                    {
                        MessageBox.Show("Message ID: " + messageID);
                    }
                }
                else
                {
                    MessageBox.Show("لم يتم تحديد أي رسائل.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message);
            }
        }


        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            DisplaySelectedMessageIDs();
        }

        private List<object> GetColumnValues(string columnName)
        {
            try
            {
                List<object> columnValues = new List<object>();
                int[] selectedRowHandles = gridView1.GetSelectedRows();

                foreach (int selectedRowHandle in selectedRowHandles)
                {
                    if (selectedRowHandle >= 0)
                    {
                        object cellValue = gridView1.GetRowCellValue(selectedRowHandle, columnName);
                        columnValues.Add(cellValue);
                    }
                }

                return columnValues;
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message);
                return new List<object>();
            }
        }

        private void DisplayColumnValues(string columnName)
        {
            try
            {
                List<object> values = GetColumnValues(columnName);

                if (values.Count > 0)
                {
                    foreach (object value in values)
                    {
                        MessageBox.Show(value?.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("لم يتم تحديد أي صفوف، أو العمود فارغ.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message);
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            ////  الحصول على  Row  المُحدد
            //int rowHandle = gridView1.FocusedRowHandle;

            ////  التعامل مع البيانات في الـ  Row
            //var product = gridView1.GetRow(rowHandle) as Soul_Message; //  افترضنا إنّ  Grid  بيرجع  Product  objects

            //if (product != null)
            //{
            //    MessageBox.Show(product.MessageSenderName);
            //}
        }

        private void gridControl1_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            //gridView1.SetFocusedRowCellValue("Value", 999);
            //MessageBox.Show("button clicked");
        }

        private void r(object sender, EventArgs e)
        {

        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //  الحصول على  Row  المُحدد
            int rowHandle = gridView1.FocusedRowHandle;
            if (e.Button.Tag == "OKOK")
            {
                string xx = gridView1.GetRowCellValue(rowHandle, "MessageID").ToString();

                MessageBox.Show("clicked OKOK : " + xx);
            }
            if (e.Button.Tag == "ClearClear")
            {
                string xx = gridView1.GetRowCellValue(rowHandle, "MessageID").ToString();

                MessageBox.Show("clicked ClearClear: " + xx);
            }

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void CopyColumnToClipboard(DevExpress.XtraGrid.Columns.GridColumn column)
        {
            try
            {
                GridView view = gridView1; // Assuming your GridView is named gridView1

                if (view.SelectedRowsCount == 0)
                {
                    MessageBox.Show("No rows selected.");
                    return;
                }

                StringBuilder clipboardText = new StringBuilder();

                // Get selected rows (sorted by handle to maintain order)
                int[] selectedRowHandles = view.GetSelectedRows().OrderBy(r => r).ToArray();

                foreach (int rowHandle in selectedRowHandles)
                {
                    // Get the cell value from the specified column
                    object cellValue = view.GetRowCellValue(rowHandle, column);
                    clipboardText.AppendLine(cellValue?.ToString()); // Append value and new line
                }

                Clipboard.SetText(clipboardText.ToString());
                MessageBox.Show($"Column '{column.FieldName}' copied to clipboard!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during copying: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopySelectedRowsToClipboard()
        {
            try
            {
                GridView view = gridView1; // Assuming your GridView is named gridView1

                if (view.SelectedRowsCount == 0)
                {
                    MessageBox.Show("No rows selected.");
                    return;
                }

                StringBuilder clipboardText = new StringBuilder();

                // Get selected rows (sorted by handle to maintain order)
                int[] selectedRowHandles = view.GetSelectedRows().OrderBy(r => r).ToArray();

                foreach (int rowHandle in selectedRowHandles)
                {
                    // Loop through visible columns
                    foreach (DevExpress.XtraGrid.Columns.GridColumn column in view.VisibleColumns)
                    {
                        clipboardText.Append(view.GetRowCellDisplayText(rowHandle, column));
                        clipboardText.Append(view.GetRowCellValue(rowHandle, column));
                        clipboardText.Append("\t"); // Separate column values with a tab
                        clipboardText.Append("|"); // Separate column values with a tab
                    }

                    clipboardText.AppendLine(); // Add a new line after each row
                    clipboardText.Append("---------------------"); // Separate column values with a tab
                    clipboardText.AppendLine(); // Add a new line after each row
                    clipboardText.Append("---------- New Message - | - New Message ----------"); // Separate column values with a tab
                    clipboardText.AppendLine(); // Add a new line after each row
                    clipboardText.Append("---------------------"); // Separate column values with a tab

                    clipboardText.AppendLine(); // Add a new line after each row

                }

                Clipboard.SetText(clipboardText.ToString());
                MessageBox.Show("Selected rows copied to clipboard!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during copying: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItem_Copy_ItemClick(object sender, ItemClickEventArgs e)
        {
            CopySelectedRowsToClipboard();
        }

        private void barButtonItemC_Copy_Columns_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraGrid.Columns.GridColumn columnToCopy = gridView1.Columns[Textbox_Columnname.EditValue.ToString()]; // Replace "YourColumnName"

            if (columnToCopy == null)
            {
                MessageBox.Show("Column 'YourColumnName' not found!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CopyColumnToClipboard(columnToCopy);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gridView1.FocusedRowHandle;

                string messagecontent = gridView1.GetRowCellValue(rowHandle, "MessageContent").ToString();
                GeneralRichText.Text = messagecontent;
                FlyoutPanel_Richtext.ShowPopup();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Button_CloseFlyout1_Click(object sender, EventArgs e)
        {
            
            FlyoutPanel_Richtext.HidePopup(false);
        }

        private void Button_CloseFlyout2_Click(object sender, EventArgs e)
        {
            FlyoutPanel_Richtext.HidePopup(true);
        }
    }
}