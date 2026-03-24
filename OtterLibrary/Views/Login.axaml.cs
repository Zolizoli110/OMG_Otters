using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using OtterLibrary.Views;
using System;
using System.Security.Cryptography;

namespace OtterLibrary;

public partial class Login : Window
{
    private readonly string _username = "admin";

    private readonly byte[] _savedSalt;
    private readonly byte[] _savedHash;

    public Login()
    {
        InitializeComponent();
        (_savedHash, _savedSalt) = HashPassword("1234");
    }

    public void SignInButton_Click(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("Attempting login...");
        var inputUsername = UsernameTextBox.Text;
        var inputPassword = PasswordTextBox.Text;

        if (inputUsername != _username)
        {
            Console.WriteLine("Wrong username");
            return;
        }

        bool isValid = VerifyPassword(inputPassword, _savedHash, _savedSalt);

        if (isValid)
        {
            Console.WriteLine("Login SUCCESS");
            var win = new MainWindow();
            win.Show();
            this.Close();
        }
        else
            Console.WriteLine("Wrong password");
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