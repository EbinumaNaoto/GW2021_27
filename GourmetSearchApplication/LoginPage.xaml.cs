using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GourmetSearchApplication {
    /// <summary>
    /// LoginPage.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginPage : Page {
        public LoginPage() {
            InitializeComponent();
        }

        //ログインボタン
        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            //ログイン情報が一致しているかの処理

            //ログイン成功時の処理
            ScreenInformation.loginInformation = true; //ログイン情報を持たせる

            NavigationService.Navigate(ScreenInformation.searchPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.検索;
        }

        //新規会員登録ボタン
        private void SignupButton_Click(object sender, RoutedEventArgs e) {
            //画面タイトル変更処理
            ScreenInformation.registerPage.TitleTextBlock.Text = "新規会員登録";

            //画面表示処理
            NavigationService.Navigate(ScreenInformation.registerPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.会員登録;
        }
    }
}
