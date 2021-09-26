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
    class CreateAuctionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AppDbContext _db = new AppDbContext();

        public Item Item { get { return _item; } set { _item = value; NotifyPropertyChanged(); } }
        public DateTimeOffset Date { get { return _date; } set { _date = value; NotifyPropertyChanged(); } }
        public TimeSpan Time { get { return _time; } set { _time = value; NotifyPropertyChanged(); } }

        private Item _item;
        private DateTimeOffset _date;
        private TimeSpan _time;

        public CreateAuctionViewModel()
        {
            Item = new Item { };
            Date = DateTime.Now;
            Time = DateTime.Now.TimeOfDay;
            CreateCommand = new RelayCommand(() =>
            {
                Item item = Item;

                item.End = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds);

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                item.UserId = _db.Users.Single(a => a.Name == localSettings.Values["CurrentUser"].ToString()).Id;

                _db.Items.Add(item);
                _db.SaveChangesAsync();

#pragma warning disable CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
                ShowPopup("Úspěšně vytvořeno");
#pragma warning restore CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.


                Frame rootFrame = Window.Current.Content as Frame;

                rootFrame.Navigate(typeof(MainPage));
            });
        }

        public RelayCommand CreateCommand { get; set; }


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
