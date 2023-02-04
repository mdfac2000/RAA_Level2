using Microsoft.Win32;
using System;
using System.Windows;


namespace RAA_Level2
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MyForm : Window
    {
        public MyForm()
        {
            InitializeComponent();
        }

        OpenFileDialog openDialog;
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            openDialog = new OpenFileDialog();
            openDialog.Filter = "csv files (*.csv)|*.csv";
            openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openDialog.ShowDialog() == true)
            {
                txtBox1.Text = openDialog.SafeFileName;
            }
            else
            {
                txtBox1.Text = string.Empty;
            }

        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public string GetFileName()
        {
            return openDialog.FileName;
        }

        public bool GetRadioButtonGroup()
        {
            if (rdButton1.IsChecked == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetCheckBox1()
        {
            if (chkBox1.IsChecked == true)
            {
                return true;
            }
            return false;
        }

        public bool GetCheckBox2()
        {
            if (chkBox2.IsChecked == true)
            {
                return true;
            }
            return false;
        }



    }
}
