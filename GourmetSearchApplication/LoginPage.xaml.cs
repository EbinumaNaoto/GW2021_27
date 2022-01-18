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

        ObservableCollection<StoreInformation> items { get; set; } = null;

        public LoginPage() {
            InitializeComponent();
        }

        //ログインボタン
        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            //ログイン情報が一致しているかの処理

            //ログイン成功時の処理
            //データベースからログイン情報をLoginInformationに登録

            //LoginInfomationの情報をもとに表示
            LoginInformation.loginInformation = true; //ログイン情報を持たせる
            ScreenInformation.searchPage.UserNameTextBlock.Text = LoginInformation.MemberName+" 様"; //ユーザー名表示
            //近くのおすすめ店舗一覧とお気に入り店舗一覧の表示
            using (var wc = new WebClient()) {
                wc.Headers.Add("Content-type", "charset=UTF-8");                                                                         //データベースからデータを持ってくる
                var urlString = string.Format(@"https://webservice.recruit.co.jp/hotpepper/gourmet/v1/?key=0f725f5af8c55f63&keyword={0}", "群馬県 ラーメン");
                var url = new Uri(urlString);
                var stream = wc.OpenRead(url);

                var xdoc = XDocument.Load(stream);

                //apiから取得したデータをObservableClooectionに格納する
                items = new ObservableCollection<StoreInformation>(xdoc.Root.Descendants("{http://webservice.recruit.co.jp/HotPepper/}shop").Select(x => new StoreInformation {
                    Photo = x.Element("{http://webservice.recruit.co.jp/HotPepper/}logo_image").Value,
                    Name = x.Element("{http://webservice.recruit.co.jp/HotPepper/}name").Value,
                    Genre = x.Element("{http://webservice.recruit.co.jp/HotPepper/}genre").Element("{http://webservice.recruit.co.jp/HotPepper/}name").Value,
                    Information = x.Element("{http://webservice.recruit.co.jp/HotPepper/}catch").Value,
                    Time = x.Element("{http://webservice.recruit.co.jp/HotPepper/}open").Value,
                    Address = x.Element("{http://webservice.recruit.co.jp/HotPepper/}address").Value,
                    Station = x.Element("{http://webservice.recruit.co.jp/HotPepper/}station_name").Value + "駅",
                    Url = x.Element("{http://webservice.recruit.co.jp/HotPepper/}urls").Element("{http://webservice.recruit.co.jp/HotPepper/}pc").Value,
                }).ToList());

                ScreenInformation.searchPage.NearbyShopDataGrid.ItemsSource = items;
            };

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
