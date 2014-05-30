using Microsoft.Practices.Prism.Commands;
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

      renameItem.RenameObject.AcceptNewNameCmd = new DelegateCommand<string>((newName) =>
      {
        if (MessageBox
          .Show(string.Format("Rename requested!\n Accept?\nNew Name: {0}",newName), "Confirm", MessageBoxButton.YesNo)
          .Equals(MessageBoxResult.Yes))
        {
          renameItem.Name = newName;
          renameItem.RenameObject.DefaultEditText = newName;
        }
        });

      renameItem.RenameObject.Constraint = new MyConstraint();

      items = new ObservableCollection<Item>{
        new Item("Default", false){CanDelete = false},
        new Item("Item1", true),
        new Item("Save Me!", true, (item) => 
          {
            return MessageBox
              .Show(string.Format("Save requested!\nItem: {0} - {1}\n Accept?",item.Name, item.IsFavorite),"Confirm",MessageBoxButton.YesNo)
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
      set { items = value;
      PropertyChanged(this, new PropertyChangedEventArgs("Items"));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
  }
}
