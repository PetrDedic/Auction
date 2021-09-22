using Aukce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aukce.ViewModels
{
    class AllAuctionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AppDbContext _db = new AppDbContext();

        public AllAuctionsViewModel()
        {
            Auctions = _db.Items.Include(b => b.Creator).Where(a => a.End > DateTime.Now).ToList();
        }

        private List<Item> _item;
        public List<Item> Auctions { get { return _item; } set { _item = value; NotifyPropertyChanged(); } }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}