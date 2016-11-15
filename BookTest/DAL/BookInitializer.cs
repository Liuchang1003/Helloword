using System.Collections.Generic;
using System.Data.Entity;
using BookTest.Models;

namespace BookTest.DAL
{
    public class BookInitializer:DropCreateDatabaseIfModelChanges<BookContext>
    {
        protected override void Seed(BookContext context)
        {
            var author = new Author()
            {
                FirstName = "Chang",
                LastName = "Liu"
            };
            
            var books = new List<Book>()
            {
                new Book()
                {
                    Author = author,
                    Description = "This is Descriptions......",
                    ImgUrl = "http://file.feiaibang.cn/product/5268289412334132645s.png",
                    Title = "Cancer is not terrible"
                },
                new Book()
                {
                    Author = author,
                    Description = "This is Descriptions2......",
                    ImgUrl = "http://file.feiaibang.cn/product/5268289412334132645s.png",
                    Title = "Cancer is not terrible2"
                },
            };
            books.ForEach(b=>context.Books.Add(b));

            context.SaveChanges();
        }
    }
}