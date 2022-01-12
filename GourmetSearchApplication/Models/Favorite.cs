using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetSearchApplication.Models {
    //お気に入り表
    public class Favorite {
        public int Id { get; set; } //お気に入りID
        public string MemberId { get; set; } //会員ID
        public string StoreName { get; set; } //店舗名
        public string StoreInformation { get; set; } //店舗情報
        public string Url { get; set; } //店舗Url

        public Member Member { get; set; } //追加表
    }
}
