﻿using ListManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManager.View
{
  class PlaceHolder : Item
  {
    public PlaceHolder()
      :base("Click to add...",true)
    {
      CanDelete = false;
    }
  }
}
