using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetSearchApplication {

    //画面情報を保持するクラス
    public class ScreenInformation {

        public static RegisterPage registerPage = new RegisterPage(); //新規会員登録ページ
        public static LoginPage loginPage = new LoginPage(); //ログインページ
        public static SearchPage searchPage = new SearchPage(); //検索ページ

        public static DisplayScreen displayScreen = DisplayScreen.ログイン; //現在表示されている画面の情報を保持する

        //ユーザーサイドのクラスを作って、そのクラスにログイン情報を持たせる
        //一時的に画面情報においている
        public static bool loginInformation = false; //ログイン情報(true:ログイン済み false:ログアウト済み)

        //画面情報
        public enum DisplayScreen {
            検索,
            ログイン,
            会員登録,
        }
    }
}
