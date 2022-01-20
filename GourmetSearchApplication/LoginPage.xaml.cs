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
            //一致したログイン情報を取得する
            var memberInformation = MainWindow.infosys202127DataSet.Members.Where(x => x.MemberID == UserIdText.Text && x.Password == PasswordText.Text).ToList();

            //ログイン情報がない場合(ログイン失敗)
            if (memberInformation.Count() == 0) {
                MessageBox.Show("会員情報がありません", null, MessageBoxButton.OK, MessageBoxImage.Error);
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
            ScreenInformation.searchPage.UserNameTextBlock.Text = LoginInformation.MemberName+" 様"; //ユーザー名表示
            
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
            UserIdText.Text = "";
            PasswordText.Text = "";
        }
    }
}
