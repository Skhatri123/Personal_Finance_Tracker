using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Database;

namespace PersonalFinanceTracker.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
       
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadSummary(); // Refresh balance when page appears
    }

    // Load Income, Expenses & Balance
    private async void LoadSummary()
    {
        var transactions = await App.TransactionDB.GetTransactions();

        decimal totalIncome = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
        decimal totalExpenses = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);
        decimal totalBalance = totalIncome + totalExpenses;

        IncomeLabel.Text = $"₹ {totalIncome}";
        ExpenseLabel.Text = $"₹ {Math.Abs(totalExpenses)}";
        BalanceLabel.Text = $"₹ {totalBalance}";
    }

    // Navigation to Other Pages
    private async void OnViewTransactionsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TransactionListPage());
    }

    private async void OnAddTransactionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTransactionPage());
    }

    private async void OnViewReportsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReportsPage());
    }
}