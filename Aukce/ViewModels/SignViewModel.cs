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
    class SignViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AppDbContext _db = new AppDbContext();


        private string _name;
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }


        private string _password;
        public string Password { get { return _password; } set { _password = value; NotifyPropertyChanged(); } }

        public SignViewModel()
        {
            SignInCommand = new RelayCommand(
                () =>
                {
                    if (Name != null && Password != null && !_db.Users.Any(a => a.Name == Name))
                    {
                        User user = new User { Name = Name, Password = Password };
                        _db.Users.Add(user);
                        _db.SaveChangesAsync();

#pragma warning disable CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
                        ShowPopup("Úspěšně registrován");
#pragma warning restore CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
                    }
                    else
                    {
#pragma warning disable CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
                        ShowPopup("Nastala chyba :)");
#pragma warning restore CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
                    }
                });
        }

        public RelayCommand SignInCommand { get; set; } 

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
