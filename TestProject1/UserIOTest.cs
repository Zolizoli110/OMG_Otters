using OtterLibrary.Data;
using OtterLibrary.Models;
using Xunit;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace TestProject1
{
    public class UserIOTests
    {
        private string CreateTempFile(string? initialContent = null)
        {
            string path = Path.GetTempFileName();
            if (initialContent != null)
                File.WriteAllText(path, initialContent);
            return path;
        }

        [Fact]
        public void UserExists_ReturnsUser()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserName = "Alice" },
                new User { UserName = "Bob" }
            };

            string path = CreateTempFile(JsonSerializer.Serialize(users));
            var io = new UserIO(path);

            var result = io.CheckUser("Bob");

            Assert.NotNull(result);
            Assert.Equal("Bob", result.UserName);
        }

        [Fact]
        public void UserDoesNotExist_ReturnsNull()
        {
            var users = new List<User>
            {
                new User { UserName = "Alice" }
            };

            string path = CreateTempFile(JsonSerializer.Serialize(users));
            var io = new UserIO(path);

            var result = io.CheckUser("Charlie");

            Assert.Null(result);
        }

        [Fact]
        public void CheckUser_EdgeCase_EmptyFile_ReturnsNull()
        {
            string path = CreateTempFile("[]");
            var io = new UserIO(path);

            var result = io.CheckUser("Anything");

            Assert.Null(result);
        }

        [Fact]
        public void Borrow_Positive_AddsBooksToUser()
        {
            var users = new List<User>
            {
                new User { UserName = "Alice" }
            };

            string path = CreateTempFile(JsonSerializer.Serialize(users));
            var io = new UserIO(path);

            var booksToBorrow = new ObservableCollection<Book>
            {
                new Book { Title = "Book1" },
                new Book { Title = "Book2" }
            };

            io.Borrow("Alice", booksToBorrow);

            var updatedJson = File.ReadAllText(path);
            var updatedUsers = JsonSerializer.Deserialize<List<User>>(updatedJson);

            Assert.NotNull(updatedUsers);
            Assert.Equal(2, updatedUsers[0].LeasedBooks.Count);
        }

        [Fact]
        public void UserDoesNotExist_DoesNotThrow()
        {
            var users = new List<User>();

            string path = CreateTempFile(JsonSerializer.Serialize(users));
            var io = new UserIO(path);

            var books = new ObservableCollection<Book>
            {
                new Book { Title = "BookX" }
            };

            io.Borrow("GhostUser", books);

            var updatedUsers = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(path));
            Assert.Empty(updatedUsers);
        }

        [Fact]
        public void NoBooksBorrowed_NoChanges()
        {
            var users = new List<User>
            {
                new User { UserName = "Bob" }
            };

            string path = CreateTempFile(JsonSerializer.Serialize(users));
            var io = new UserIO(path);

            var emptyList = new ObservableCollection<Book>();

            io.Borrow("Bob", emptyList);

            var updatedUsers = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(path));
            Assert.Empty(updatedUsers[0].LeasedBooks);
        }

        [Fact]
        public void UserDoesNotExist_NoThrow()
        {
            var users = new List<User>();

            string path = CreateTempFile(JsonSerializer.Serialize(users));
            var io = new UserIO(path);

            io.Return("Nobody", new Book { Title = "XYZ" });

            var updatedUsers = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(path));
            Assert.Empty(updatedUsers);
        }

        [Fact]
        public void BookNotInUserList_NoChange()
        {
            var users = new List<User>
            {
                new User
                {
                    UserName = "Alice",
                    LeasedBooks = new ObservableCollection<Book> { new Book { Title = "OnlyBook" } }
                }
            };

            string path = CreateTempFile(JsonSerializer.Serialize(users));
            var io = new UserIO(path);

            io.Return("Alice", new Book { Title = "NonExisting" });

            var updatedUsers = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(path));
            Assert.Single(updatedUsers[0].LeasedBooks);
            Assert.Equal("OnlyBook", updatedUsers[0].LeasedBooks[0].Title);
        }
    }
}