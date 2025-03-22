using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
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

    //private async void OnLoginClicked(object sender, EventArgs e)
    //{
    //    string username = UsernameEntry.Text;
    //    string password = PasswordEntry.Text;

    //    var user = await App.UserDB.GetUser(username, password);

    //    if (user != null)
    //    {
    //        App.LoggedInUserId = user.Id; // Store the logged-in user ID
    //        await DisplayAlert("Success", "Login Successful!", "OK");
    //        Application.Current.MainPage = new NavigationPage(new Pages.HomePage());
    //    }
    //    else
    //    {
    //        await DisplayAlert("Error", "Invalid Username or Password", "OK");
    //    }
    //}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text?.Trim();
        string password = PasswordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Please enter both username and password.", "OK");
            return;
        }

        var user = await App.UserDB.GetUser(username, password);
        if (user != null)
        {
            App.LoggedInUserId = user.Id;
            Application.Current.MainPage = new NavigationPage(new HomePage());
        }
        else
        {
            await DisplayAlert("Login Failed", "Invalid email or password. Try again.", "OK");
        }
    }
    //private async void OnRegisterClicked(object sender, EventArgs e)
    //{
    //    string username = UsernameEntry.Text;
    //    string password = PasswordEntry.Text;

    //    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
    //    {
    //        await App.UserDB.AddUser(new User { Username = username, Password = password });
    //        await DisplayAlert("Success", "Registration Complete! You can now log in.", "OK");
    //    }
    //    else
    //    {
    //        await DisplayAlert("Error", "Please enter valid credentials!", "OK");
    //    }
    //}

   

    private void OnRegisterTapped(object sender, EventArgs e)
    {
         Application.Current.MainPage = new NavigationPage(new RegisterPage());
    }


}