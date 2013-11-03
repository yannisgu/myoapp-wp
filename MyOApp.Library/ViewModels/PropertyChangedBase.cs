  using System;
  using System.ComponentModel;
  using System.Runtime.CompilerServices;

namespace MyOApp.Library.ViewModels
  {
    /// <summary>
    /// Enable automatic RaisePropertyChanged notification generation
    /// </summary>
    public class MagicAttribute : Attribute { }

    /// <summary>
    /// Disable automatic RaisePropertyChanged notification generation 
    /// </summary>
    public class NoMagicAttribute : Attribute { }

    [Magic]
    public class PropertyChangedBase : INotifyPropertyChanged
    {
      protected virtual void RaisePropertyChanged([CallerMemberName] string property = "")
      {
        var e = PropertyChanged;
        if (e != null)
          e(this, new PropertyChangedEventArgs(property));
      }

      public event PropertyChangedEventHandler PropertyChanged;
    }
  }
