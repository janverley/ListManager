namespace Lms.ViewModelI.Infrastructure
{
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.ComponentModel;

  public class ItemCollection : ObservableCollection<IItem>
  {
    public ItemCollection()
      : base()
    {
    }

    public ItemCollection(IEnumerable<IItem> items)
      : base(items)
    {
      foreach (var item in items)
      {
        item.PropertyChanged += item_PropertyChanged;
      }
    }

    void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName.Equals("IsCurrent"))
      {
        var item = sender as IItem;
        if (item.IsCurrent)
        {
          foreach (var other in Items)
          {
            if (!ReferenceEquals(other, item))
            {
              other.IsCurrent = false;
            }
          }
        }
      }
    }

    protected override void ClearItems()
    {
      foreach (var item in Items)
      {
        item.PropertyChanged -= item_PropertyChanged;
      }
      base.ClearItems();

    }
    protected override void InsertItem(int index, IItem item)
    {
      base.InsertItem(index, item);
      item.PropertyChanged += item_PropertyChanged;

      item_PropertyChanged(item, new PropertyChangedEventArgs("IsCurrent"));
    }

    protected override void RemoveItem(int index)
    {
      Items[index].PropertyChanged -= item_PropertyChanged;
      base.RemoveItem(index);
    }

    protected override void SetItem(int index, IItem item)
    {
      Items[index].PropertyChanged -= item_PropertyChanged;
      base.SetItem(index, item);
      item.PropertyChanged += item_PropertyChanged;
    }
  }

}
