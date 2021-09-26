using Aukce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Aukce.ViewModels
{
    class MyAuctionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AppDbContext _db = new AppDbContext();

        public MyAuctionsViewModel()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            
            Auctions = _db.Items.Include(b => b.LastBidder).Where(a => a.Creator.Name == localSettings.Values["CurrentUser"].ToString()).ToList();
        }

        private List<Item> _item;
        public List<Item> Auctions { get { return _item; } set { _item = value; NotifyPropertyChanged(); } }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
