using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetSearchApplication {

    class LoginInformation {
        //ユーザー情報
        public static string MemberID { get; set; } = null;//会員ID
        public static string MemberName { get; set; } = null;//会員名
        public static string Password { get; set; } = null;//パスワード
        public static int PrefecturesID { get; set; } = -1;//都道府県ID
        public static int GenreID { get; set; } = -1;//ジャンルID

        //一時的に画面情報においている
        public static bool loginInformation = false; //ログイン情報(true:ログイン済み false:ログアウト済み)
    }
}
