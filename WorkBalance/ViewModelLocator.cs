/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:WorkBalance"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using WorkBalance.ViewModel;
using System;
using System.Linq;
using WorkBalance.Repositories;
using System.Reflection;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using WorkBalance.Utilities;

namespace WorkBalance
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator: IDisposable
    {
        private const string c_Storage = "WorkBalance.db4o";
        private CompositionContainer m_Container;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var catalog = new DesignTimeCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            m_Container = new CompositionContainer(catalog);                

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time services and viewmodels
            }
            else
            {
                m_Container.ComposeExportedValue<Db4objects.Db4o.IObjectContainer>(Db4objects.Db4o.Db4oFactory.OpenFile(c_Storage));
            }
            m_Container.ComposeExportedValue<IMessenger>(new Messenger());
        }

        public void Dispose()
        {
            if (m_Container != null)
            {
                m_Container.Dispose();
            }
        }

        /// <summary>
        /// Gets the Main property which defines the main viewmodel.
        /// </summary>
        public MainViewModel Main { get { return m_Container.GetExportedValue<MainViewModel>(); } }

        public ActivityInventoryViewModel ActivityInventory { get { return m_Container.GetExportedValue<ActivityInventoryViewModel>(); } }
    }
}