using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Database;

namespace PersonalFinanceTracker.Pages;

public partial class AddTransactionPage : ContentPage
{
	public AddTransactionPage()
	{
		InitializeComponent();
	}

    private async void OnSaveTransaction(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryDescription.Text) ||
            string.IsNullOrWhiteSpace(EntryAmount.Text) ||
            TransactionTypePicker.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Please fill all fields", "OK");
            return;
        }

        decimal amount;
        if (!decimal.TryParse(EntryAmount.Text, out amount))
        {
            await DisplayAlert("Error", "Invalid amount", "OK");
            return;
        }

        var transaction = new Transaction
        {
            Description = EntryDescription.Text,
            Amount = TransactionTypePicker.SelectedItem.ToString() == "Expense" ? -amount : amount,
            Date = TransactionDatePicker.Date,
            UserId = App.LoggedInUserId // Store user ID
        };

        await App.TransactionDB.AddTransaction(transaction);

        await DisplayAlert("Success", "Transaction added successfully", "OK");
        await Navigation.PopAsync();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}