using Aukce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Aukce.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AppDbContext _db = new AppDbContext();


        private string _name;
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }


        private string _password;
        public string Password { get { return _password; } set { _password = value; NotifyPropertyChanged(); } }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(
                () =>
                {
                    if (Name != null && Password != null && _db.Users.Any(a => a.Name == Name && a.Password == Password))
                    {
                        User user = _db.Users.Single(a => a.Name == Name && a.Password == Password);

                        var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                        localSettings.Values["CurrentUser"] = user.Name;

#pragma warning disable CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
                        ShowPopup("Úspěšně přihlášen");
#pragma warning restore CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.

                        Frame rootFrame = Window.Current.Content as Frame;

                        rootFrame.Navigate(typeof(MainPage));
                    }
                    else
                    {
#pragma warning disable CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
                        ShowPopup("Nastala chyba :)");
#pragma warning restore CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
                    }
                });
        }

        public RelayCommand LoginCommand { get; set; }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ShowPopup(string text)
        {
            var popupDialog = new MessageDialog(text);
            await popupDialog.ShowAsync();
        }
    }
}
