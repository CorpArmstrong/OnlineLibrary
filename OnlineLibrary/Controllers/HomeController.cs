using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OnlineLibrary.Infrastructure;
using OnlineLibrary.Infrastructure.Attributes;
using OnlineLibrary.Infrastructure.DAL;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers
{
    [CustomAuthorize]
    [AllowReader]
    public class HomeController : Controller
    {
        private Reader _currentReader;

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AllBooks(bool showAllBooks = true)
        {
            BookDAL bookDAL = new BookDAL();
            return View(bookDAL.GetAvailableBooks(showAllBooks));
        }

        public ActionResult MyAccount()
        {
            _currentReader = _currentReader ?? (Session["Reader"] as Reader);
            List<UserBook> userBooks = UserDAL.GetBooksTakenByUser(_currentReader.ReaderId);
            return View(userBooks);
        }

        [AllowAdmin]
        public ActionResult Administration()
        {
            BookDAL bookDAL = new BookDAL();
            return View(bookDAL.GetAvailableBooks(true));
        }

        [AllowAdmin]
        public ActionResult AddNewBook()
        {
            AuthorDAL authorDAL = new AuthorDAL();
            Book book = new Book();
            book.Authors = authorDAL.GetAuthors();
            return View(book);
        }

        [AllowAdmin]
        public ActionResult AddBookValues(Book book)
        {
            BookDAL bookDAL = new BookDAL();
            bool result = bookDAL.AddBook(book);

            if (result)
            {
                return RedirectToAction("Administration");
            }
            else
            {
                return Content("An Error Has Occured! Please check input data!");
            }
        }

        [AllowAdmin]
        public ActionResult AddNewAuthor()
        {
            return View();
        }

        [AllowAdmin]
        public ActionResult AddAuthorValues(Author author)
        {
            AuthorDAL authorDAL = new AuthorDAL();
            bool result = authorDAL.AddAuthor(author);

            if (result)
            {
                return RedirectToAction("AddNewBook");
            }
            else
            {
                return Content("An Error Has Occured! Please check input data!");
            }
        }

        [AllowAdmin]
        public JsonResult SaveBook(Book saveBook)
        {
            BookDAL bookDAL = new BookDAL();
            int realQuantity = bookDAL.ChangeBookQuantity(saveBook.BookId, saveBook.NormQuantity);

            if (saveBook != null)
            {
                return Json(new { NormQuantity = saveBook.NormQuantity, RealQuantity = realQuantity });
            }
            else
            {
                return Json("An Error Has Occoured!");
            }
        }

        [AllowAdmin]
        public JsonResult DeleteBook(long bookId = -1)
        {
            BookDAL bookDAL = new BookDAL();
            bool isDeleted = bookDAL.RemoveBook(bookId);

            if (bookId != -1)
            {
                return Json(new { IsDeleted = isDeleted });
            }
            else
            {
                return Json("An Error Has Occoured!");
            }
        }

        [HttpPost]
        public JsonResult TakeBook(Book book)
        {
            BookDAL bookDAL = new BookDAL();

            if(_currentReader == null)
            {
                _currentReader = Session["Reader"] as Reader;
            }

            string bookName;
            bool isBookTaken = bookDAL.TakeBook(_currentReader.ReaderId, book.BookId, out bookName);

            if (!isBookTaken)
            {
                EmailSender emailSender = new EmailSender();
                emailSender.SendMail("cherry.cake128@gmail.com", bookName);
                return Json(new { isBookTaken = book.IsBookTaken });
            }
            else
            {
                return Json("An Error Has Occoured!");
            }
        }

        public JsonResult ReturnBook(ReturnBook returnBook)
        {
            BookDAL bookDAL = new BookDAL();
            DateTime? returnDate = bookDAL.ReturnBook(returnBook.ReaderId, returnBook.BookId);
            List<UserBook> userBooks = UserDAL.GetBooksTakenByUser(returnBook.ReaderId);

            if (returnBook != null)
            {
                return Json(new { ReturnDate = returnDate });
            }
            else
            {
                return Json("An Error Has Occoured!");
            }
        }
    }
}
