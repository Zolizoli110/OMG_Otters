using OtterLibrary.Data;
using OtterLibrary.Models;
using Xunit;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace OtterLibrary.Tests
{
    public class BookIOTests
    {
        private string CreateTempFile(string? initialContent = null)
        {
            string path = Path.GetTempFileName();
            if (initialContent != null)
                File.WriteAllText(path, initialContent);
            return path;
        }

        [Fact]
        public void ReadsValidJsonCorrectly()
        {
            string json = JsonSerializer.Serialize(new ObservableCollection<Book>
            {
                new Book { Title = "Test", Author = "Author1", ISBN = "123" }
            });

            string path = CreateTempFile(json);
            var io = new BookIO(path);

            var books = io.ReadBook();

            Assert.NotNull(books);
            Assert.Single(books);
            Assert.Equal("Test", books[0].Title);
        }

        [Fact]
        public void CorruptedJsonReturnsEmpty()
        {
            string path = CreateTempFile("{NOT VALID JSON");
            var io = new BookIO(path);

            var books = io.ReadBook();

            Assert.NotNull(books);
            Assert.Empty(books);
        }

        [Fact]
        public void EmptyFileReturnsEmptyCollection()
        {
            string path = CreateTempFile(""); 
            var io = new BookIO(path);

            var books = io.ReadBook();

            Assert.NotNull(books);
            Assert.Empty(books);
        }

        [Fact]
        public void FileDoesNotExistCreatesNew()
        {
            string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var io = new BookIO(path);

            var books = io.ReadBook();

            Assert.True(File.Exists(path));
            Assert.NotNull(books);
            Assert.Empty(books);
        }

        [Fact]
        public void SavesBooksCorrectly()
        {
            string path = CreateTempFile();
            var io = new BookIO(path);

            var books = new ObservableCollection<Book>
            {
                new Book { Title = "Hello", Author = "Author", ISBN = "111" }
            };

            io.Save(books);

            var json = File.ReadAllText(path);
            var loaded = JsonSerializer.Deserialize<ObservableCollection<Book>>(json);

            Assert.NotNull(loaded);
            Assert.Single(loaded);
            Assert.Equal("Hello", loaded[0].Title);
        }

        [Fact]
        public void PathInvalidThrowsException()
        {
            string invalidPath = "/this/path/does/not/exist/books.json";
            var io = new BookIO(invalidPath);

            var books = new ObservableCollection<Book>
            {
                new Book { Title = "X" }
            };

            Assert.ThrowsAny<Exception>(() => io.Save(books));
        }

        [Fact]
        public void SaveEmptyCollectionCreatesValidJsonArray()
        {
            string path = CreateTempFile();
            var io = new BookIO(path);

            var emptyBooks = new ObservableCollection<Book>();

            io.Save(emptyBooks);

            string json = File.ReadAllText(path);
            Assert.Equal("[]", json);
        }
    }
}