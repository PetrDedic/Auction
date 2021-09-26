using Aukce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Dokumentaci k šabloně Prázdná aplikace najdete na adrese https://go.microsoft.com/fwlink/?LinkId=234238

namespace Aukce
{
    /// <summary>
    /// Prázdná stránka, která se dá použít samostatně nebo se na ni dá přejít v rámci
    /// </summary>
    public sealed partial class AllAuctions : Page
    {
        private static AppDbContext _db = new AppDbContext();

        public AllAuctions()
        {
            this.InitializeComponent();
        }

        public static Button Selected { get; set; }
        private void Bid_Click(object sender, RoutedEventArgs e)
        {
            Selected = (Button)e.OriginalSource;
#pragma warning disable CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
            BidAsync("Kolik přihodím:");
#pragma warning restore CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            Selected = (Button)e.OriginalSource;
#pragma warning disable CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
            BuyAsync("Koupit hned?");
#pragma warning restore CS4014 // Protože se toto volání neočekává, vykonávání aktuální metody pokračuje před dokončením volání.
        }

        public static async Task<string> BidAsync(string title)
        {
            var inputTextBox = new TextBox { AcceptsReturn = false };
            (inputTextBox as FrameworkElement).VerticalAlignment = VerticalAlignment.Bottom;
            var dialog = new ContentDialog
            {
                Content = inputTextBox,
                Title = title,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel"

            };
            dialog.PrimaryButtonClick += Bid_PrimaryButtonClick;

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return inputTextBox.Text;
            else
                return "";
        }

        public static async Task<string> BuyAsync(string title)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel"

            };
            dialog.PrimaryButtonClick += Buy_PrimaryButtonClick;

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return "";
            else
                return "";
        }

        public static void Bid_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Item item = _db.Items.Find((int)Selected.Opacity);
            var price = (TextBox)sender.Content;
            item.Price += double.Parse(price.Text);
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChangesAsync();
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(MainPage));
        }

        public static void Buy_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Item item = _db.Items.Find((int)Selected.Opacity);
            item.End = DateTime.Now;
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChangesAsync();
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(MainPage));
        }
    }
}
