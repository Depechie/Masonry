using System;
using System.Collections.Generic;
using System.Text;

namespace Masonry.Helpers
{
    public static class MasonryHelper
    {
        private const string SEARCHKEY_CSS = "##CSS##";
        private const string SEARCHKEY_JS = "##INLINEJS##";
        private const string SEARCHKEY_ITEMS = "##ITEMS##";
        private const string SEARCHKEY_ITEMSOURCE = "##ITEMSOURCE##";

        private static string _css = @"
* { box-sizing: border-box; }

body {
  font-family: sans-serif;
  line-height: 1.4;
  font-size: 18px;
  max-width: 640px;
/*   max-width: 820px; */
  margin: 0 auto;
}

.grid {
  max-width: 1200px;
}

/* reveal grid after images loaded */
.grid.are-images-unloaded {
  opacity: 0;
}

/* hide by default */
.grid.are-images-unloaded .image-grid__item {
  opacity: 0;
}

.grid:after {
  content: '';
  display: block;
  clear: both;
}

.grid__item,
.grid__col-sizer {
    width: 50%;
    /* width: 32% */
}

.grid__item {
  float: left;
}

.grid__item img {
  display: block;
  max-width: 100%;
}

button {
  font-size: 20px;
}
";

        private static string _inlineJScript = @"
var grid = document.querySelector('.grid');
var msnry = new Masonry( grid, {
  columnWidth: '.grid__col-sizer',
  itemSelector: 'none',
  percentPosition: true,
  stagger: 30,
  visibleStyle: { transform: 'translateY(0)', opacity: 1 },
  hiddenStyle: { transform: 'translateY(100px)', opacity: 0 },  
});

// initial items reveal
imagesLoaded( grid, function() {
  grid.classList.remove('are-images-unloaded');
  msnry.options.itemSelector = '.grid__item';
  let items = grid.querySelectorAll('.grid__item');
  msnry.appended(items);
});

var appendButton = document.querySelector('.append-button');
appendButton.addEventListener( 'click', function() {
  var elems = [];
  var fragment = document.createDocumentFragment();
  
  for ( var i = 0; i < 3; i++ ) {
    var elem = getItemElement();
    fragment.appendChild(elem);
    elems.push(elem);
  }

  grid.appendChild(fragment);
  var imageLoad = imagesLoaded(grid);
  imageLoad.on( 'progress', function() {  msnry.layout(); });
  msnry.appended(elems);
});

function getItemElement() {
  var elem = document.createElement('div');
  elem.className = 'grid__item';
  elem.innerHTML = '<img class=""image-grid__item"" src=""https://i.imgur.com/kXUHDn5.jpg"">';
  return elem;
}
";

        private static string _gridItem = @"
  <div class='grid__item'>
    <img src='##ITEMSOURCE##' />
  </div>";

        private static string _extraImages = @"          
          <div class='grid__item'><img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/cat-nose.jpg'></div>
          <div class='grid__item'><img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/contrail.jpg'></div>
          <div class='grid__item'><img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/golden-hour.jpg'></div>
          <div class='grid__item'><img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/flight-formation.jpg'></div>
";

        private static string _body = @"
<!DOCTYPE html>
<html>
    <head>
        <meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' />
        <style type='text/css'>##CSS##</style>
    </head>
    <body>
        <div class='grid are-images-unloaded'>
          <div class='grid__col-sizer'></div>
          <div class='grid__item'><img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/orange-tree.jpg'></div>
          <div class='grid__item'><img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/submerged.jpg'></div>
          <div class='grid__item'><img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/look-out.jpg'></div>
          <div class='grid__item'><img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/one-world-trade.jpg'></div>
          <div class='grid__item'><img src='https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/drizzle.jpg'></div>
        </div>

        <p><button class='append-button'>Append new items</button></p>

        <script src='https://unpkg.com/masonry-layout@4/dist/masonry.pkgd.js'></script>
        <script src='https://unpkg.com/imagesloaded@4/imagesloaded.pkgd.js'></script>
        <script id='render-js'>##INLINEJS##</script>
    </body>    
</html>
";

        public static string GenerateHTMLSource()
        {
            return _body.Replace(SEARCHKEY_CSS, _css).Replace(SEARCHKEY_JS, _inlineJScript);
        }

        public static string GenerateItemSource(List<string> items)
        {
            StringBuilder itemSource = new StringBuilder();
            foreach (var item in items)
            {
                itemSource.AppendLine(_gridItem.Replace(SEARCHKEY_ITEMSOURCE, item));
            }

            return itemSource.ToString();
        }

        public static string InsertItems(string htmlSource, string itemSource)
        {
            return htmlSource.Replace(SEARCHKEY_ITEMS, itemSource);
        }
    }
}