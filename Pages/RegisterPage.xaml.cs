using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Pages;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Logo Animation
        await logoImage.FadeTo(1, 1500); // Fade-in over 1.5 seconds

        // Add a slight delay before showing the form
        await Task.Delay(300);
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Validate Input Fields
        if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text) ||
            string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
        {
            await DisplayAlert("Error", "All fields are required.", "OK");
            return;
        }

        if (txtPassword.Text != txtConfirmPassword.Text)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        // Call Database Helper to Register User
        //var dbHelper = new UserDatabaseHelper();
        //bool isRegistered = await dbHelper.RegisterUser(txtUsername.Text, txtPassword.Text);

        int result = await App.UserDB.AddUser(new User { Username = txtUsername.Text, Password = txtPassword.Text });
        bool isRegistered = result > 0; // Convert int to bool
        if (isRegistered)
        {
            await DisplayAlert("Success", "Registration successful!", "OK");
            await Navigation.PopAsync(); // Go back to Login Page
        }
        else
        {
            await DisplayAlert("Error", "Username already exists!", "OK");
        }
    }

    private void OnLoginTapped(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }

}