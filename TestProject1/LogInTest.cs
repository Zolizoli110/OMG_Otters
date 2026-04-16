using Xunit;
using OtterLibrary.ViewModels;
using System;
using System.IO;

public class LoginWindowTests
{
    private void PrepareUsersJson()
    {
        string realUsers = 
            @"C:\SDU\OMG_Otters\OtterLibrary\bin\Debug\net9.0\users.json";

        if (!File.Exists(realUsers))
            throw new FileNotFoundException("users.json not found at expected location", realUsers);

        // Create a temp directory
        string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        File.Copy(realUsers, Path.Combine(tempDir, "users.json"));

        Directory.SetCurrentDirectory(tempDir);
    }

    [Fact]
    public void Login_Admin_Success()
    {
        PrepareUsersJson();

        var vm = new LoginWindowViewModel();
        vm.UsernameInput = "admin";
        vm.PasswordInput = "password"; // CORRECT PASSWORD

        bool callbackCalled = false;
        vm.LoginCallback = u => callbackCalled = true;

        vm.SignInCommand.Execute(null);

        Assert.Equal("Login SUCCESS", vm.LoginResult);
        Assert.True(callbackCalled);
    }

    [Fact]
    public void Login_Librarian_Success()
    {
        PrepareUsersJson();

        var vm = new LoginWindowViewModel();
        vm.UsernameInput = "librarian";
        vm.PasswordInput = "password";

        vm.SignInCommand.Execute(null);

        Assert.Equal("Login SUCCESS", vm.LoginResult);
    }

    [Fact]
    public void Login_Member_Success()
    {
        PrepareUsersJson();

        var vm = new LoginWindowViewModel();
        vm.UsernameInput = "member";
        vm.PasswordInput = "1234";

        vm.SignInCommand.Execute(null);

        Assert.Equal("Login SUCCESS", vm.LoginResult);
    }

    [Fact]
    public void Login_Alex_Success()
    {
        PrepareUsersJson();

        var vm = new LoginWindowViewModel();
        vm.UsernameInput = "alex";
        vm.PasswordInput = "1234";

        vm.SignInCommand.Execute(null);

        Assert.Equal("Login SUCCESS", vm.LoginResult);
    }
    [Fact]
    public void Login_WrongPassword_Fails()
    {
        PrepareUsersJson();

        var vm = new LoginWindowViewModel();
        vm.UsernameInput = "admin";
        vm.PasswordInput = "incorrect";

        vm.SignInCommand.Execute(null);

        Assert.Equal("Wrong username or password", vm.LoginResult);
    }

    [Fact]
    public void Login_UnknownUser_Fails()
    {
        PrepareUsersJson();

        var vm = new LoginWindowViewModel();
        vm.UsernameInput = "doesnotexist";
        vm.PasswordInput = "anything";

        vm.SignInCommand.Execute(null);

        Assert.Equal("Wrong username or password", vm.LoginResult);
    }

    [Fact]
    public void Login_CallbackNotSet_DoesNotThrow()
    {
        PrepareUsersJson();

        var vm = new LoginWindowViewModel();
        vm.UsernameInput = "admin";
        vm.PasswordInput = "password";

        var ex = Record.Exception(() => vm.SignInCommand.Execute(null));

        Assert.Null(ex);
        Assert.Equal("Login SUCCESS", vm.LoginResult);
    }
}