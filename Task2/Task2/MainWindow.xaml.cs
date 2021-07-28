using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        int addEdit;
        public MainWindow()
        {
            client.BaseAddress = new Uri("http://localhost:1202/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
            btnEdit.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            clear();
            this.getContact(txtContact.Text);
        }
        private async void getContact(string name)
        {
            try
            {
                var response = await client.GetStringAsync("contact?name=" + name);
                var contact = JsonConvert.DeserializeObject<List<Contact>>(response);
                txtFirstName.Text = contact[0].FirstName;
                txtLastName.Text = contact[0].LastName;
                dateDOB.SelectedDate = contact[0].Dob;
                txtEmail.Text = contact[0].Email;
                txtPhone.Text = contact[0].Phone;
                btnEdit.IsEnabled = true;
                btnAdd.IsEnabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            clear();
            addEdit = 0;
            btnEdit.IsEnabled = false;
            btnAdd.IsEnabled = false;
            txtFirstName.IsReadOnly = false;
            txtLastName.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            txtPhone.IsReadOnly = false;
            btnSave.IsEnabled = true;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("First name and Last name are mandatory");
                if(string.IsNullOrWhiteSpace(txtFirstName.Text))
                    txtFirstName.BorderBrush = new SolidColorBrush(Colors.Red);
                else
                    txtFirstName.BorderBrush = new SolidColorBrush(Colors.LightGray);
                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                    txtLastName.BorderBrush = new SolidColorBrush(Colors.Red);
                else
                    txtLastName.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                Contact contact = new Contact()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Dob = dateDOB.SelectedDate,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text
                };
                if (addEdit == 0)
                    this.saveContact(contact);
                else
                    this.updateContact(contact);
                clear();
            }
        }
        private async void saveContact(Contact conact)
        {
            try
            {
                await client.PostAsJsonAsync("contact", conact);
                MessageBox.Show("Contact saved successffully");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void updateContact(Contact conact)
        {
            try
            {
                await client.PutAsJsonAsync("contact", conact);
                MessageBox.Show("Contact updated successffully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            addEdit = 1;
            btnEdit.IsEnabled = false;
            btnAdd.IsEnabled = true;
            txtLastName.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            txtPhone.IsReadOnly = false;
            btnSave.IsEnabled = true;
        }
        private void clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dateDOB.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtFirstName.IsReadOnly = true;
            txtLastName.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            txtPhone.IsReadOnly = true;
            btnSave.IsEnabled = false;
            btnAdd.IsEnabled = true;
            btnEdit.IsEnabled = false;
        }
    }
}
