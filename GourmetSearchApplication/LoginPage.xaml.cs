using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;

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
            //エラーチェック
            var errorCheck = false;

            //会員IDの未入力チェック
            if (string.IsNullOrWhiteSpace(UserIdText.Text)) {
                UserIdText.Background = Brushes.LightCoral;
                UserIdErrorMessageTextBlock.Text = "入力されていません！";
                errorCheck = true;
            }

            //会員IDの未入力チェック
            if (string.IsNullOrWhiteSpace(PasswordText.Text)) {
                PasswordText.Background = Brushes.LightCoral;
                PasswordErrorMessageTextBlock.Text = "入力されていません！";
                errorCheck = true;
            }

            //エラーが発生した場合に処理を抜ける
            if (errorCheck) {
                return;
            }

            //一致したログイン情報を取得する
            var memberInformation = MainWindow.infosys202127DataSet.Members.Where(x => x.MemberID == UserIdText.Text && x.Password == PasswordText.Text).ToList();

            //ログイン情報がない場合(ログイン失敗)
            if (memberInformation.Count() == 0) {
                UserIdErrorMessageTextBlock.Text = "会員情報がありません";
                return;
            }

            //データベースからログイン情報をLoginInformationに登録
            LoginInformation.MemberID = memberInformation.Select(x => x.MemberID).Single();
            LoginInformation.MemberName = memberInformation.Select(x => x.MemberName).Single();
            LoginInformation.Password = memberInformation.Select(x => x.Password).Single();
            LoginInformation.GenreID = memberInformation.Select(x => x.GenreID).Single();
            LoginInformation.PrefecturesID = memberInformation.Select(x => x.PrefecturesID).Single();

            //LoginInfomationの情報をもとに表示
            LoginInformation.loginInformation = true; //ログイン情報を持たせる
            ScreenInformation.searchPage.UserNameTextBlock.Text = LoginInformation.MemberName; //ユーザー名表示
            
            NavigationService.Navigate(ScreenInformation.searchPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.検索;
        }

        //新規会員登録ボタン
        private void SignupButton_Click(object sender, RoutedEventArgs e) {
            //登録データのリセット
            ScreenInformation.registerPage.NameText.Text = null;
            ScreenInformation.registerPage.UserIdText.Text = null;
            ScreenInformation.registerPage.PasswordText.Text = null;
            ScreenInformation.registerPage.PasswordConfirmationText.Text = null;
            ScreenInformation.registerPage.PrefecturesComboBox.SelectedValue = null;
            ScreenInformation.registerPage.GenreComboBox.SelectedValue = null;

            //画面タイトル変更処理
            ScreenInformation.registerPage.TitleTextBlock.Text = "新規会員登録";
            ScreenInformation.registerPage.RegisterButton.Content = "登録";

            //画面表示処理
            NavigationService.Navigate(ScreenInformation.registerPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.会員登録;
        }

        //パスワード表示・非表示ボタン
        private void PasswordButton_Click(object sender, RoutedEventArgs e) {
            if (PasswordText.Foreground == Brushes.White) {
                PasswordText.Foreground = Brushes.Black;
                PasswordButton.Content = "✖";
            } else {
                PasswordText.Foreground = Brushes.White;
                PasswordButton.Content = "👁";
            }
        }

        //ログイン画面がロードされた時のイベントハンドラ
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            //入力項目のリセット
            UserIdText.Text = "";
            PasswordText.Text = "";
            //エラー項目のリセット
            UserIdErrorMessageTextBlock.Text = "";
            PasswordErrorMessageTextBlock.Text = "";
            //パスワード表示項目のリセット
            PasswordText.Foreground = Brushes.White;
            PasswordButton.Content = "👁";
        }

        //ユーザーIDが入力された時に呼ばれるイベントハンドラ
        private void UserIdText_TextChanged(object sender, TextChangedEventArgs e) {
            UserIdErrorMessageTextBlock.Text = "";
            UserIdText.Background = Brushes.White;
        }

        //パスワードが入力された時に呼ばれるイベントハンドラ
        private void PasswordText_TextChanged(object sender, TextChangedEventArgs e) {
            PasswordErrorMessageTextBlock.Text = "";
            PasswordText.Background = Brushes.White;
        }
    }
}
