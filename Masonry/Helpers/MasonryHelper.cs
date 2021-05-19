﻿using System;
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
body {
  font-family: sans-serif;
  line-height: 1.4;
  font-size: 18px;
  //padding: 20px;
  max-width: 640px;
  margin: 0 auto;
}

.grid {
  max-width: 1200px;
}

/* reveal grid after images loaded */
.grid.are-images-unloaded {
  opacity: 0;
}

.grid__item,
.grid__col-sizer {
  /* width: 32%; */
    width: 50%;
}

/* hide by default */
.grid.are-images-unloaded .image-grid__item {
  opacity: 0;
}

.grid__item {
  float: left;
}

.grid__item img {
  display: block;
  max-width: 100%;
}

.page-load-status {
  display: none;
  padding-top: 20px;
  border-top: 1px solid #DDD;
  text-align: center;
  color: #777;
}

/* loader ellips in separate pen CSS */

";

        private static string _inlineJScript = @"
let grid = document.querySelector('.grid');

let msnry = new Masonry( grid, {
  itemSelector: 'none', // select none at first
  columnWidth: '.grid__col-sizer',  
  percentPosition: true,
  stagger: 30,
  // nicer reveal transition
  visibleStyle: { transform: 'translateY(0)', opacity: 1 },
  hiddenStyle: { transform: 'translateY(100px)', opacity: 0 },
});


// initial items reveal
imagesLoaded( grid, function() {
  grid.classList.remove('are-images-unloaded');
  msnry.options.itemSelector = '.grid__item';
  let items = grid.querySelectorAll('.grid__item');
  msnry.appended( items );
});

//-------------------------------------//
// hack CodePen to load pens as pages

var nextPenSlugs = [
  '202252c2f5f192688dada252913ccf13',
  'a308f05af22690139e9a2bc655bfe3ee',
  '6c9ff23039157ee37b3ab982245eef28',
];

function getPenPath() {
  let slug = nextPenSlugs[ this.loadCount ];
  if ( slug ) {
    return `/desandro/debug/${slug}`;
  }
}

//-------------------------------------//
// init Infinte Scroll

let infScroll = new InfiniteScroll( grid, {
  path: getPenPath,
  append: '.grid__item',
  outlayer: msnry,
  status: '.page-load-status',
});
";

        private static string _gridItem = @"
  <div class='grid__item'>
    <img src = '##ITEMSOURCE##' />
  </div>";

        private static string _body = @"
<!DOCTYPE html>
<html>
    <head>
        <meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' />
        <style type='text/css'>##CSS##</style>
        <body>
<div class='grid are-images-unloaded'>
  <div class='grid__col-sizer'></div>
  <div class='grid__gutter-sizer'></div>
  ##ITEMS## 
</div>

<div class='page-load-status'>
  <div class='loader-ellips infinite-scroll-request'>
    <span class='loader-ellips__dot'></span>
    <span class='loader-ellips__dot'></span>
    <span class='loader-ellips__dot'></span>
    <span class='loader-ellips__dot'></span>
  </div>
  <p class='infinite-scroll-last'>End of content</p>
  <p class='infinite-scroll-error'>No more pages to load</p>
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