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
    /// SearchPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SearchPage : Page {
        public SearchPage() {
            InitializeComponent();

        }

        //ログアウトボタン
        private void LogoutButton_Click(object sender, RoutedEventArgs e) {
            ScreenInformation.loginInformation = false; //ログアウト情報を持たせる
            NavigationService.Navigate(ScreenInformation.loginPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.ログイン;
        }
        
        //変更ボタン
        private void ChangeButton_Click(object sender, RoutedEventArgs e) {
            //画面タイトル変更処理
            ScreenInformation.registerPage.TitleTextBlock.Text = "変更";

            //ログイン情報を取ってきて、TextBoxに表示させる処理

            //画面表示処理
            NavigationService.Navigate(ScreenInformation.registerPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.会員登録;
        }
    }
}
