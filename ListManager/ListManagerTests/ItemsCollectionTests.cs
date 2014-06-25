using System;
using System.ComponentModel;
//using System.Linq;
using ListManager.ViewModel;
using Lms.ViewModelI.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListManagerTests
{
  [TestClass]
  public class ItemsCollectionTests
  {
    class MockItem : IItem
    {
      public bool CanDelete
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsCurrent
      {
        get
        {
          return true;
        }
        set
        {
          
        }
      }

      public bool IsDirty
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsFavorite
      {
        get { throw new NotImplementedException(); }
      }

      public string Name
      {
        get { throw new NotImplementedException(); }
      }

      public System.Windows.Input.ICommand SaveCommand
      {
        get { throw new NotImplementedException(); }
      }

      public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

      public int Subscriptions
      {
        get
        {
          return PropertyChanged == null ? 0 : PropertyChanged.GetInvocationList().Length;
        }

      }
    }

    class PartialMockItem : IItem
    {
      public PartialMockItem(bool isCurrent)
      {
        IsCurrent = isCurrent;
      }

      public bool CanDelete
      {
        get { throw new NotImplementedException(); }
      }

      private bool isCurrent;

      public bool IsCurrent
      {
        get { return isCurrent; }
        set
        {
          if (!Equals(isCurrent, value))
          {
            isCurrent = value;
            PropertyChanged(this, new PropertyChangedEventArgs("IsCurrent"));
          }
        }
      }

      public bool IsDirty
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsFavorite
      {
        get { throw new NotImplementedException(); }
      }

      public string Name
      {
        get { throw new NotImplementedException(); }
      }

      public System.Windows.Input.ICommand SaveCommand
      {
        get { throw new NotImplementedException(); }
      }

      public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged = delegate { };
    }

    [TestMethod]
    public void ItemCollectionCtorShouldSubscribe()
    {
      var x = new MockItem();

      Assert.AreEqual(0, x.Subscriptions);
      var target = new ItemCollection(new[] { x });
      Assert.AreEqual(1, x.Subscriptions);

    }

    [TestMethod]
    public void ClearingItemCollectionShouldUnsubscribe()
    {
      var x = new MockItem();
      var target = new ItemCollection(new[] { x });

      Assert.AreEqual(1, x.Subscriptions);
      target.Clear();
      Assert.AreEqual(0, x.Subscriptions);
    }

    [TestMethod]
    public void AddToItemCollectionShouldSubscribe()
    {
      var x = new MockItem();
      var target = new ItemCollection();

      Assert.AreEqual(0, x.Subscriptions);
      target.Add(x);
      Assert.AreEqual(1, x.Subscriptions);
    }

    [TestMethod]
    public void RemoveFromItemCollectionShouldUnsubscribe()
    {
      var x = new MockItem();
      var target = new ItemCollection(new[] { x });

      Assert.AreEqual(1, x.Subscriptions);
      target.Remove(x);
      Assert.AreEqual(0, x.Subscriptions);
    }

    [TestMethod]
    public void RemoveFromItemCollectionByIndexShouldUnsubscribe()
    {
      var x = new MockItem();
      var target = new ItemCollection(new[] { x });

      Assert.AreEqual(1, x.Subscriptions);
      target.RemoveAt(0);
      Assert.AreEqual(0, x.Subscriptions);
    }

    [TestMethod]
    public void ItemCollectionCtorShouldSubscribeAll()
    {
      var x1 = new MockItem();
      var x2 = new MockItem();

      Assert.AreEqual(0, x1.Subscriptions);
      Assert.AreEqual(0, x2.Subscriptions);
      var target = new ItemCollection(new[] { x1, x2 });
      Assert.AreEqual(1, x1.Subscriptions);
      Assert.AreEqual(1, x2.Subscriptions);

    }


    [TestMethod]
    public void ClearingItemCollectionShouldUnsubscribeAll()
    {
      var x1 = new MockItem();
      var x2 = new MockItem();

      var target = new ItemCollection(new[] { x1, x2 });
      Assert.AreEqual(1, x1.Subscriptions);
      Assert.AreEqual(1, x2.Subscriptions);

      target.Clear();

      Assert.AreEqual(0, x1.Subscriptions);
      Assert.AreEqual(0, x2.Subscriptions);
    }

    [TestMethod]
    public void InsertingInItemCollectionShouldSubscribe()
    {
      var x1 = new MockItem();
      var x2 = new MockItem();

      var target = new ItemCollection(new[] { x1, x2 });
      Assert.AreEqual(1, x1.Subscriptions);
      Assert.AreEqual(1, x2.Subscriptions);

      var x3 = new MockItem();
      Assert.AreEqual(0, x3.Subscriptions);
      target.Insert(1, x3);

      Assert.AreEqual(1, x1.Subscriptions);
      Assert.AreEqual(1, x2.Subscriptions);
      Assert.AreEqual(1, x3.Subscriptions);
    }


    [TestMethod]
    public void PartialMockItemCtorTest()
    {
      var x = new PartialMockItem(false);
      Assert.AreEqual(false, x.IsCurrent);
      var x2 = new PartialMockItem(true);
      Assert.AreEqual(true, x2.IsCurrent);
    }

    [TestMethod]
    public void PartialMockItemPropertyChangedTest()
    {
      var eventRaised = false;
      var x = new PartialMockItem(false);

      x.PropertyChanged += (_, __) => { eventRaised = true; };

      Assert.AreEqual(false, eventRaised);

      x.IsCurrent = true;

      Assert.AreEqual(true, eventRaised);
    }

    [TestMethod]
    public void Making1ItemCurrentShouldResetTheOther()
    {
      var x1 = new PartialMockItem(true);
      var x2 = new PartialMockItem(false);
      var x3 = new PartialMockItem(false);

      var target = new ItemCollection(new []{x1,x2, x3});

      Assert.AreEqual(true, x1.IsCurrent);
      Assert.AreEqual(false, x2.IsCurrent);
      Assert.AreEqual(false, x3.IsCurrent);

      x2.IsCurrent = true;

      Assert.AreEqual(false, x1.IsCurrent);
      Assert.AreEqual(true, x2.IsCurrent);
      Assert.AreEqual(false, x3.IsCurrent);
    }

    [TestMethod]
    public void Making1ItemCurrentShouldResetTheOtherUsingAdd()
    {
      var x1 = new PartialMockItem(true);
      var x2 = new PartialMockItem(false);
      var x3 = new PartialMockItem(false);

      var target = new ItemCollection();
      target.Add(x1);
      target.Add(x2);
      target.Add(x3);

      Assert.AreEqual(true, x1.IsCurrent);
      Assert.AreEqual(false, x2.IsCurrent);
      Assert.AreEqual(false, x3.IsCurrent);

      x2.IsCurrent = true;

      Assert.AreEqual(false, x1.IsCurrent);
      Assert.AreEqual(true, x2.IsCurrent);
      Assert.AreEqual(false, x3.IsCurrent);
    }

    [TestMethod]
    public void AddingMaking1ItemCurrentShouldResetTheOtherUsingAdd()
    {
      var x1 = new PartialMockItem(true);
      var x2 = new PartialMockItem(false);
      var x3 = new PartialMockItem(false);

      var target = new ItemCollection();
      target.Add(x1);
      target.Add(x2);
      target.Add(x3);

      Assert.AreEqual(true, x1.IsCurrent);
      Assert.AreEqual(false, x2.IsCurrent);
      Assert.AreEqual(false, x3.IsCurrent);

      x2.IsCurrent = true;

      Assert.AreEqual(false, x1.IsCurrent);
      Assert.AreEqual(true, x2.IsCurrent);
      Assert.AreEqual(false, x3.IsCurrent);
    }

    [TestMethod]
    public void RemovedItemsShouldNotReceiceNotifications()
    {
      var x1 = new PartialMockItem(true);
      var x2 = new PartialMockItem(false);

      var target = new ItemCollection();
      target.Add(x1);
      target.Add(x2);

      target.Remove(x1);

      Assert.AreEqual(true, x1.IsCurrent);
      Assert.AreEqual(false, x2.IsCurrent);

      x2.IsCurrent = true;

      Assert.AreEqual(true, x1.IsCurrent); // <<<<
      Assert.AreEqual(true, x2.IsCurrent);
    }

    [TestMethod]
    public void AddingACurrentItemShouldResetTheOthers()
    {
      var x1 = new PartialMockItem(true);
      var x2 = new PartialMockItem(false);

      var target = new ItemCollection();
      target.Add(x1);
      target.Add(x2);


      Assert.AreEqual(true, x1.IsCurrent);
      Assert.AreEqual(false, x2.IsCurrent);

      var x3 = new PartialMockItem(true);
      Assert.AreEqual(true, x3.IsCurrent);

      target.Add(x3);

      Assert.AreEqual(false, x1.IsCurrent); // <<<<
      Assert.AreEqual(false, x2.IsCurrent);
      Assert.AreEqual(true, x3.IsCurrent);
    }

    [TestMethod]
    public void AddingANotCurrentItemShouldNotResetTheOthers()
    {
      var x1 = new PartialMockItem(true);
      var x2 = new PartialMockItem(true);

      var target = new ItemCollection();
      target.Add(x1);
      target.Add(x2);

      Assert.AreEqual(false, x1.IsCurrent);
      Assert.AreEqual(true, x2.IsCurrent);

      var x3 = new PartialMockItem(false);
      Assert.AreEqual(false, x3.IsCurrent);

      target.Add(x3);

      Assert.AreEqual(false, x1.IsCurrent); // <<<<
      Assert.AreEqual(true, x2.IsCurrent);
      Assert.AreEqual(false, x3.IsCurrent);
    }
  }
}
