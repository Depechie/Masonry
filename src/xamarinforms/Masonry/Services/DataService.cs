using System.Collections.Generic;
using System.Linq;
using Masonry.Models;

namespace Masonry.Services
{
    public class DataService
    {
        private Dictionary<string, List<string>> _artistPictures = new Dictionary<string, List<string>>();

        public DataService()
        {
            InitArtists();
        }

        public IEnumerable<string> GetPhotos(string artist, int pageIndex)
        {
            return _artistPictures[artist].Skip(pageIndex * 12).Take(12);
        }

        private void InitArtists()
        {
            List<string> pictures = new List<string>();

            pictures.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/orange-tree.jpg");
            pictures.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/submerged.jpg");
            pictures.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/look-out.jpg");
            pictures.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/one-world-trade.jpg");
            pictures.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/drizzle.jpg");
            pictures.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/cat-nose.jpg");
            pictures.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/contrail.jpg");
            pictures.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/golden-hour.jpg");
            pictures.Add("https://s3-us-west-2.amazonaws.com/s.cdpn.io/82/flight-formation.jpg");
            pictures.Add("https://i.imgur.com/laIuV0D.jpg");
            pictures.Add("https://i.imgur.com/777dcVU.jpg");
            pictures.Add("https://i.imgur.com/ZPPFND3.jpg");
            pictures.Add("https://i.imgur.com/EpYbuG7.jpg");
            pictures.Add("https://i.imgur.com/Qmz61wo.jpg");
            pictures.Add("https://i.imgur.com/aPia86B.jpg");
            pictures.Add("https://i.imgur.com/iQRKg2a.jpg");
            pictures.Add("https://i.imgur.com/XREWwIc.jpg");
            pictures.Add("https://i.imgur.com/MV9SvaP.jpg");
            pictures.Add("https://i.imgur.com/qjQ9XWl.jpg");
            pictures.Add("https://i.imgur.com/ZJ088Tk.jpg");
            pictures.Add("https://i.imgur.com/SuZLV2U.jpg");
            pictures.Add("https://i.imgur.com/71H2B0k.jpg");
            pictures.Add("https://i.imgur.com/vxOA4hg.jpg");
            pictures.Add("https://i.imgur.com/8kLXqdP.jpg");
            pictures.Add("https://i.imgur.com/QeN4jBt.jpg");
            pictures.Add("https://i.imgur.com/ahtrWkN.jpg");
            pictures.Add("https://i.imgur.com/fd1Mmhy.jpg");
            pictures.Add("https://i.imgur.com/AOgABvd.jpg");
            pictures.Add("https://i.imgur.com/ypd73RX.jpg");
            pictures.Add("https://i.imgur.com/kXUHDn5.jpg");
            pictures.Add("https://i.imgur.com/Qmz61wo.jpg");
            pictures.Add("https://i.imgur.com/aPia86B.jpg");

            _artistPictures.Add(Artists.Depechie, pictures);
        }
    }
}