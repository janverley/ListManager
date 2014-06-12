namespace Lms.View.Infrastructure.RenamableControl
{
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Input;
  //using Microsoft.Practices.Prism.Commands;
  using System.Diagnostics;
  using ListManager.ViewModel;
  using Lms.ViewModel.Infrastructure.RenamableControl;
  using Microsoft.Practices.Prism.Commands;

  [TemplatePart(Type = typeof(Grid), Name = RenamableControl.GRID_NAME)]
  [TemplatePart(Type = typeof(TextBlock), Name = RenamableControl.TEXTBLOCK_DISPLAYTEXT_NAME)]
  [TemplatePart(Type = typeof(TextBox), Name = RenamableControl.TEXTBOX_EDITTEXT_NAME)]
  public class RenamableControl : Control
  {
    static RenamableControl()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(RenamableControl), new FrameworkPropertyMetadata(typeof(RenamableControl)));
    }

    public RenamableControl()
    {
      StartRenameCmd = new DelegateCommand(() =>
      {
        Debug.Assert(IsRenamable);
        StartEditing();
      }, () => 
      { 
        return IsRenamable; 
      });

      DataContextChanged += RenamableControl_DataContextChanged;
    }

    void RenamableControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      var vm = DataContext as RenamableNotificationObject;

      if ( vm != null )
        vm.StartRenameCmd = StartRenameCmd ;
    }

    #region Dependecy Properties
    public bool RenameOnClick
    {
      get { return (bool)GetValue(RenameOnClickProperty); }
      set { SetValue(RenameOnClickProperty, value); }
    }

    // Using a DependencyProperty as the backing store for RenameOnClick.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RenameOnClickProperty =
        DependencyProperty.Register("RenameOnClick", typeof(bool), typeof(RenamableControl), new UIPropertyMetadata(true));

    #endregion

    public ICommand StartRenameCmd
    {
      get;
      private set;
    }
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      gridContainer = Template.FindName(GRID_NAME, this) as Grid;
      if (gridContainer != null)
      {
        textBlockDisplayText = gridContainer.FindName(TEXTBLOCK_DISPLAYTEXT_NAME) as TextBlock;
        textBoxEditText = gridContainer.FindName(TEXTBOX_EDITTEXT_NAME) as TextBox;

        textBlockDisplayText.MouseLeftButtonDown += textBlockDisplayText_MouseLeftButtonDown;

      }
    }

    #region Private

    private void textBlockDisplayText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (RenameOnClick && IsRenamable)
      {
        StartEditing();
        e.Handled = true;
      }
    }

    private void textBoxEditText_LostFocus(object sender, RoutedEventArgs e)
    {
      textBoxEditText.Undo();

      StopEditing();
    }
    
    private void textBoxEditText_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Return)
      {
        var vm = DataContext as RenamableNotificationObject;
        if (vm.EditIsValid)
        {
          vm.AcceptNewName(vm.EditName);

          StopEditing();
          e.Handled = true;
        }
      }
      if (e.Key == Key.Escape)
      {
        textBoxEditText.Undo();

        StopEditing();
        e.Handled = true;
      }
    }

    private void StartEditing()
    {
      Debug.Assert(IsRenamable);

      var vm = DataContext as RenamableNotificationObject;
      vm.EditName = vm.DefaultEditText;

      textBlockDisplayText.Visibility = Visibility.Hidden;
      textBoxEditText.Visibility = Visibility.Visible;
      Keyboard.Focus(textBoxEditText);
      textBoxEditText.SelectAll();

      textBoxEditText.LostFocus += textBoxEditText_LostFocus;
      textBoxEditText.KeyDown += textBoxEditText_KeyDown;
    }

    private void StopEditing()
    {
      textBoxEditText.LostFocus -= textBoxEditText_LostFocus;
      textBoxEditText.KeyDown -= textBoxEditText_KeyDown;

      textBlockDisplayText.Visibility = Visibility.Visible;
      textBoxEditText.Visibility = Visibility.Hidden;
    }

    private bool IsRenamable
    {
      get
      {
        var vm = DataContext as RenamableNotificationObject;
        return vm.IsRenamable;
      }
    }

    private const string GRID_NAME = "PART_GridContainer";
    private const string TEXTBLOCK_DISPLAYTEXT_NAME = "PART_TbDisplayText";
    private const string TEXTBOX_EDITTEXT_NAME = "PART_TbEditText";
    private Grid gridContainer;
    private TextBlock textBlockDisplayText;
    private TextBox textBoxEditText;
    #endregion

  }
}

