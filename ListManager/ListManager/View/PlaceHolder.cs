﻿using ListManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;

namespace ListManager.View
{
  class PlaceHolder : Item
  {
    public PlaceHolder(Action<string> OnAcceptNewName)
      : base("Click to add...", true)
    {
      CanDelete = false;
      Name.IsRenamable = true;
      Name.AcceptNewNameCmd = new DelegateCommand<string>(newName => OnAcceptNewName(newName));
    }
  }
}
