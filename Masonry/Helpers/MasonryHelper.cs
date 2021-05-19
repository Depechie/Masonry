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
html { overflow-y: scroll; }
body { font-family: sans-serif; }

.grid:after {
  content: '';
  display: block;
  clear: both;
}

.grid-sizer,
.grid-item { width: 50%; padding: 2.5px; }

.grid-item { float: left; }

.grid-item img { display: block; max-width: 100%; }
";

        private static string _inlineJScript = @"
var grid = document.querySelector('.grid');
var msnry;

imagesLoaded( grid, function() {
  msnry = new Masonry( grid, { itemSelector: '.grid-item', columnWidth: '.grid-sizer', percentPosition: true, horizontalOrder: false });
});
";

        private static string _gridItem = @"
  <div class='grid-item'>
    <img src = '##ITEMSOURCE##' />
  </div>";

        private static string _body = @"
<!DOCTYPE html>
<html>
    <head>
        <meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' />
        <style type='text/css'>##CSS##</style>
        <body>
<div class='grid'>
  <div class='grid-sizer' />
  ##ITEMS##
</div>            
            <script src='https://unpkg.com/masonry-layout@4/dist/masonry.pkgd.js'></script>
            <script src='https://unpkg.com/imagesloaded@4/imagesloaded.pkgd.js'></script>
            <script id='render-js'>##INLINEJS##</script>
        </body>
    </head>
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