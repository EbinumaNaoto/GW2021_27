using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetSearchApplication {
    class StoreInformation {

        //プロパティ
        public string Photo { get; set; } //写真
        public string Name { get; set; } //店舗名
        public string Genre { get; set; } //ジャンル
        public string Information { get; set; } //店舗情報(お店キャッチ)
        public string Time { get; set; } //営業時間
        public string Address { get; set; } //住所
        public string Station { get; set; } //最寄り駅
        public string Url { get; set; } //url
    }
}
