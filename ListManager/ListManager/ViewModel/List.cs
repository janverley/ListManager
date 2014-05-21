using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManager.ViewModel
{
  class List : INotifyCollectionChanged, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
  }
}
