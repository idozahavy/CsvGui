﻿using Csv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvGui
{
    public partial class FormInfo : Form
    {
        private CsvForm form = null;
        public FormInfo(CsvForm form)
        {
            InitializeComponent();
            this.form = form;
            RefreshInfo();
        }

        private void RefreshInfo()
        {
            FilenameTextBox.Text = form.name;
            RowCountTextBox.Text = form.rows.Count.ToString();
            ColumnHeaderComboBox.Items.Clear();
            ColumnIndexComboBox.Items.Clear();
            if (form.headRow != null)
            {
                foreach (CsvItem item in form.headRow)
                {
                    ColumnHeaderComboBox.Items.Add(item);
                    ColumnIndexComboBox.Items.Add(item.index);
                }
            }
        }

        private void ColumnHeaderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CsvItem itemColumnHeader = (CsvItem)ColumnHeaderComboBox.SelectedItem;
            if (ColumnIndexComboBox.SelectedItem is null || itemColumnHeader.index != (int)ColumnIndexComboBox.SelectedItem)
            {
                ColumnIndexComboBox.SelectedItem = itemColumnHeader.index;
            }
        }

        private void ColumnIndexComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int columnIndex = (int)ColumnIndexComboBox.SelectedItem;
            if (ColumnHeaderComboBox.SelectedItem is null || columnIndex != ((CsvItem)ColumnHeaderComboBox.SelectedItem).index)
            {
                ColumnHeaderComboBox.SelectedItem = form.headRow.GetItem(columnIndex);
            }
            RefreshColumnUniqueValues(columnIndex);
        }

        private void RefreshColumnUniqueValues(int columnIndex)
        {
            ColumnHeaderValuesPanel.Visible = true;
            ColumnHeaderValuesListView.Items.Clear();
            List<CsvItem> uniqueValueItems = form.GetUniqueValueColumnItems(columnIndex);
            foreach (CsvItem item in uniqueValueItems)
            {
                ColumnHeaderValuesListView.Items.Add(item.ToString());
            }
        }
    }
}