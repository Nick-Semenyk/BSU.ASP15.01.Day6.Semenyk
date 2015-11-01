using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BookClasses
{
    public class BookListService : IDisposable
    {
        private Stream stream;
        private List<Book> books;
        private bool disposed = false;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public BookListService()
        {
            stream = null;
            books = new List<Book>();
            logger.Info("BookListService without default stream was created.");
        }

        public BookListService(Stream stream)
        {
            this.stream = stream;
            books = GetBooks();
            logger.Info("BookListService with default stream was created.");
        }

        ~BookListService()
        {
            if (!disposed)
                Dispose();
            logger.Info("Destructor was called.");
        }

        public void AddBook(Book book)
        {
            if (disposed)
            {
                logger.Error("AddBook: BookListService already disposed");
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            if (!books.Contains(book))
            {
                books.Add(book);
            }
            else
            {
                logger.Error($"AddBook: book {book.ToString()} already exists.");
                throw new BookAlreadyExistsException();
            }
        }

        public List<Book> FindByTag(Predicate<Book> predicate)
        {
            if (disposed)
            {
                logger.Error("FindByTag: BookListService already disposed");
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            if (predicate == null)
            {
                logger.Error("FindByTag: argument is null");
                throw new ArgumentNullException();
            }
            List<Book> result = books.FindAll(predicate);
            logger.Info("FindByTag: matching books were found");
            return result;
        } 

        public void SortBooksByTag(IComparer<Book> comparer)
        {
            if (disposed)
            {
                logger.Error("SortBooksByTag: BookListService already disposed");
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            if (comparer == null)
            {
                logger.Error("SortBooksByTag: argument is null");
                throw new ArgumentNullException();
            }
            books.Sort(comparer);
            logger.Info("SortByTag: books were sorted successfully");
        }

        public void RemoveBook(Book book)
        {
            if (disposed)
            {
                logger.Error("RemoveBook: BookListService already disposed");
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            if (books.Contains(book))
            {
                books.Remove(book);
                logger.Info($"RemoveBook: book {book.ToString()} was successfully removed");
            }
            else
            {
                logger.Error("RemoveBook: no such book in BookListService");
                throw new BookDoesntExistException();
            }
        }

        public void Load(Stream input)
        {
            if (input == null)
            {
                logger.Error("Load: argument is null");
                throw new ArgumentNullException();
            }
            stream = input;
            GetBooks();
            logger.Info("Load: books were loaded successfully");
        }

        public void Save(Stream output)
        {
            if (output == null)
            {
                logger.Error("Save: argument is null");
                throw new ArgumentNullException();
            }
            
            stream = output;
            WriteBooks();
            logger.Info("Save: books were saved successfully");
        }

        public void Dispose()
        {
            logger.Info("Dispose was called");
            WriteBooks();
            disposed = true;
        }

        private List<Book> GetBooks()
        {
            if (disposed)
            {
                logger.Error("BookListService already disposed.");
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            List<Book> result = new List<Book>();
            BinaryReader reader = new BinaryReader(stream);
            try
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Book book = new Book();
                    book.Title = reader.ReadString();
                    book.Author = reader.ReadString();
                    book.Length = reader.ReadInt32();
                    book.YearOfPublishing = reader.ReadInt32();
                    book.EditionNumber = reader.ReadInt32();
                    result.Add(book);
                }
            }
            catch (Exception)
            {
                logger.Error("Error during reading data from stream");
                throw new IOException();
            }
            return result;
        }

        private void WriteBooks()
        {
            if (stream == null)
                return;
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            BinaryWriter writer = new BinaryWriter(stream);
            writer.BaseStream.Position = 0;
            try
            {
                foreach(Book book in books)
                {
                    writer.Write(book.Title);
                    writer.Write(book.Author);
                    writer.Write(book.Length);
                    writer.Write(book.YearOfPublishing);
                    writer.Write(book.EditionNumber);
                }
            }
            catch (Exception)
            {
                logger.Error("Error during writing data into stream");
                throw new IOException();
            }
        }
    }

    class BookAlreadyExistsException : Exception
    {
        public BookAlreadyExistsException()
        {
        }

        public BookAlreadyExistsException(string message) : base(message)
        {
        }

        public BookAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BookAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    class BookDoesntExistException : Exception
    {
        public BookDoesntExistException()
        {
        }

        public BookDoesntExistException(string message) : base(message)
        {
        }

        public BookDoesntExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BookDoesntExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
