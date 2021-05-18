using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Masonry
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private string _body = @"
<!DOCTYPE html>
<html>
    <head>
        <meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' />
        <style type='text/css'>
* { box-sizing: border-box; }

/* force scrollbar */
html { overflow-y: scroll; }

body { font-family: sans-serif; }

/* ---- grid ---- */

//.grid {
//  background: #DDD;
//}

/* clear fix */
.grid:after {
  content: '';
  display: block;
  clear: both;
}

/* ---- .grid-item ---- */

.grid-sizer,
.grid-item {
  /* width: 33.333%; */
  width: 50%;
  padding: 2.5px;
}

.grid-item {
  float: left;
}

.grid-item img {
  display: block;
  max-width: 100%;
}

        </style>
        <body>
<div class='grid'>
  <div class='grid-sizer' />
  <div class='grid-item'>
    <img src = 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/orange-tree.jpg' />
  </div>
  <div class='grid-item'>
    <img src = 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/submerged.jpg' />
  </div>
  <div class='grid-item'>
    <img src = 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/look-out.jpg' />
  </div>
  <div class='grid-item'>
    <img src = 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/one-world-trade.jpg' />
  </div>
  <div class='grid-item'>
    <img src = 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/drizzle.jpg' />
  </div>
  <div class='grid-item'>
    <img src = 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/cat-nose.jpg' />
  </div>
  <div class='grid-item'>
    <img src = 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/contrail.jpg' />
  </div>
  <div class='grid-item'>
    <img src = 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/golden-hour.jpg' />
  </div>
  <div class='grid-item'>
    <img src = 'https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/flight-formation.jpg' />
  </div>
</div>
            <script src='https://unpkg.com/masonry-layout@4/dist/masonry.pkgd.js'></script>
            <script src='https://unpkg.com/imagesloaded@4/imagesloaded.pkgd.js'></script>
            <script id='render-js'>
//var grid = document.querySelector('.grid');

//var msnry = new Masonry( grid, {
//  itemSelector: '.grid-item',
//  columnWidth: '.grid-sizer',
//  percentPosition: true
//});

//imagesLoaded( grid ).on( 'progress', function() {
//  // layout Masonry after each image loads
//  msnry.layout();
//});

var grid = document.querySelector('.grid');
var msnry;

imagesLoaded( grid, function() {
  // init Isotope after all images have loaded
  msnry = new Masonry( grid, {
    itemSelector: '.grid-item',
    columnWidth: '.grid-sizer',
    percentPosition: true
  });
});
            </script>
        </body>
    </head>
</html>
";

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

        private string InitHTMLSource()
        {
            return _body;
        }
    }
}
