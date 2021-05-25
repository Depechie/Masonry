using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Masonry.Helpers;
using Masonry.Models;
using Masonry.Services;
using Xamarin.Forms;

namespace Masonry
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private DataService _dataService = new DataService();

        private HtmlWebViewSource _htmlSource = new HtmlWebViewSource();
        public HtmlWebViewSource HTMLSource
        {
            get => _htmlSource;
            set
            {
                if (value != _htmlSource)
                {
                    _htmlSource = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            HTMLSource.Html = InitHTMLSource();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            webViewElement.RegisterAction(InvokeCSharpFromJS);
            webViewElement.Source = new HtmlWebViewSource()
            {
                Html = InitHTMLSource()
            };
        }

        private void InvokeCSharpFromJS(string data)
        {
            if (!string.IsNullOrWhiteSpace(data) && int.Parse(data) is int pageIndex)
            {
                IEnumerable<string> artistPictures = _dataService.GetPhotos(Artists.Depechie, ++pageIndex);

                if (artistPictures.Any())
                {
                    var items = string.Join("#", artistPictures);

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        string result = await webViewElement.EvaluateJavaScriptAsync($"invokeJSFromCSharp('{items}')");
                    });
                }
            }
        }

        private string InitHTMLSource()
        {
            IEnumerable<string> artistPictures = _dataService.GetPhotos(Artists.Depechie, 0);
            var body = MasonryHelper.GenerateHTMLSource();
            var items = MasonryHelper.GenerateItemSource(artistPictures);

            return MasonryHelper.InsertItems(body, items);
        }
    }
}
