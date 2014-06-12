using ListManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;

namespace ListManager.ViewModel
{
  class PlaceHolder : Item
  {
    public PlaceHolder(Func<string, bool> onAcceptNewName)
      : base("Click to add...", "NewName", true, (_)=>true,onAcceptNewName)
    {
      CanDelete = false;
      RenameObject.IsRenamable = true;
    }
  }
}
