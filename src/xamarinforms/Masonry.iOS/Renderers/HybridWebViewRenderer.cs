using Foundation;
using Masonry.Controls;
using Masonry.iOS.Renderers;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace Masonry.iOS.Renderers
{
    public class HybridWebViewRenderer : WkWebViewRenderer, IWKScriptMessageHandler
    {
        private const string JavaScriptFunction = "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
        private WKUserContentController _userController;

        public HybridWebViewRenderer() : this(new WKWebViewConfiguration())
        {
        }

        public HybridWebViewRenderer(WKWebViewConfiguration config) : base(config)
        {
            _userController = config.UserContentController;
            var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
            _userController.AddUserScript(script);
            _userController.AddScriptMessageHandler(this, "invokeAction");
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            this.Opaque = false;
            this.BackgroundColor = Color.Transparent.ToUIColor();

            if (e.OldElement != null)
            {
                _userController.RemoveAllUserScripts();
                _userController.RemoveScriptMessageHandler("invokeAction");
                HybridWebView hybridWebViewMain = e.OldElement as HybridWebView;
                hybridWebViewMain?.Cleanup();
            }

            if (e.NewElement != null)
            {
                //// No need this since we're loading dynamically generated HTML content
                //string filename = Path.Combine(NSBundle.MainBundle.BundlePath, $"Content/{((HybridWebView)Element).Uri}");
                //LoadRequest(new NSUrlRequest(new NSUrl(filename, false)));
            }
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            ((HybridWebView)Element).InvokeAction(message.Body.ToString());
        }
    }
}