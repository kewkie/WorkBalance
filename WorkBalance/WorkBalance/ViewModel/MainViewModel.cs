using GalaSoft.MvvmLight;
using System;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics.Contracts;
using System.ComponentModel;

namespace WorkBalance.ViewModel
{


    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        IMessenger m_Messenger;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMessenger messenger)
        {
            Contract.Requires(messenger != null);

            m_Messenger = messenger;
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            Timer = new WorkBalance.Timer();
            // Translate state change notification and propagate it to the user interface
            Timer.PropertyChanged += new PropertyChangedEventHandler(
                CreatePropertyChangedHandler("State", s => RaisePropertyChanged("ToggleTimerActionName")));
            m_ToggleTimerCommand = new RelayCommand(Timer.ToggleTimer);
            m_CloseCommand = new RelayCommand(() => App.Current.Shutdown());            
        }

        public Timer Timer { get; private set; }

        public string ToggleTimerActionName
        {
            get 
            {
                string result = null;
                switch (Timer.State)
                {
                    case TimerState.Ready:
                        result = "Start Sprint";
                        break;
                    case TimerState.Sprint:
                        result = "Abort Sprint";
                        break;
                    case TimerState.Break:
                        result = "Abort Break";
                        break;
                    case TimerState.BreakOverrun:
                        result = "Stop Break";
                        break;
                }
                return result;
            }
        }

        private Action<object, PropertyChangedEventArgs> CreatePropertyChangedHandler(string property, Action<object> handler)
        {
            return new Action<object,PropertyChangedEventArgs>(delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "State")
                {
                    handler(sender);
                }
            });
        }

        private RelayCommand m_ToggleTimerCommand;
        public ICommand ToggleTimerCommand { get { return m_ToggleTimerCommand; } }

        private RelayCommand m_CloseCommand;
        public ICommand CloseCommand { get { return m_CloseCommand; } }
    }
}