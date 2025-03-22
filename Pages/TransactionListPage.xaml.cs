using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Database;

namespace PersonalFinanceTracker.Pages;

public partial class TransactionListPage : ContentPage
{
    private List<Transaction> transactions = new();
    public TransactionListPage()
	{
		InitializeComponent();
        LoadTransactions();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var transactions = await App.TransactionDB.GetTransactions();

        if (transactions.Count == 0)
        {
            EmptyMessage.IsVisible = true; // Show "No transactions found."
            TransactionListView.IsVisible = false;
        }
        else
        {
            EmptyMessage.IsVisible = false;
            TransactionListView.IsVisible = true;
            TransactionListView.ItemsSource = transactions;
        }
    }

    // Load Transactions from SQLite
    private async void LoadTransactions()
    {
        transactions = await App.TransactionDB.GetTransactions();
        TransactionListView.ItemsSource = transactions;
    }

    // Swipe to Delete Transaction with Confirmation
    private async void OnDeleteTransaction(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var transaction = swipeItem.BindingContext as Transaction;

        if (transaction == null) return;

        bool confirm = await DisplayAlert("Delete Transaction",
                                          $"Are you sure you want to delete \"{transaction.Description}\"?",
                                          "Yes", "No");

        if (confirm)
        {
            await App.TransactionDB.DeleteTransaction(transaction);
            LoadTransactions(); // Refresh the list
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}