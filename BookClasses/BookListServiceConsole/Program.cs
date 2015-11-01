﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClasses;
using NLog;

namespace BookListServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //read or recreate??
            Stream stream = new FileStream("books.b", FileMode.OpenOrCreate);
            Book book = new Book();
            Logger logger = LogManager.GetCurrentClassLogger();
            BookListService service = new BookListService(stream);
            //book.Author = "author2";
            //book.Title = "title";
            //book.Length = 100;
            //book.YearOfPublishing = 1999;
            //book.EditionNumber = 0;
            //service.AddBook(book);
            List<Book> list = service.FindByTag(b => b.Author == "author");
            logger.Info("Test");
            foreach (Book result in list)
            {
                Console.WriteLine(result.ToString());
            }
            service.Dispose();
            Console.ReadLine();
        }
    }
}
