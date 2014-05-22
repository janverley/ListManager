﻿using ListManager.ViewModel;
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

namespace ListManager
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      DataContext = new ViewModel.MainWindow();
    }

    private ViewModel.MainWindow ViewModel
    {
      get { return DataContext as ViewModel.MainWindow; }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      ViewModel.Items.Add(new Item("added", true));
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      ViewModel.Items.RemoveAt(1);
    }
  }
}
