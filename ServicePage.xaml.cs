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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            //добавляем строки
            // загрузить в список из бд
            var currentServices = Lopatkin_Model.GetContext().Service.ToList();
            // связать с нашим листвью
            ServiceListView.ItemsSource = currentServices;
            //добавили строки
            ComboType.SelectedIndex = 0;
            UpdateService();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateService();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateService();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateService();
        }
        private void UpdateService()
        {
            //берем из бд данные таблицы Сервис
            var currentServices = Lopatkin_Model.GetContext().Service.ToList();
            //прописываем фильтрацию по условию задания
            if (ComboType.SelectedIndex== 0)
            {
                currentServices = currentServices.Where(p=> (p.Discount >= 0 && p.Discount <= 100)).ToList();
            }

            if (ComboType.SelectedIndex == 1)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 0 && p.Discount < 5)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 5 && p.Discount < 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 15 && p.Discount < 30)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 30 && p.Discount < 70)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 70 && p.Discount < 100)).ToList();
            }
            //реализуем поиск данных в листвью при вводе текста в окно поиска
            currentServices = currentServices.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            //реализуем сортировку по убыванию
            if (RButtonDown.IsChecked.Value)
            {
                currentServices = currentServices.OrderByDescending(p => p.Cost).ToList();

            }
            //реализуем сортировку по возрастанию 

            if (RButtonUp.IsChecked.Value)
            {
                currentServices = currentServices.OrderBy(p => p.Cost).ToList();
            }
            //отображаем итоги поиска/фильтрации/сортировки ServiceListview.ItemsSource - currentServices;
            ServiceListView.ItemsSource = currentServices;
        }
    }
    

}
