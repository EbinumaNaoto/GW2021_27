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
using System.Drawing;
using System.IO;
using System.Reflection;

namespace GourmetSearchApplication {
    /// <summary>
    /// SearchPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SearchPage : Page {

        ObservableCollection<StoreInformation> ResultItems { get; set; } = null; //検索結果
        ObservableCollection<StoreInformation> NearbyShopItems { get; set; } = null; //近くの店舗
        ObservableCollection<StoreInformation> FavoriteStoreItems { get; set; } = null; //お気に入り店舗

        public SearchPage() {
            InitializeComponent();
        }

        //ログアウトボタン
        private void LogoutButton_Click(object sender, RoutedEventArgs e) {
            LoginInformation.loginInformation = false; //ログアウト情報を持たせる
            NavigationService.Navigate(ScreenInformation.loginPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.ログイン;
        }

        //変更ボタン
        private void ChangeButton_Click(object sender, RoutedEventArgs e) {
            //画面タイトル変更処理
            ScreenInformation.registerPage.TitleTextBlock.Text = "変更";

            //ログイン情報を取ってきて、TextBoxに表示させる処理
            ScreenInformation.registerPage.NameText.Text = LoginInformation.MemberName;
            ScreenInformation.registerPage.UserIdText.Text = LoginInformation.MemberID;
            ScreenInformation.registerPage.PasswordText.Text = LoginInformation.Password;
            ScreenInformation.registerPage.PasswordConfirmationText.Text = LoginInformation.Password;
            ScreenInformation.registerPage.PrefecturesComboBox.SelectedValue = LoginInformation.PrefecturesID;
            ScreenInformation.registerPage.GenreComboBox.SelectedValue = LoginInformation.GenreID;

            //画面表示処理
            NavigationService.Navigate(ScreenInformation.registerPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.会員登録;
        }

        //検索ボタン
        private void SearchButton_Click(object sender, RoutedEventArgs e) {
            //検索textに何もない場合はreturnを返す
            if (string.IsNullOrWhiteSpace(KeywordTextBox.Text))
                return;

            //店舗情報をxml形式で取り出す
            using (var wc = new WebClient()) {
                wc.Headers.Add("Content-type", "charset=UTF-8");
                var urlString = string.Format(@"https://webservice.recruit.co.jp/hotpepper/gourmet/v1/?key=0f725f5af8c55f63&keyword={0}", KeywordTextBox.Text);
                var url = new Uri(urlString);
                var stream = wc.OpenRead(url);

                var xdoc = XDocument.Load(stream);

                //apiから取得したデータをObservableClooectionに格納する
                ResultItems = new ObservableCollection<StoreInformation>(xdoc.Root.Descendants("{http://webservice.recruit.co.jp/HotPepper/}shop").Select(x => new StoreInformation {
                    Photo = x.Element("{http://webservice.recruit.co.jp/HotPepper/}logo_image").Value,
                    Name = x.Element("{http://webservice.recruit.co.jp/HotPepper/}name").Value,
                    Genre = x.Element("{http://webservice.recruit.co.jp/HotPepper/}genre").Element("{http://webservice.recruit.co.jp/HotPepper/}name").Value,
                    Information = x.Element("{http://webservice.recruit.co.jp/HotPepper/}catch").Value,
                    Time = x.Element("{http://webservice.recruit.co.jp/HotPepper/}open").Value,
                    Address = x.Element("{http://webservice.recruit.co.jp/HotPepper/}address").Value,
                    Station = x.Element("{http://webservice.recruit.co.jp/HotPepper/}station_name").Value + "駅",
                    Url = x.Element("{http://webservice.recruit.co.jp/HotPepper/}urls").Element("{http://webservice.recruit.co.jp/HotPepper/}pc").Value,
                }).ToList());

                ResultDataGrid.ItemsSource = ResultItems;
            };
        }

        //検索テキストでEnterキーが押された時のイベントハンドラー
        private void KeywordTextBox_KeyDown(object sender, KeyEventArgs e) {
            SearchButton_Click(sender, e);
        }

        //お気に入り登録ボタン
        private void FavoriteButton_Click(object sender, RoutedEventArgs e) {

        }

        //ResultDataGridの選択された行がダブルクリックされた時のイベントハンドラー
        private void ResultDataGridRow_DoubleClick(object sender, MouseButtonEventArgs e) {
            var webBrowser = new WebBrowserWindow();
            webBrowser.ShopsWebBrowser.Source = new Uri(ResultItems[ResultDataGrid.Items.IndexOf(ResultDataGrid.SelectedItem)].Url);
            webBrowser.ShowDialog();
        }

        //NearbyShopDataGridの選択された行がダブルクリックされた時のイベントハンドラ
        private void NearbyShopDataGridRow_DoubleClick(object sender, MouseButtonEventArgs e) {
            var webBrowser = new WebBrowserWindow();
            webBrowser.ShopsWebBrowser.Source = new Uri(NearbyShopItems[NearbyShopDataGrid.Items.IndexOf(NearbyShopDataGrid.SelectedItem)].Url);
            webBrowser.ShowDialog();
        }

        //画面がロードされたときに呼ばれるイベントハンドラ
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            //検索結果のリセット
            ResultDataGrid.ItemsSource = null;

            //近くのおすすめ店舗一覧とお気に入り店舗一覧の表示
            using (var wc = new WebClient()) {
                wc.Headers.Add("Content-type", "charset=UTF-8");                                                                         //データベースからデータを持ってくる
                var urlString = string.Format(@"https://webservice.recruit.co.jp/hotpepper/gourmet/v1/?key=0f725f5af8c55f63&keyword={0}", "群馬県 ラーメン");
                var url = new Uri(urlString);
                var stream = wc.OpenRead(url);

                var xdoc = XDocument.Load(stream);

                //apiから取得したデータをObservableClooectionに格納する
                NearbyShopItems = new ObservableCollection<StoreInformation>(xdoc.Root.Descendants("{http://webservice.recruit.co.jp/HotPepper/}shop").Select(x => new StoreInformation {
                    Photo = x.Element("{http://webservice.recruit.co.jp/HotPepper/}logo_image").Value,
                    Name = x.Element("{http://webservice.recruit.co.jp/HotPepper/}name").Value,
                    Genre = x.Element("{http://webservice.recruit.co.jp/HotPepper/}genre").Element("{http://webservice.recruit.co.jp/HotPepper/}name").Value,
                    Information = x.Element("{http://webservice.recruit.co.jp/HotPepper/}catch").Value,
                    Time = x.Element("{http://webservice.recruit.co.jp/HotPepper/}open").Value,
                    Address = x.Element("{http://webservice.recruit.co.jp/HotPepper/}address").Value,
                    Station = x.Element("{http://webservice.recruit.co.jp/HotPepper/}station_name").Value + "駅",
                    Url = x.Element("{http://webservice.recruit.co.jp/HotPepper/}urls").Element("{http://webservice.recruit.co.jp/HotPepper/}pc").Value,
                }).ToList());

                NearbyShopDataGrid.ItemsSource = NearbyShopItems;
            };
        }
    }
}
