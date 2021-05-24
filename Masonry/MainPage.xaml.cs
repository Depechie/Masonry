using System.Collections.Generic;
using System.ComponentModel;
using Masonry.Helpers;
using Xamarin.Forms;

namespace Masonry
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private List<string> _items = new List<string>();
        private Dictionary<int, string> _extraItems = new Dictionary<int, string>();

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

            InitItems();
            InitExtraItems();
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

        private async void InvokeCSharpFromJS(string data)
        {
            if (!string.IsNullOrWhiteSpace(data) && int.Parse(data) is int pageIndex)
            {
                var items = _extraItems[pageIndex];
                string result = await webViewElement.EvaluateJavaScriptAsync($"invokeJSFromCSharp('{items}')");
            }
        }

        private void InitItems()
        {
            _items.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/orange-tree.jpg");
            _items.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/submerged.jpg");
            _items.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/look-out.jpg");
            _items.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/one-world-trade.jpg");
            _items.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/drizzle.jpg");
            _items.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/cat-nose.jpg");
            _items.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/contrail.jpg");
            _items.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/golden-hour.jpg");
            _items.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/flight-formation.jpg");
        }

        private void InitExtraItems()
        {
            List<string> extraItems = new List<string>();

            extraItems.Add("https://i.imgur.com/Qmz61wo.jpg");
            extraItems.Add("https://i.imgur.com/aPia86B.jpg");
            extraItems.Add("https://i.imgur.com/iQRKg2a.jpg");
            extraItems.Add("https://i.imgur.com/XREWwIc.jpg");
            extraItems.Add("https://i.imgur.com/MV9SvaP.jpg");
            extraItems.Add("https://i.imgur.com/qjQ9XWl.jpg");
            extraItems.Add("https://i.imgur.com/ZJ088Tk.jpg");
            extraItems.Add("https://i.imgur.com/SuZLV2U.jpg");
            extraItems.Add("https://i.imgur.com/71H2B0k.jpg");

            _extraItems.Add(0, string.Join("#", extraItems));

            extraItems.Clear();

            extraItems.Add("https://i.imgur.com/vxOA4hg.jpg");
            extraItems.Add("https://i.imgur.com/8kLXqdP.jpg");
            extraItems.Add("https://i.imgur.com/QeN4jBt.jpg");
            extraItems.Add("https://i.imgur.com/ahtrWkN.jpg");
            extraItems.Add("https://i.imgur.com/fd1Mmhy.jpg");
            extraItems.Add("https://i.imgur.com/AOgABvd.jpg");
            extraItems.Add("https://i.imgur.com/ypd73RX.jpg");

            _extraItems.Add(1, string.Join("#", extraItems));
        }

        private string InitHTMLSource()
        {
            var body = MasonryHelper.GenerateHTMLSource();
            var items = MasonryHelper.GenerateItemSource(_items);

            return MasonryHelper.InsertItems(body, items);
        }
    }
}
