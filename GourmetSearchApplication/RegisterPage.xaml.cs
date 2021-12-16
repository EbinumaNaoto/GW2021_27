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
    /// RegisterPage.xaml の相互作用ロジック
    /// </summary>
    public partial class RegisterPage : Page {
        public RegisterPage() {
            InitializeComponent();
        }

        //戻るボタン
        private void ReturnButton_Click(object sender, RoutedEventArgs e) {
            if (ScreenInformation.loginInformation) {
                //ログイン済みの場合の処理
                NavigationService.Navigate(ScreenInformation.searchPage);
                ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.検索;
            } else {
                //まだログインしていない場合の処理
                NavigationService.Navigate(ScreenInformation.loginPage);
                ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.ログイン;
            }
        }
    }
}
