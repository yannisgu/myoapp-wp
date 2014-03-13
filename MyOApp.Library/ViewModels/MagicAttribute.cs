  using System;
  using System.ComponentModel;
  using System.Runtime.CompilerServices;
  using Cirrious.MvvmCross.ViewModels;

namespace MyOApp.Library.ViewModels
{
    /// <summary>
    /// Enable automatic RaisePropertyChanged notification generation
    /// </summary>
    public class MagicAttribute : Attribute
    {
    }

    /// <summary>
    /// Disable automatic RaisePropertyChanged notification generation 
    /// </summary>
    public class NoMagicAttribute : Attribute
    {
    }

}
