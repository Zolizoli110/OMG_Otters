using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OtterLibrary.Data;
using OtterLibrary.Models;
using System;
using System.Security.Cryptography;

namespace OtterLibrary.ViewModels;

public partial class LoginWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string usernameInput;

    [ObservableProperty]
    private string passwordInput;

    [ObservableProperty]
    private string loginResult;

    private readonly string _username = "admin";
    
    
    public RelayCommand SignInCommand { get; }

    public LoginWindowViewModel()
    {
        SignInCommand = new RelayCommand(SignIn);
    }
    
    private void SignIn()
    {
        if (UsernameInput != _username)
        {
            LoginResult = "Wrong username";
            return;
        }
        UserIO userIO = new UserIO("users.json");
        User user = userIO.CheckUser(UsernameInput);
        
        

        
        bool isValid = VerifyPassword(PasswordInput, user.hash, user.salt);
        
        LoginResult = isValid ? "Login SUCCESS" : "Wrong username or password";
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