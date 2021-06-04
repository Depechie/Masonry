using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using masonrymauiblazor.Data;

namespace masonrymauiblazor.ViewModels
{
    public class IndexViewModel : ComponentBase
    {
        protected string result;
        private DotNetObjectReference<IndexViewModel> _objRef;
        protected bool IsScrollTrackingEnabled { get; set; } = false;
        protected List<string> Images { get; set;}

        [Inject]
        protected IJSRuntime JS { get; set; }

        [Inject]
        protected DataService DataService { get; set; }

        public void Dispose()
        {
            _objRef?.Dispose();
        }

        [JSInvokable]
        public bool IsAtWindowBottom(double contentScrollTop, double contentHeight, double containerHeight)
        {
            bool retVal = (contentScrollTop + contentHeight) >= containerHeight;
            return retVal;
        }

        [JSInvokable]
        public bool IsNearWindowBottom(double contentScrollTop, double contentHeight, double containerHeight)
        {
            bool retVal = (contentScrollTop + contentHeight) > (containerHeight - 100);
            return retVal;
        }

        [JSInvokable]
        public async Task LoadMoreImages(int pageIndex)
        {
            var newImages = DataService.GetPhotos(Artists.Depechie, pageIndex);

            //TODO: Glenn - This list of new images should be added to the Blazor list with Images.AddRange(newImages)
            //TODO: Glenn - But this will have the bad positioning effect, we need to trigger the imagesloaded function
            //TODO: Glenn - Also this should be doable according to https://stackoverflow.com/questions/64593058/blazor-force-full-render-instead-of-differential-render
            //TODO: Glenn - But I was not able to get it working correctly, hence the roundtrip and pushing the string to JavaScript
            if (newImages != null && newImages.Any())
                _ = await JS.InvokeAsync<string>("blazorExtensions.triggerMasonry", string.Join("#", newImages));
        }

        public async Task ToggleDotNetTrackScroll()
        {
            _ = await JS.InvokeAsync<string>("blazorExtensions.toggleTrackScroll", _objRef);
        }

        [JSInvokable]
        public bool ToggleTrackScroll() { IsScrollTrackingEnabled = !IsScrollTrackingEnabled; return IsScrollTrackingEnabled; }

        protected override async Task OnInitializedAsync()
        {
            Images = DataService.GetPhotos(Artists.Depechie, 0);
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _objRef = DotNetObjectReference.Create(this);
                await JS.InvokeVoidAsync("blazorExtensions.toggleTrackScroll", _objRef);
                StateHasChanged();

                await JS.InvokeVoidAsync("blazorExtensions.initMasonry", _objRef);
            }
        }
    }
}