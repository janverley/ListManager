﻿using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace ListManager.ViewModel
{
  class MainWindow : INotifyPropertyChanged
  {
    public MainWindow()
    {
      var renameItem = new Item("Rename Me!", true);

      renameItem.Name.AcceptNewNameCmd = new DelegateCommand<string>((newName) =>
      {
        if (MessageBox
          .Show(string.Format("Rename requested!\n Accept?\nNew Name: {0}",newName), "Confirm", MessageBoxButton.YesNo)
          .Equals(MessageBoxResult.Yes))
        {
          renameItem.Name.Name = newName;
          renameItem.Name.DefaultEditText = newName;
        }
        });

      items = new ObservableCollection<Item>{
        new Item("Default", false){CanDelete = false},
        new Item("Item1", true),
        new Item("Save Me!", true, () => 
          {
            return MessageBox
              .Show("Save requested!\n Accept?","Confirm",MessageBoxButton.YesNo)
              .Equals(MessageBoxResult.Yes);
          }),
        new Item("UnRenamable", false),
        renameItem,

        new Item("Item1", true),
      };
    }

    private ObservableCollection<Item> items;

    public ObservableCollection<Item> Items
    {
      get { return items; }
      private set { items = value; }
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
