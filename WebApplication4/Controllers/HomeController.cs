using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        List<Models.BOOK_CLASS> bookClassList = Models.BOOK_CLASS.GetData();
        List<Models.MEMBER_M> memberList = Models.MEMBER_M.GetData();
        List<Models.BOOK_CODE> bookCodeList = Models.BOOK_CODE.GetData();
        
        public ActionResult SelectPage()
        {
            ViewBag.bookClassList = bookClassList;
            ViewBag.memberList = memberList;
            ViewBag.bookCodeList = bookCodeList;
            
            return View();
        }

        public ActionResult SelectResult()
        {
            
            string bookName;
            string bookClassId;
            string userId;
            string codeId;

            var form = Request.Form;
            if (form.Count != 0)
            {
                HttpCookieCollection cookies = HttpContext.Response.Cookies;
                cookies.Add(new HttpCookie("bookName", form["bookName"]));
                cookies.Add(new HttpCookie("bookClassId", form["bookClassId"]));
                cookies.Add(new HttpCookie("userId", form["userId"]));
                cookies.Add(new HttpCookie("codeId", form["codeId"]));
                bookName = form["bookName"];
                bookClassId = form["bookClassId"];
                userId = form["userId"];
                codeId = form["codeId"];
            }else
            {
                bookName = Request.Cookies["bookName"].Value;
                bookClassId = Request.Cookies["bookClassId"].Value;
                userId = Request.Cookies["userId"].Value;
                codeId = Request.Cookies["codeId"].Value;
            }

            List<Models.BOOK_DATA> bookDataList = Models.BOOK_DATA.GetData();
            bookDataList = Models.CRUDMode.DoSelect(bookDataList, bookName, "BOOK_NAME", "0");
            bookDataList = Models.CRUDMode.DoSelect(bookDataList, bookClassId, "BOOK_CLASS_ID", "1");
            bookDataList = Models.CRUDMode.DoSelect(bookDataList, userId, "USER_ID", "1");
            bookDataList = Models.CRUDMode.DoSelect(bookDataList, codeId, "BOOK_CODE_ID", "1");

            bookDataList = Models.CRUDMode.GetNameWithId(bookDataList);

            ViewBag.bookDataList = bookDataList;
            return View();
        }

        public ActionResult UpdatePage()
        {
            ViewBag.bookClassList = bookClassList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateResult(FormCollection post)
        {
            Models.BOOK_DATA Updated = Models.CRUDMode.UpdateBookList(post);
            List<Models.BOOK_DATA> book = new List<Models.BOOK_DATA>() { Updated };      
            book = Models.CRUDMode.GetNameWithId(book);
            ViewBag.book = book;
            return View();
        }

        public ActionResult DeleteResult(int id)
        {
            bool isSuccess = Models.CRUDMode.DoDelete(id.ToString());
            ViewBag.message = isSuccess ? "刪除成功" : "刪除失敗,請先確認借閱狀態";

            return View();
        }

        public ActionResult EditPage(int id)
        {
            Models.BOOK_DATA book = Models.CRUDMode.GetBookDataWithId(id.ToString());
            ViewBag.book = book;

            ViewBag.bookClassList = bookClassList;
            ViewBag.memberList = memberList;
            ViewBag.bookCodeList = bookCodeList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPage(FormCollection post)
        {
            string codeId = post["codeId"];
            string userId = post["userId"];
            bool a = codeId == "0" && (userId == "" || userId=="null");
            bool b = codeId == "1" && (userId != "" && userId != "null");
            bool c = codeId == "2" && (userId == "" || userId == "null");
            bool d = codeId == "3" && (userId != "" && userId != "null");

            if (a || b || c || d)
            {
                Models.BOOK_DATA book = Models.CRUDMode.EditBookDataWithId(post, post["id"].ToString());
                ViewBag.book = book;
            }
            else
            {
                Models.BOOK_DATA book = Models.CRUDMode.GetBookDataWithId(post["id"].ToString());
                ViewBag.book = book;
            }
            ViewBag.bookClassList = bookClassList;
            ViewBag.memberList = memberList;
            ViewBag.bookCodeList = bookCodeList;

            return View();
        }
    }
}