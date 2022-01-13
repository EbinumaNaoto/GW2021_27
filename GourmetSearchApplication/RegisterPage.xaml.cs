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

        public Dictionary<int, string> PrefecturesDic { get; } = new Dictionary<int, string> {
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
        public Dictionary<int, string> GenresDic { get; } = new Dictionary<int, string> {
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
            //null値が含まれている場合のエラー処理
            /*
            if (string.IsNullOrWhiteSpace(ScreenInformation.registerPage.UserIdText.Text) ||
                string.IsNullOrWhiteSpace(ScreenInformation.registerPage.NameText.Text) ||
                string.IsNullOrWhiteSpace(ScreenInformation.registerPage.PasswordText.Text) ||
                string.IsNullOrWhiteSpace(ScreenInformation.registerPage.PasswordConfirmationText.Text) ||
                string.IsNullOrWhiteSpace((string)ScreenInformation.registerPage.PrefecturesComboBox.SelectedValue) ||
                string.IsNullOrWhiteSpace(ScreenInformation.registerPage.GenreComboBox.SelectedValue.ToString())) {
                MessageBox.Show("未入力項目があります", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            */
            /*
            //会員IDが一意ではない場合(既に登録されている場合)のエラー処理
            foreach (var MemberID in infosys202127DataSet.Members.Select(x => x.MemberID).ToList()) {
                if (MemberID.Equals(ScreenInformation.registerPage.UserIdText.Text)) {
                    MessageBox.Show("会員IDが既に登録されています", null, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            */
            //2回打たれたパスワードが一致していない場合の処理
            /*
            if (!ScreenInformation.registerPage.PasswordText.Text.Equals(ScreenInformation.registerPage.PasswordConfirmationText.Text)) {
                MessageBox.Show("パスワードと確認用パスワードの内容が一致しません", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            */
            //DBにユーザー情報を登録する処理(新規レコードの追加)
            //DataRow newDrv = (DataRow)infosys202127DataSet.Members.NewRow();
            //newDrv[0] = ScreenInformation.registerPage.UserIdText.Text;
            //newDrv[1] = ScreenInformation.registerPage.NameText.Text;
            //newDrv[2] = ScreenInformation.registerPage.PasswordText.Text;
            //newDrv[3] = ScreenInformation.registerPage.PrefecturesComboBox.SelectedValue;
            //newDrv[4] = ScreenInformation.registerPage.GenreComboBox.SelectedValue;
            //データセットに新しいレコードを追加
            //infosys202127DataSet.Members.Rows.Add(newDrv);
            //データベース更新
            //infosys202127DataSetMembersTableAdapter.Update(infosys202127DataSet.Members);

            MessageBox.Show("会員情報を登録しました。");

            //画面切り替え処理
            changedScreen();
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

                //登録データのリセット
                ScreenInformation.registerPage.NameText.Text = null;
                ScreenInformation.registerPage.UserIdText.Text = null;
                ScreenInformation.registerPage.PasswordText.Text = null;
                ScreenInformation.registerPage.PasswordConfirmationText.Text = null;
                ScreenInformation.registerPage.PrefecturesComboBox.SelectedValue = null;
                ScreenInformation.registerPage.GenreComboBox.SelectedValue = null;

                //画面表示
                NavigationService.Navigate(ScreenInformation.loginPage);
                ScreenInformation.displayScreen = ScreenInformation.DisplayScreen.ログイン;
            }
        }
    }
}
