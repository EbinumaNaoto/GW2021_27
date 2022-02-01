using System;
using System.Collections.Generic;
using System.Data;
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

        public Dictionary<int, string> PrefecturesDic { get; set; } = new Dictionary<int, string> {
            {1,"北海道"},
            {2,"青森県"},
            {3,"岩手県"},
            {4,"宮城県"},
            {5,"秋田県"},
            {6,"山形県"},
            {7,"福島県"},
            {8,"茨城県"},
            {9,"栃木県"},
            {10,"群馬県"},
            {11,"埼玉県"},
            {12,"千葉県"},
            {13,"東京都"},
            {14,"神奈川県"},
            {15,"新潟県"},
            {16,"富山県"},
            {17,"石川県"},
            {18,"福井県"},
            {19,"山梨県"},
            {20,"長野県"},
            {21,"岐阜県"},
            {22,"静岡県"},
            {23,"愛知県"},
            {24,"三重県"},
            {25,"滋賀県"},
            {26,"京都府"},
            {27,"大阪府"},
            {28,"兵庫県"},
            {29,"奈良県"},
            {30,"和歌山県"},
            {31,"鳥取県"},
            {32,"島根県"},
            {33,"岡山県"},
            {34,"広島県"},
            {35,"山口県"},
            {36,"徳島県"},
            {37,"香川県"},
            {38,"愛媛県"},
            {39,"高知県"},
            {40,"福岡県"},
            {41,"佐賀県"},
            {42,"長崎県"},
            {43,"熊本県"},
            {44,"大分県"},
            {45,"宮崎県"},
            {46,"鹿児島県"},
            {47,"沖縄県"},
        }; //都道府県
        public Dictionary<int, string> GenresDic { get; set; } = new Dictionary<int, string> {
            {1,"居酒屋" },
            {2,"ダイニングバー・バル" },
            {3,"創作料理" },
            {4,"和食" },
            {5,"洋食" },
            {6,"イタリアン・フレンチ" },
            {7,"中華" },
            {8,"焼肉・ホルモン" },
            {9,"韓国料理" },
            {10,"アジア・エスニック料理" },
            {11,"各国料理" },
            {12,"カラオケ・パーティ" },
            {13,"バー・カクテル" },
            {14,"ラーメン" },
            {15,"お好み焼き・もんじゃ" },
            {16,"カフェ・スイーツ" },
            {17,"その他" },
        }; //ジャンル

        public RegisterPage() {
            InitializeComponent();
            DataContext = this;
        }

        //戻るボタン
        private void ReturnButton_Click(object sender, RoutedEventArgs e) {
            changedScreen();
        }

        //登録ボタン
        private void RegisterButton_Click(object sender, RoutedEventArgs e) {
            //エラーメッセージのリセット
            UserIdErrorMessageTextBlock.Text = null;
            NameErrorMessageTextBlock.Text = null;
            PasswordErrorMessageTextBlock.Text = null;
            PasswordConfirmationErrorMessageTextBlock.Text = null;
            PrefecturesErrorMessageTextBlock.Text = null;
            GenreErrorMessageTextBlock.Text = null;

            //エラー判定
            var errorCheck = false;

            try {
                //未入力項目チェック

                //会員IDの未入力チェック
                if (string.IsNullOrWhiteSpace(ScreenInformation.registerPage.UserIdText.Text)) {
                    UserIdErrorMessageTextBlock.Text = "入力されていません！";
                    errorCheck = true;
                }

                //会員名の未入力チェック
                if (string.IsNullOrWhiteSpace(ScreenInformation.registerPage.NameText.Text)) {
                    NameErrorMessageTextBlock.Text = "入力されていません！";
                    errorCheck = true;
                }

                //パスワードの未入力チェック
                if (string.IsNullOrWhiteSpace(ScreenInformation.registerPage.PasswordText.Text)) {
                    PasswordErrorMessageTextBlock.Text = "入力されていません！";
                    errorCheck = true;
                }

                //確認用パスワードの未入力チェック
                if (string.IsNullOrWhiteSpace(ScreenInformation.registerPage.PasswordConfirmationText.Text)) {
                    PasswordConfirmationErrorMessageTextBlock.Text = "入力されていません！";
                    errorCheck = true;
                }

                //都道府県の未入力チェック
                if (ScreenInformation.registerPage.PrefecturesComboBox.SelectedIndex <= -1) {
                    PrefecturesErrorMessageTextBlock.Text = "入力されていません！";
                    errorCheck = true;
                }

                //ジャンルの未入力チェック
                if (ScreenInformation.registerPage.GenreComboBox.SelectedIndex <= -1) {
                    GenreErrorMessageTextBlock.Text = "入力されていません！";
                    errorCheck = true;
                }

                //エラーが発生した場合に処理を抜ける
                if (errorCheck) {
                    return;
                }

                //passwordが文字内に収まっているか？
                if (ScreenInformation.registerPage.PasswordText.Text.Length <= 6) {
                    PasswordErrorMessageTextBlock.Text = "パスワードは最低でも6文字以上にしてください";
                    return;
                }

                //確認用passwordが文字内に収まっているか？
                if (ScreenInformation.registerPage.PasswordConfirmationText.Text.Length <= 6) {
                    PasswordConfirmationErrorMessageTextBlock.Text = "パスワードは最低でも6文字以上にしてください";
                    return;
                }

                //passwordと確認用passwordの内容が一致しない場合
                if (!ScreenInformation.registerPage.PasswordText.Text.Equals(ScreenInformation.registerPage.PasswordConfirmationText.Text)) {
                    PasswordErrorMessageTextBlock.Text = "パスワードと確認用パスワードの内容が一致しません";
                    return;
                }

                if (LoginInformation.loginInformation) {
                    //ログイン済みの場合
                    //DBにユーザー情報を変更する処理(既存レコードの変更)
                    DataRow drv = MainWindow.infosys202127DataSet.Members.Rows[MainWindow.infosys202127DataSet.Members.Select(x => x.MemberID).ToList().FindIndex(x => x == LoginInformation.MemberID)];
                    drv[1] = ScreenInformation.registerPage.NameText.Text;
                    drv[2] = ScreenInformation.registerPage.PasswordText.Text;
                    drv[3] = ScreenInformation.registerPage.PrefecturesComboBox.SelectedValue;
                    drv[4] = ScreenInformation.registerPage.GenreComboBox.SelectedValue;

                    //データベース更新
                    MainWindow.infosys202127DataSetMembersTableAdapter.Update(MainWindow.infosys202127DataSet.Members);

                    MessageBox.Show("会員情報を変更しました");

                    //C#側の会員情報も更新する
                    //データベースからログイン情報をLoginInformationに登録
                    LoginInformation.MemberID = ScreenInformation.registerPage.UserIdText.Text;
                    LoginInformation.MemberName = ScreenInformation.registerPage.NameText.Text;
                    LoginInformation.Password = ScreenInformation.registerPage.PasswordText.Text;
                    LoginInformation.GenreID = int.Parse(ScreenInformation.registerPage.PrefecturesComboBox.SelectedValue.ToString());
                    LoginInformation.PrefecturesID = int.Parse(ScreenInformation.registerPage.GenreComboBox.SelectedValue.ToString());

                    //LoginInfomationの情報をもとに表示
                    ScreenInformation.searchPage.UserNameTextBlock.Text = LoginInformation.MemberName + " 様"; //ユーザー名表示

                } else {
                    //まだログインしていない場合
                    //DBにユーザー情報を登録する処理(新規レコードの追加)
                    DataRow newDrv = (DataRow)MainWindow.infosys202127DataSet.Members.NewRow();
                    newDrv[0] = ScreenInformation.registerPage.UserIdText.Text;
                    newDrv[1] = ScreenInformation.registerPage.NameText.Text;
                    newDrv[2] = ScreenInformation.registerPage.PasswordText.Text;
                    newDrv[3] = ScreenInformation.registerPage.PrefecturesComboBox.SelectedValue;
                    newDrv[4] = ScreenInformation.registerPage.GenreComboBox.SelectedValue;
                    //データセットに新しいレコードを追加
                    MainWindow.infosys202127DataSet.Members.Rows.Add(newDrv);
                    //データベース更新
                    MainWindow.infosys202127DataSetMembersTableAdapter.Update(MainWindow.infosys202127DataSet.Members);

                    MessageBox.Show("会員情報を登録しました");
                }
                //画面切り替え処理
                changedScreen();

            } catch (ConstraintException) {
                //一意性制約違反
                UserIdErrorMessageTextBlock.Text = "入力された会員IDが既に登録されています";
            } catch (Exception) {
                //ErrorMessageTextBlock.Text = ex.Message;
            }
        }

        //画面切り替え処理
        private void changedScreen() {
            if (LoginInformation.loginInformation) {
                //ログイン済みの場合の処理

                //画面表示
                NavigationService.Navigate(ScreenInformation.searchPage);
                ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.検索;
            } else {
                //まだログインしていない場合の処理

                //画面表示
                NavigationService.Navigate(ScreenInformation.loginPage);
                ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.ログイン;
            }
        }

        //画面がロードされた時のイベントハンドラ
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            //エラーメッセージのリセット
            UserIdErrorMessageTextBlock.Text = null;
            NameErrorMessageTextBlock.Text = null;
            PasswordErrorMessageTextBlock.Text = null;
            PasswordConfirmationErrorMessageTextBlock.Text = null;
            PrefecturesErrorMessageTextBlock.Text = null;
            GenreErrorMessageTextBlock.Text = null;

            if (LoginInformation.loginInformation) {
                //ログイン済みの場合
                ScreenInformation.registerPage.UserIdText.IsEnabled = false;
            } else {
                ScreenInformation.registerPage.UserIdText.IsEnabled = true;
            }
        }

        //会員名のテキスト内容が変更された時に呼ばれるイベントハンドラ
        private void NameText_TextChanged(object sender, TextChangedEventArgs e) {
            NameErrorMessageTextBlock.Text = null;
        }

        //会員IDのテキスト内容が変更された時に呼ばれるイベントハンドラ
        private void UserIdText_TextChanged(object sender, TextChangedEventArgs e) {
            UserIdErrorMessageTextBlock.Text = null;
        }

        //パスワードのテキスト内容が変更された時に呼ばれるイベントハンドラ
        private void PasswordText_TextChanged(object sender, TextChangedEventArgs e) {
            PasswordErrorMessageTextBlock.Text = null;
        }

        //確認用パスワードのテキスト内容が変更された時に呼ばれるイベントハンドラ
        private void PasswordConfirmationText_TextChanged(object sender, TextChangedEventArgs e) {
            PasswordConfirmationErrorMessageTextBlock.Text = null;
        }

        //都道府県が選択された時に呼ばれるイベントハンドラ
        private void PrefecturesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            PrefecturesErrorMessageTextBlock.Text = null;
        }

        //ジャンルが選択された時に呼ばれるイベントハンドラ
        private void GenreComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            GenreErrorMessageTextBlock.Text = null;
        }
    }
}
