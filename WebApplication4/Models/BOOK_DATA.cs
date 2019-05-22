using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
/// <summary>
/// This is NoSQL.
/// </summary>
namespace WebApplication4.Models
{
    [Serializable]
    public class BOOK_DATA
    {
        private static List<BOOK_DATA> bookDataList = new List<BOOK_DATA>()
        {
            new BOOK_DATA("0","屏東TOP100美食餐廳","0","Tony",new DateTime(2018,10,15),"好書出版","介紹屏東美食","0",null),
            new BOOK_DATA("1","屏東TOP100旅遊景點","1","Kim",new DateTime(2019,4,12),"好書出版","介紹屏東旅遊景點","0",null),
            new BOOK_DATA("2","屏東TOP100伸展動作","2","John",new DateTime(2017,2,11),"好書出版","介紹屏東伸展動作","0",null),
            new BOOK_DATA("3","屏東TOP100家庭遊戲種類","3","Weber",new DateTime(2011,3,4),"好書出版","介紹屏東遊戲種類","0",null),
            new BOOK_DATA("4","屏東TOP100上市上櫃公司","4","Miller",new DateTime(2012,2,22),"好書出版","介紹屏東股市","0",null),
            new BOOK_DATA("5","屏東TOP100正念思考術","5","Taylor",new DateTime(2014,1,28),"好書出版","介紹屏東心理學","0",null),
        };

        public static List<BOOK_DATA> GetData()
        {
            if (File.Exists(ConnectDB.GetDBPath()))
            {
                return CRUDMode.LoadBookData();
            }
            CRUDMode.SaveBookData(bookDataList);
            return bookDataList;
        }

        public BOOK_DATA(string bookId, string bookName, string ClassId,string Author,DateTime BoughtDate,string Publisher,string Note,string StatusId,string keeper)
        {
            BOOK_ID = bookId;
            BOOK_NAME = bookName;
            BOOK_CLASS_ID = ClassId;
            BOOK_AUTHOR = Author;
            BOOK_BOUGHT_DATE = BoughtDate;
            BOOK_PUBLISHER = Publisher;
            BOOK_NOTE = Note;
            BOOK_CODE_ID = StatusId;
            USER_ID = keeper;
        }

        public BOOK_DATA() { }//設定一個空的建構子,保留原本建立物件的方式

        public string BOOK_ID { get; set; }
        public string BOOK_NAME { get;set; }
        public string BOOK_CLASS_ID { get; set; }
        public string BOOK_AUTHOR { get; set; }
        public DateTime BOOK_BOUGHT_DATE { get; set; }
        public string BOOK_PUBLISHER { get; set; }
        public string BOOK_NOTE { get; set; }
        public string BOOK_CODE_ID { get; set; }
        public string USER_ID { get; set; }
        public string BOOK_CLASS_NAME { get; set; }
        public string BOOK_CODE_DESC { get; set; }
        public string USER_CNAME { get; set; }

    }
}