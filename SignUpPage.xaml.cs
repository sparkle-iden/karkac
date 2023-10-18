using System;
using System.Collections.Generic;
using System.Linq;
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

namespace karkac
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        private Service _currentService = new Service();
        public SignUpPage(Service SelectedService)
        {
            InitializeComponent();
            if (SelectedService != null)
                this._currentService = SelectedService;
            DataContext = _currentService;

            var _currentClient = Lopatkin_Model.GetContext().Client.ToList();
            ComboClient.ItemsSource = _currentClient;
        }

        private ClientService _currentClientService = new ClientService();
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int Duration = Convert.ToInt32(_currentService.Duration.ToString());
            StringBuilder errors = new StringBuilder();
            if (ComboClient.SelectedItem == null)
            {
                errors.AppendLine("Укажите ФИО клиента");
            }
            if (StartDate.Text == "")
            {
                errors.AppendLine("Укажите Дату услуги");
            }
            if (TBStart.Text=="")
            {
                errors.AppendLine("Укажите Время начала");
            }
            if (Duration<=240&&Duration>0)
            {
                errors.AppendLine("Длительность не может быть больше 240 и меньше 0");
            }
            if (errors.Length>0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _currentClientService.ClientID = ComboClient.SelectedIndex + 1;
            _currentClientService.ServiceID = _currentService.ID;
            
            _currentClientService.StartTime = Convert.ToDateTime(StartDate.Text + " " + TBStart.Text);
            if (_currentClientService.ID == 0)
            {
                Lopatkin_Model.GetContext().ClientService.Add(_currentClientService);
            }
            try
            {
                Lopatkin_Model.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TBStart_TextChanged(object sender, TextChangedEventArgs e)
        {

            string s = TBStart.Text;
            if (s.Length < 5 || !s.Contains(':'))
                TBEnd.Text = "";
            else
            {
                int Duration = Convert.ToInt32(_currentService.Duration.ToString());
                string[] start = s.Split(new char[] { ':' });
                int startHour = Convert.ToInt32(start[0].ToString()) * 60;
                int startMin = Convert.ToInt32(start[1].ToString());
                int sum = startHour + startMin + Duration;
                int EndHour = sum / 60;
                int EndMin = sum % 60;
                if (EndMin < 10)
                {
                    s = EndHour.ToString() + ":0" + EndMin.ToString();
                    TBEnd.Text = s;
                }
                else
                {
                    s = EndHour.ToString() + ":" + EndMin.ToString();
                    TBEnd.Text = s;
                }
                
            }
        }


      
    }
}
