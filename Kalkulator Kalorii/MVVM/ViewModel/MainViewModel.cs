using Kalkulator_Kalorii.Core;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kalkulator_Kalorii.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private string _user;
        public string User { 
            get { 
                return _user; 
            } 
            set { 
                _user = value; 
                OnPropertyChanged();
            } 
        }

        private ComboBoxItem _profile;
        public ComboBoxItem Profile
        {
            get
            {
                return _profile;
            }
            set
            {
                _profile = value;
                OnPropertyChanged();
            }
        }

        private ICommand _setUsername;
        public ICommand SetUsername => _setUsername ?? (_setUsername = new CommandHandler(() => SetUser(), () => CanExecute));

        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        private void SetUser()
        {
            User = (string)Profile.Content;
        }

        public MainViewModel()
        {
 
        }
    }
}
