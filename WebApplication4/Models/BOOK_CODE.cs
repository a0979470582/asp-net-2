using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This is NoSQL.
/// </summary>
namespace WebApplication4.Models
{
    public class BOOK_CODE
    {
        private static List<BOOK_CODE> bookCodeList = new List<BOOK_CODE>
        {
            new BOOK_CODE() {BOOK_CODE_ID="0",BOOK_CODE_DESC="可以借出"},
            new BOOK_CODE() {BOOK_CODE_ID="1",BOOK_CODE_DESC="已借出"},
            new BOOK_CODE() {BOOK_CODE_ID="2",BOOK_CODE_DESC="不可借出"},
            new BOOK_CODE() {BOOK_CODE_ID="3",BOOK_CODE_DESC="已借出(未領)"}
        };

        public static List<BOOK_CODE> GetData()
        {
            return bookCodeList;
        }



        public static string GetNameWithId(string id)
        {
            foreach (BOOK_CODE item in bookCodeList)
            {
                if (item.BOOK_CODE_ID == id)
                    return item.BOOK_CODE_DESC;
            }
            return "";
        }
        public BOOK_CODE(string id,string desc)
        {
            BOOK_CODE_ID = id;
            BOOK_CODE_DESC = desc;
        }

        public BOOK_CODE() { }//設定一個空的建構子,保留原本建立物件的方式

        public string BOOK_CODE_ID { get; set; }

        public string BOOK_CODE_DESC { get; set; }
    }
}