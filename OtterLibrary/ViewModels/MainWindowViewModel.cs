using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Security.Cryptography;

namespace OtterLibrary.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string usernameInput;

    [ObservableProperty]
    private string passwordInput;

    [ObservableProperty]
    private string loginResult;

    private readonly string _username = "admin";
    private readonly byte[] _savedSalt;
    private readonly byte[] _savedHash;

    public IRelayCommand SignInCommand { get; }

    public MainWindowViewModel()
    {
        (_savedHash, _savedSalt) = HashPassword("1234");
        SignInCommand = new RelayCommand(SignIn);
    }

    private void SignIn()
    {
        if (UsernameInput != _username)
        {
            LoginResult = "Wrong username";
            return;
        }

        bool isValid = VerifyPassword(PasswordInput, _savedHash, _savedSalt);

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