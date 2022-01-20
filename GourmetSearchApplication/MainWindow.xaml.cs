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
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        //データベースの親
        public static GourmetSearchApplication.infosys202127DataSet infosys202127DataSet;

        //お気に入り表
        public static GourmetSearchApplication.infosys202127DataSetTableAdapters.FavoritesTableAdapter infosys202127DataSetFavoritesTableAdapter;
        //ジャンル表
        public static GourmetSearchApplication.infosys202127DataSetTableAdapters.GenresTableAdapter infosys202127DataSetGenresTableAdapter;
        //会員表
        public static GourmetSearchApplication.infosys202127DataSetTableAdapters.MembersTableAdapter infosys202127DataSetMembersTableAdapter;
        //都道府県表
        public static GourmetSearchApplication.infosys202127DataSetTableAdapters.PrefecturesTableAdapter infosys202127DataSetPrefecturesTableAdapter;
        
        //プロパティ
        public MainWindow() {
            InitializeComponent();

            // Manually alter window height and width
            this.SizeToContent = SizeToContent.Manual;

            // Automatically resize width relative to content
            this.SizeToContent = SizeToContent.Width;

            // Automatically resize height relative to content
            this.SizeToContent = SizeToContent.Height;

            // Automatically resize height and width relative to content
            this.SizeToContent = SizeToContent.WidthAndHeight;

            //データベースに会員情報がある場合はログイン画面に切り替え
            //データベースに会員情報がない場合は新規会員登録画面に切り替え

            Uri uri = new Uri("/loginPage.xaml", UriKind.Relative);
            frame.Source = uri;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            infosys202127DataSet = ((GourmetSearchApplication.infosys202127DataSet)(this.FindResource("infosys202127DataSet")));
            // テーブル Favorites にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202127DataSetFavoritesTableAdapter = new GourmetSearchApplication.infosys202127DataSetTableAdapters.FavoritesTableAdapter();
            infosys202127DataSetFavoritesTableAdapter.Fill(infosys202127DataSet.Favorites);
            System.Windows.Data.CollectionViewSource favoritesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("favoritesViewSource")));
            favoritesViewSource.View.MoveCurrentToFirst();
            // テーブル Genres にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202127DataSetGenresTableAdapter = new GourmetSearchApplication.infosys202127DataSetTableAdapters.GenresTableAdapter();
            infosys202127DataSetGenresTableAdapter.Fill(infosys202127DataSet.Genres);
            System.Windows.Data.CollectionViewSource genresViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("genresViewSource")));
            genresViewSource.View.MoveCurrentToFirst();
            // テーブル Members にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202127DataSetMembersTableAdapter = new GourmetSearchApplication.infosys202127DataSetTableAdapters.MembersTableAdapter();
            infosys202127DataSetMembersTableAdapter.Fill(infosys202127DataSet.Members);
            System.Windows.Data.CollectionViewSource membersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("membersViewSource")));
            membersViewSource.View.MoveCurrentToFirst();
            // テーブル Prefectures にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202127DataSetPrefecturesTableAdapter = new GourmetSearchApplication.infosys202127DataSetTableAdapters.PrefecturesTableAdapter();
            infosys202127DataSetPrefecturesTableAdapter.Fill(infosys202127DataSet.Prefectures);
            System.Windows.Data.CollectionViewSource prefecturesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("prefecturesViewSource")));
            prefecturesViewSource.View.MoveCurrentToFirst();
        }
    }
}
