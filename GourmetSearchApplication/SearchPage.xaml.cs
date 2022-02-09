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
using System.Data;
using System.Diagnostics;

namespace GourmetSearchApplication {
    /// <summary>
    /// SearchPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SearchPage : Page {

        ObservableCollection<StoreInformation> ResultItems { get; set; } //検索結果
        ObservableCollection<StoreInformation> NearbyShopItems { get; set; } //近くの店舗
        ObservableCollection<FavoriteStoreInformation> FavoriteStoreItems { get; set; } //お気に入り店舗

        public SearchPage() {
            InitializeComponent();
            PrefecturesComboBox.ItemsSource = ScreenInformation.registerPage.PrefecturesDic;
            GenreComboBox.ItemsSource = ScreenInformation.registerPage.GenresDic;
        }

        //ログアウトボタン
        private void LogoutButton_Click(object sender, RoutedEventArgs e) {
            //ログイン情報をリセットする
            LoginInformation.MemberID = null;
            LoginInformation.MemberName = null;
            LoginInformation.Password = null;
            LoginInformation.GenreID = -1;
            LoginInformation.PrefecturesID = -1;

            //ObservableCollectionの削除
            ResultItems = null;
            NearbyShopItems = null;
            FavoriteStoreItems = null;

            LoginInformation.loginInformation = false; //ログアウト情報を持たせる
            NavigationService.Navigate(ScreenInformation.loginPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.ログイン;
        }

        //変更ボタン
        private void ChangeButton_Click(object sender, RoutedEventArgs e) {
            //ログイン情報を取ってきて、TextBoxに表示させる処理
            ScreenInformation.registerPage.NameText.Text = LoginInformation.MemberName;
            ScreenInformation.registerPage.UserIdText.Text = LoginInformation.MemberID;
            ScreenInformation.registerPage.PasswordText.Text = LoginInformation.Password;
            ScreenInformation.registerPage.PasswordConfirmationText.Text = LoginInformation.Password;
            ScreenInformation.registerPage.PrefecturesComboBox.SelectedValue = LoginInformation.PrefecturesID;
            ScreenInformation.registerPage.GenreComboBox.SelectedValue = LoginInformation.GenreID;

            //画面タイトル変更処理
            ScreenInformation.registerPage.TitleTextBlock.Text = "変更";
            ScreenInformation.registerPage.RegisterButton.Content = "変更";

            //画面表示処理
            NavigationService.Navigate(ScreenInformation.registerPage);
            ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.会員登録;
        }

        //検索ボタン
        private void SearchButton_Click(object sender, RoutedEventArgs e) {
            //店舗情報をxml形式で取り出す
            using (var wc = new WebClient()) {
                wc.Headers.Add("Content-type", "charset=UTF-8");
                var urlString = string.Format(@"https://webservice.recruit.co.jp/hotpepper/gourmet/v1/?key=0f725f5af8c55f63&count=100&genre={0}&address={1}&private_room={2}&lunch={3}&parking={4}&non_smoking={5}&barrier_free={6}&keyword={7}",
                    MainWindow.infosys202127DataSet.Genres.Where(x => x.GenreID == int.Parse(GenreComboBox.SelectedValue.ToString())).Select(x => x.GenreName).Single(),
                    MainWindow.infosys202127DataSet.Prefectures.Where(x => x.PrefecturesID == int.Parse(PrefecturesComboBox.SelectedValue.ToString())).Select(x => x.PrefecturesName).Single(),
                    Convert.ToInt32((bool)PrivateRoomCheckBox.IsChecked),
                    Convert.ToInt32((bool)LunchCheckBox.IsChecked),
                    Convert.ToInt32((bool)ParkingCheckBox.IsChecked),
                    Convert.ToInt32((bool)NonSmokingCheckBox.IsChecked),
                    Convert.ToInt32((bool)BarrierFreeCheckBox.IsChecked),
                    KeywordTextBox.Text);
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
            try {
                //DBにお気に入り店舗情報を登録する処理(新規レコードの追加)
                DataRow newDrv = (DataRow)MainWindow.infosys202127DataSet.Favorites.NewRow();
                newDrv[0] = MainWindow.infosys202127DataSet.Favorites.Last().FavoriteID + 1;
                newDrv[1] = LoginInformation.MemberID;
                newDrv[2] = ResultItems[ResultDataGrid.Items.IndexOf(ResultDataGrid.SelectedItem)].Name;
                newDrv[3] = ResultItems[ResultDataGrid.Items.IndexOf(ResultDataGrid.SelectedItem)].Information;
                newDrv[4] = ResultItems[ResultDataGrid.Items.IndexOf(ResultDataGrid.SelectedItem)].Url;
                //データセットに新しいレコードを追加
                MainWindow.infosys202127DataSet.Favorites.Rows.Add(newDrv);
                //データベース更新
                MainWindow.infosys202127DataSetFavoritesTableAdapter.Update(MainWindow.infosys202127DataSet.Favorites);

                DisplayFavoriteStoreDataGrid();
            } catch (NullReferenceException) {
                ErrorTextBlock.Text = "検索結果からお気に入り登録してください";
            } catch (ArgumentOutOfRangeException) {
                ErrorTextBlock.Text = "検索結果からお気に入り登録してください";
            }
        }


        //お気に入り削除
        private void FavoriteDeleteButton_Click(object sender, RoutedEventArgs e) {
            try {
                //選択行の削除
                MainWindow.infosys202127DataSet.Favorites.Rows[MainWindow.infosys202127DataSet.Favorites.Select(x => x.FavoriteID).ToList().FindIndex(x => x == FavoriteStoreItems[FavoriteStoreDataGrid.Items.IndexOf(FavoriteStoreDataGrid.SelectedItem)].FavoriteId)].Delete();
                //データベースの更新
                MainWindow.infosys202127DataSetFavoritesTableAdapter.Update(MainWindow.infosys202127DataSet.Favorites);

                DisplayFavoriteStoreDataGrid();
            } catch (IndexOutOfRangeException) {
                ErrorTextBlock.Text = "お気に入り店舗を選択してから削除してください";
            } catch(ArgumentOutOfRangeException) {
                ErrorTextBlock.Text = "お気に入り店舗を選択してから削除してください";
            }
            
        }

        //ResultDataGridの選択された行がダブルクリックされた時のイベントハンドラー
        private void ResultDataGridRow_DoubleClick(object sender, MouseButtonEventArgs e) {
            OpenUrl(ResultItems[ResultDataGrid.Items.IndexOf(ResultDataGrid.SelectedItem)].Url);
        }

        //NearbyShopDataGridの選択された行がダブルクリックされた時のイベントハンドラ
        private void NearbyShopDataGridRow_DoubleClick(object sender, MouseButtonEventArgs e) {
            OpenUrl(NearbyShopItems[NearbyShopDataGrid.Items.IndexOf(NearbyShopDataGrid.SelectedItem)].Url);;
        }

        //FavoriteStoreDataGridの選択された行がダブルクリックされた時のイベントハンドラ
        private void FavoriteStoreDataGridRow_DoubleClick(object sender, MouseButtonEventArgs e) {
            OpenUrl(FavoriteStoreItems[FavoriteStoreDataGrid.Items.IndexOf(FavoriteStoreDataGrid.SelectedItem)].Url);
        }

        //画面がロードされたときに呼ばれるイベントハンドラ
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            //検索結果のリセット
            ResultDataGrid.ItemsSource = null;
            //検索テキストのリセット
            KeywordTextBox.Text = null;
            //エラーメッセージのリセット
            ErrorTextBlock.Text = null;
            //検索条件のリセット
            PrivateRoomCheckBox.IsChecked = false;
            ParkingCheckBox.IsChecked = false;
            LunchCheckBox.IsChecked = false;
            NonSmokingCheckBox.IsChecked = false;
            BarrierFreeCheckBox.IsChecked = false;
            PrefecturesComboBox.SelectedValue = LoginInformation.PrefecturesID;
            GenreComboBox.SelectedValue = LoginInformation.GenreID;

            //近くのおすすめ店舗一覧とお気に入り店舗一覧の表示
            using (var wc = new WebClient()) {
                wc.Headers.Add("Content-type", "charset=UTF-8");
                var urlString = string.Format(@"https://webservice.recruit.co.jp/hotpepper/gourmet/v1/?key=0f725f5af8c55f63&count=100&genre={0}&address={1}",
                    MainWindow.infosys202127DataSet.Genres.Where(x => x.GenreID == LoginInformation.GenreID).Select(x => x.GenreName).Single(),
                    MainWindow.infosys202127DataSet.Prefectures.Where(x => x.PrefecturesID == LoginInformation.PrefecturesID).Select(x => x.PrefecturesName).Single());

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

                DisplayFavoriteStoreDataGrid();
            };
        }

        //お気に入り店舗情報を表示する処理
        private void DisplayFavoriteStoreDataGrid() {
            //データベースからデータをObservableClooectionに格納する
            FavoriteStoreItems = new ObservableCollection<FavoriteStoreInformation>(MainWindow.infosys202127DataSet.Favorites.Where(x => x.MemberID == LoginInformation.MemberID).Select(x => new FavoriteStoreInformation {
                FavoriteId = x.FavoriteID,
                StoreName = x.StoreName,
                StoreInformation = x.StoreInformation,
                Url = x.StoreUrl
            }).ToList());

            FavoriteStoreDataGrid.ItemsSource = FavoriteStoreItems;
        }

        //ホットペッパーのクレジット表示
        private void HotpepperCreditButton_Click(object sender, RoutedEventArgs e) {
            OpenUrl("http://webservice.recruit.co.jp/");
        }

        //お気に入り店舗が選ばれた時に呼ばれるイベントハンドラ
        private void FavoriteStoreDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ErrorTextBlock.Text = "";
        }

        //検索店舗が選ばれた時に呼ばれるイベントハンドラ
        private void ResultDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ErrorTextBlock.Text = "";
        }

        //おすすめ店舗が選ばれた時に呼ばれるイベントハンドラ
        private void NearbyShopDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ErrorTextBlock.Text = "";
        }

        //選択されたurlをedgeに表示する
        private void OpenUrl(string url) {
            ProcessStartInfo pi = new ProcessStartInfo() {
                FileName = url,
                UseShellExecute = true,
            };
            try {
                Process.Start(pi);
            } catch (System.ComponentModel.Win32Exception noBrowser) {
                if (noBrowser.ErrorCode == -2147467259)
                    ErrorTextBlock.Text = noBrowser.Message;
            } catch (System.Exception other) {
                ErrorTextBlock.Text = other.Message;
            }

        }

        //ジャンルボタン
        private void GenreButton_Click(object sender, RoutedEventArgs e) {
            GenreComboBox.SelectedValue = LoginInformation.GenreID;
        }

        //都道府県ボタン
        private void PrefecturesButton_Click(object sender, RoutedEventArgs e) {
            PrefecturesComboBox.SelectedValue = LoginInformation.PrefecturesID;
        }
    }
}
