using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OtterLibrary.Data;
using OtterLibrary.Models;
using OtterLibrary.Views;
using System;
using System.Security.Cryptography;
using System.Text;

namespace OtterLibrary.ViewModels;

public partial class LoginWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string usernameInput;

    [ObservableProperty]
    private string passwordInput;

    [ObservableProperty]
    private string loginResult;
    
    public RelayCommand SignInCommand { get; }
    public Action<User> LoginCallback { get; set; }

    public LoginWindowViewModel()
    {
        SignInCommand = new RelayCommand(SignIn);
    }
    
    private void SignIn()
    {
        UserIO userIO = new UserIO("users.json");
        User user = userIO.CheckUser(UsernameInput);
        
        byte[] hashBytes = Convert.FromBase64String(user.Hash);
        byte[] saltBytes = Convert.FromBase64String(user.Salt);
        
        bool isValid = VerifyPassword(PasswordInput, hashBytes, saltBytes);
        if (isValid)
        {
            LoginResult ="Login SUCCESS";
            LoginCallback?.Invoke(user);
        }
        else
        {
            LoginResult = "Wrong username or password";
        }
    }

    private (byte[] hash, byte[] salt) HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        var hash = new Rfc2898DeriveBytes(
            password,
            salt,
            10000,
            HashAlgorithmName.SHA256
        ).GetBytes(32);

        return (hash, salt);
    }

    private bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        var hashToCompare = new Rfc2898DeriveBytes(
            password,
            salt,
            10000,
            HashAlgorithmName.SHA256
        ).GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
    }
}