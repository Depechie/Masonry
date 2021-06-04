var _dotNetObjRef;
var _isNearBottomShown = false;
var _isBottomShown = false;

var _container;
var _content;
var _msnry;
var _pageIndex = 0;

window.blazorExtensions = {
    toggleTrackScroll: function (dotNetObjRef) {
        _dotNetObjRef = dotNetObjRef;
        _container = $('.grid'); // $(document);
        _content = $('.panel-body'); // $(window);

        dotNetObjRef.invokeMethodAsync('ToggleTrackScroll')
            .then(isScrollTrackingEnabled => {
                console.log('isScrollTrackingEnabled: ' + isScrollTrackingEnabled);
                if (isScrollTrackingEnabled) {
                    blazorExtensions.enableScrollTracking();
                } else {
                    blazorExtensions.disableScrollTracking();
                }
            });
    },

    enableScrollTracking: function () {
        console.log('enableScrollTracking invoked');
        $('.panel-body').on("scroll", blazorExtensions.handleWindowScroll);
    },

    disableScrollTracking: function () {
        console.log('disableScrollTracking invoked');
        $('.panel-body').off("scroll", blazorExtensions.handleWindowScroll);
    },
    
    handleWindowScroll: function () {
        console.log('container.height(): ' + _container.height());
        console.log('content.scrollTop(): ' + _content.scrollTop());
        console.log('content.height(): ' + _content.height());

        _dotNetObjRef.invokeMethodAsync('IsNearWindowBottom', _content.scrollTop(), _content.height(), _container.height())
            .then(isNearWindowBottom => {
                console.log('isNearWindowBottom: ' + isNearWindowBottom);
                if (isNearWindowBottom && !_isNearBottomShown) {
                    _isNearBottomShown = true;
                    _pageIndex++;
                    _dotNetObjRef.invokeMethodAsync('LoadMoreImages', _pageIndex);
                }
            });

        // _dotNetObjRef.invokeMethodAsync('IsAtWindowBottom', content.scrollTop(), content.height(), container.height())
        //     .then(isAtWindowBottom => {
        //         console.log('isAtWindowBottom: ' + isAtWindowBottom);
        //         if (isAtWindowBottom && !_isBottomShown) {
        //             _isBottomShown = true;
        //             alert("You are AT the bottom!");
        //         }
        //     });
    },

    initMasonry: function() {
        var grd = document.getElementsByClassName("grid")[0];
        
        _msnry = new Masonry(grd, {
            columnWidth: '.grid__col-sizer',
            itemSelector: 'none',
            percentPosition: true,
            stagger: 30,
            visibleStyle: { transform: 'translateY(0)', opacity: 1 },
            hiddenStyle: { transform: 'translateY(100px)', opacity: 0 },  
        });
        
        imagesLoaded(grd, function() {
            grd.classList.remove('are-images-unloaded');
            _msnry.options.itemSelector = '.grid__item';
            let items = grd.querySelectorAll('.grid__item');
            _msnry.appended(items);
        });
    },

    triggerMasonry: function(data) {
        var grd = document.getElementsByClassName("grid")[0];
        var images = data.split('#');

        var elems = [];
        var fragment = document.createDocumentFragment();

        $.each(images, function(i, item){
            var img = document.createElement("img");
            $(img).attr("src", item);
            $(img).addClass("image-grid__item");
            var div = document.createElement("div");
            $(div).addClass("grid__item");
            $(div).append(img);
            fragment.appendChild( div );
            elems.push( div );
        });

        grd.appendChild( fragment );

        imagesLoaded(grd, function(){
            _msnry.appended( elems );
        });

        _isNearBottomShown = false;
    }
};