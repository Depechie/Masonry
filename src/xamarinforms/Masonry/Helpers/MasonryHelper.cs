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
    margin-left: 8px; margin-right: 8px; margin-bottom: 20px;
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
    padding: 8px;
}

.grid__item img {
  display: block;
  max-width: 100%;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
}
";

        private static string _inlineJScript = @"
let pageIndex = 0;
let loadNext = false;

window.addEventListener('scroll', () => {
    const { scrollTop, scrollHeight, clientHeight } = document.documentElement;
    if(clientHeight + scrollTop >= scrollHeight - 100 && loadNext) {
        loadNext = false;
        invokeCSharpAction(pageIndex);
        pageIndex++;
	}
});

var grid = document.querySelector('.grid');
var msnry = new Masonry( grid, {
    columnWidth: '.grid__col-sizer',
    itemSelector: 'none',
    percentPosition: true,
    stagger: 30,
    visibleStyle: { transform: 'translateY(0)', opacity: 1 },
    hiddenStyle: { transform: 'translateY(100px)', opacity: 0 },  
});

imagesLoaded( grid, function() {
    grid.classList.remove('are-images-unloaded');
    msnry.options.itemSelector = '.grid__item';
    let items = grid.querySelectorAll('.grid__item');

    loadNext = true;
    msnry.appended(items);
});

function invokeJSFromCSharp(data) {
    var images = data.split('#');

    var elems = [];
    var fragment = document.createDocumentFragment();

    var i;
    for (i = 0; i < images.length; i++) {
        var elem = getItemElement(images[i]);
        fragment.appendChild(elem);
        elems.push(elem);
    }

    grid.appendChild(fragment);
    var imageLoad = imagesLoaded(grid);
    imageLoad.on( 'progress', function() {  msnry.layout(); });

    loadNext = true;
    msnry.appended(elems);
}

function getItemElement(content) {
  var elem = document.createElement('div');
  elem.className = 'grid__item';
  elem.innerHTML = '<img class=""image-grid__item"" src=""' + content + '"">';
  return elem;
}
";

        private static string _gridItem = @"
  <div class='grid__item'>
    <img src='##ITEMSOURCE##' />
  </div>";

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
            ##ITEMS##
        </div>

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

        public static string GenerateItemSource(IEnumerable<string> items)
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