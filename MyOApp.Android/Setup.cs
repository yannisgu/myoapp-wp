using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using MyOApp.Library;
using PluginLoader = Cirrious.MvvmCross.Plugins.Json.PluginLoader;

namespace MyOApp.Android
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Platform.DataAccess = new DataAccess();
            return new Library.App();
        }

        protected override List<Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = base.ValueConverterAssemblies;
                return toReturn;
            }
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxNavigationSerializer CreateNavigationSerializer()
        {
            PluginLoader.Instance.EnsureLoaded();
            return new MvxJsonNavigationSerializer();
        }
    }
}