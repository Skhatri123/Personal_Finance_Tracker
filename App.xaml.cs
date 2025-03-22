using PersonalFinanceTracker.Database;

namespace PersonalFinanceTracker
{
    public partial class App : Application
    {
        public static UserDatabaseHelper UserDB { get; private set; }
        public static TransactionDatabaseHelper TransactionDB { get; private set; }
        public static int LoggedInUserId { get; set; } // Store Logged-in User ID

        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;
            UserDB = new UserDatabaseHelper();
            TransactionDB = new TransactionDatabaseHelper();
            MainPage = new Pages.LoginPage();
        }
    }
}
