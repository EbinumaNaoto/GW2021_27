using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetSearchApplication {
    //お気に入り店舗情報
    class FavoriteStoreInformation {

        public int FavoriteId { get; set; } //お気に入りID(主キー:連番)
        public string StoreName { get; set; } //店舗名
        public string StoreInformation { get; set; } //店舗情報
        public string Url { get; set; } //店舗URL
    }
}
