using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Mvc;

namespace WebApplication4.Models
{/// <summary>
/// List<BOOK_DATA>是物件陣列,要注意不同List可能引用同一個物件
/// </summary>
    public class CRUDMode
    {
        public static List<BOOK_DATA> DoSelect(List<BOOK_DATA> source, string prefix, string columnName, string selectMode)//selectMode=0為模糊搜尋,1為精確搜尋
        {
            List<BOOK_DATA> newSource = new List<BOOK_DATA>();
            if (prefix == "null" || prefix == "" || prefix == null)
            {
                foreach (BOOK_DATA obj in source)
                    newSource.Add(obj);
                return newSource;
            }

            for (int i = 0; i < source.Count; i++)
            {
                Type type = source[i].GetType();
                PropertyInfo info = type.GetProperty(columnName);
                object objValue = info.GetValue(source[i]);
                string objString = objValue.ToString();

                if (selectMode == "0" && objString.Contains(prefix))
                    newSource.Add(source[i]);

                if (selectMode == "1" && objString == prefix)
                    newSource.Add(source[i]);
            }
            return newSource;
        }
        public static List<BOOK_DATA> GetNameWithId(List<BOOK_DATA> source)
        {
            List<BOOK_DATA> newSource = new List<BOOK_DATA>();
            for (int i = 0; i < source.Count; i++)
            {
                source[i].BOOK_CLASS_NAME = BOOK_CLASS.GetNameWithId(source[i].BOOK_CLASS_ID);
                source[i].BOOK_CODE_DESC = BOOK_CODE.GetNameWithId(source[i].BOOK_CODE_ID);
                source[i].USER_CNAME = MEMBER_M.GetNameWithId(source[i].USER_ID);
                newSource.Add(source[i]);
            }
            return newSource;
        }

        public static void SaveBookData(List<BOOK_DATA> source)
        {
            using (FileStream file = new FileStream(ConnectDB.GetDBPath(), FileMode.Create,FileAccess.Write))
            {
                BinaryFormatter binformat = new BinaryFormatter();
                binformat.Serialize(file, source);
            }
        }

        public static List<BOOK_DATA> LoadBookData()
        {
            using (FileStream file = new FileStream(ConnectDB.GetDBPath(), FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter binformat = new BinaryFormatter();
                List<BOOK_DATA> source = binformat.Deserialize(file) as List<BOOK_DATA>;
                return source;
            }
        }

        public static BOOK_DATA UpdateBookList(FormCollection post)
        {
            List<BOOK_DATA> bookDataList = BOOK_DATA.GetData();
            int MaxId = -1;
            for (int i = 0; i < bookDataList.Count; i++)
            {
                int bookId = Int32.Parse(bookDataList[i].BOOK_ID);
                MaxId = bookId > MaxId ? bookId : MaxId;
            }
            MaxId++;

            DateTime bookBoughtDate = StringToDateTime(post["bookBoughtDate"]);
            BOOK_DATA bookObj = new BOOK_DATA((MaxId).ToString(), post["bookName"], post["bookClassId"], post["bookAuthor"], bookBoughtDate, post["bookPublisher"], post["bookNote"], "0", null);
            bookDataList.Add(bookObj);
            SaveBookData(bookDataList);
            return bookObj;
        }

        public static bool DoDelete(string bookId)
        {
            List<BOOK_DATA> bookDataList = BOOK_DATA.GetData();
            
            for (int i = 0; i < bookDataList.Count; i++)
            {
                if (bookDataList[i].BOOK_ID == bookId)
                {
                    if (bookDataList[i].BOOK_CODE_ID=="1" || bookDataList[i].BOOK_CODE_ID == "3")
                    {
                        return false;
                    }
                    bookDataList.RemoveAt(i);
                    SaveBookData(bookDataList);
                    return true;
                }
            }
            return false;
        }

        public static BOOK_DATA GetBookDataWithId(string bookId)
        {
            List<BOOK_DATA> bookDataList = BOOK_DATA.GetData();
            BOOK_DATA book = new BOOK_DATA();
            for (int i = 0; i < bookDataList.Count; i++)
                if (bookDataList[i].BOOK_ID == bookId)
                    book = bookDataList[i];
            return book;
        }

        public static BOOK_DATA EditBookDataWithId(FormCollection post, string bookId)
        {
            List<BOOK_DATA> bookDataList = BOOK_DATA.GetData();
            BOOK_DATA book = new BOOK_DATA();
            for (int i = 0; i < bookDataList.Count; i++)
                if (bookDataList[i].BOOK_ID == bookId) {
                    DateTime datetime = StringToDateTime(post["bookBoughtDate"]);
                    book = bookDataList[i];
                    book.BOOK_NAME = post["bookName"];
                    book.BOOK_AUTHOR = post["bookAuthor"];
                    book.BOOK_PUBLISHER = post["bookPublisher"];
                    book.BOOK_NOTE = post["bookNote"];
                    book.BOOK_BOUGHT_DATE = datetime;
                    book.BOOK_CLASS_ID = post["bookClassId"];
                    book.BOOK_CODE_ID = post["codeId"];
                    book.USER_ID = post["userId"];
                    SaveBookData(bookDataList);
                }
            return book;
        }

        public static DateTime StringToDateTime(string dateString)
        { 
            string[] dateStringNumber = dateString.Split('-');
            int year = Int32.Parse(dateStringNumber[0]);
            int month = Int32.Parse(dateStringNumber[1]);
            int day = Int32.Parse(dateStringNumber[2]);
            DateTime datetime = new DateTime(year, month, day);
            return datetime;
        }

    }
}