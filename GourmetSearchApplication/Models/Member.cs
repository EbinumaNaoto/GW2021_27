using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetSearchApplication.Models {
    //会員表
    public class Member {
        public string MemberId { get; set; } //会員ID
        public string MemberName { get; set; } //会員名
        public string Password { get; set; } //パスワード
        public int PrefeturesId { get; set; } //都道府県ID
        public int GenreId { get; set; } //ジャンルID

        public Prefetures Prefetures { get; set; } //追加表
        public Genre Genre { get; set; } //追加表
    }
}
