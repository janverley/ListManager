using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Lms.ViewModel.Infrastructure;
using Lms.ViewModelI.Infrastructure;

namespace ListManager.ViewModel
{
  class MainWindow : INotifyPropertyChanged
  {
    public MainWindow()
    {
      ManagedList = new MyManagedList();  
    }

    private IManagedList managedList;
    public IManagedList ManagedList
    {
      get { return managedList; }
      set
      {
        managedList = value;
        PropertyChanged(this, new PropertyChangedEventArgs("ManagedList"));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
