using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetSearchApplication {

    class LoginInformation {
        //ユーザー情報
        public static string MemberID { get; set; } //会員ID
        public static string MemberName { get; set; } = "海老沼 尚人";//会員名
        public static string Password { get; set; } //パスワード
        public static int PrefecturesID { get; set; } //都道府県ID
        public static int GenreID { get; set; } //ジャンルID

        //一時的に画面情報においている
        public static bool loginInformation = false; //ログイン情報(true:ログイン済み false:ログアウト済み)
    }
}
