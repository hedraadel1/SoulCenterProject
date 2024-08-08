//-----------------------------------------------------------------------
// <copyright file="ComponentsTypes.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace SoulCenterProject.SoulControls.ComponentsTypes
{
    public partial class ComponentsTypes : UserControl, INotifyPropertyChanged
    {
        private string _activeFilter;

        private BindingSource _dataSource;

        public ComponentsTypes()
        {
            InitializeComponent();
        }

        public event EventHandler ButtonGetCheckedValuesClicked;

        public event EventHandler ButtonGetSelectedRowClicked;

        public event PropertyChangedEventHandler PropertyChanged;

        private void ApplyFilter()
        {
            if (Gridview_Prompt != null && !string.IsNullOrEmpty(ActiveFilter))
            {
                Gridview_Prompt.ActiveFilterString = ActiveFilter;
            }
        }

        private void groupControl2_CustomButtonClick(
            object sender,
            DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "GetCheckedItems")
            {
                // Assuming you have an instance of ComponentsTypes user control in your main form named userControl1
                string checkedValuesString = GetCheckedValues(); // Call the new method
                MessageBox.Show("Checked Values:\n" + checkedValuesString);
            }
            else if (e.Button.Properties.Caption == "GetSelectedValue")
            {
                // Assuming userControl1 has the Gridview_Prompt and GridLookUpEdit_PromptComponentPrompt controls
                string selectedRowValue = GetSelectedRowValue(); // Call the new method
                if (!string.IsNullOrEmpty(selectedRowValue))
                {
                    MessageBox.Show("Selected Row Value: " + selectedRowValue);
                }
                else
                {
                    MessageBox.Show("No row selected in the grid.");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetCheckedValues()
        {
            StringBuilder checkedValuesString = new StringBuilder();

            if (CheckedValues != null) // Check if any user control is used
            {
                foreach (object checkedItem in CheckedValues)
                {
                    // Access ValueMember using reflection (assuming you have a ValueMember property)
                    var valueMemberProperty = checkedItem.GetType().GetProperty("ValueMember");
                    if (valueMemberProperty != null)
                    {
                        object value = valueMemberProperty.GetValue(checkedItem);
                        checkedValuesString.Append(value.ToString() + ", ");
                    }
                }


                // Remove the trailing comma and space if any values were appended
                if (checkedValuesString.Length > 0)
                {
                    checkedValuesString.Remove(checkedValuesString.Length - 2, 2);
                }

                checkedValuesString.Append(string.Empty);
                MessageBox.Show("Checked Values:\n" + checkedValuesString.ToString());
            }
            else
            {
                checkedValuesString.Append(string.Empty);
                MessageBox.Show("No user control with checked items found.");
            }

            return checkedValuesString.ToString();
        }

        public string GetSelectedRowValue()
        {
            string selectedRowValue = null;

            int[] selectedRows = Gridview_Prompt.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                if (rowHandle >= 0)
                {
                    var cellValue = Gridview_Prompt.GetRowCellValue(rowHandle, "ComponentID");
                    selectedRowValue = cellValue.ToString();
                }
                else
                {
                    selectedRowValue = string.Empty;
                }
            }

            return selectedRowValue;
        }

        public string ActiveFilter
        {
            get { return _activeFilter; }
            set
            {
                _activeFilter = value;
                OnPropertyChanged(nameof(ActiveFilter));
                ApplyFilter();
            }
        }

        public List<object> CheckedValues
        {
            get { return checkedComboBoxEdit2.EditValue as List<object>; }
        }

        public BindingSource DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                OnPropertyChanged(nameof(DataSource));
            }
        }

        public string DisplayMember
        {
            get { return GridLookUpEdit_PromptComponentPrompt.Properties.DisplayMember; }
        }

        public object SelectedValue
        {
            get { return GridLookUpEdit_PromptComponentPrompt.EditValue; }
        }
    }
}