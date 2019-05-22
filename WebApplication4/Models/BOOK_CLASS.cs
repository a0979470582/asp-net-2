using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

/// <summary>
/// This is NoSQL.
/// </summary>
namespace WebApplication4.Models
{
    public class BOOK_CLASS
    {
        private static List<BOOK_CLASS> bookClassList = new List<BOOK_CLASS>
        {
            new BOOK_CLASS() {BOOK_CLASS_ID="0",BOOK_CLASS_NAME="美食"},
            new BOOK_CLASS() {BOOK_CLASS_ID="1",BOOK_CLASS_NAME="旅遊"},
            new BOOK_CLASS() {BOOK_CLASS_ID="2",BOOK_CLASS_NAME="運動鍛鍊"},
            new BOOK_CLASS() {BOOK_CLASS_ID="3",BOOK_CLASS_NAME="親子遊戲"},
            new BOOK_CLASS() {BOOK_CLASS_ID="4",BOOK_CLASS_NAME="金融投資"},
            new BOOK_CLASS() {BOOK_CLASS_ID="5",BOOK_CLASS_NAME="心理學"}
        };

        public static List<BOOK_CLASS> GetData()
        {
            return bookClassList;
        }

        public static string GetIdWithName(string name)
        {
            foreach (BOOK_CLASS item in bookClassList)
            {
                if (item.BOOK_CLASS_NAME == name)
                    return item.BOOK_CLASS_ID;
            }
            return "";
        }

        public static string GetNameWithId(string id)
        {
            foreach (BOOK_CLASS item in bookClassList)
            {
                if (item.BOOK_CLASS_ID == id)
                    return item.BOOK_CLASS_NAME;
            }
            return "";
        }

        private BOOK_CLASS(string id, string name)
        {
            BOOK_CLASS_ID = id;
            BOOK_CLASS_NAME = name;
        }

        private BOOK_CLASS() { }//設定一個空的建構子,保留原本建立物件的方式

        public string BOOK_CLASS_ID { get; set; }

        public string BOOK_CLASS_NAME { get; set; }

 
    }
}