﻿using System;
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
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
            if (errors.Length>0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
        }
       
    }
}
