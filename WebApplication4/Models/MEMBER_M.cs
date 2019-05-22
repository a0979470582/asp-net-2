using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This is NoSQL.
/// </summary>
namespace WebApplication4.Models
{   
    public class MEMBER_M
    {        
        private static List<MEMBER_M> memberList = new List<MEMBER_M>
        {
            new MEMBER_M(){USER_ID="0",USER_CNAME="美食家"},
            new MEMBER_M(){USER_ID="1",USER_CNAME="旅遊達人"},
            new MEMBER_M(){USER_ID="2",USER_CNAME="健身教練"},
            new MEMBER_M(){USER_ID="3",USER_CNAME="保母"},
            new MEMBER_M(){USER_ID="4",USER_CNAME="財務長"},
            new MEMBER_M(){USER_ID="5",USER_CNAME="諮商師"}
        };

        public static List<MEMBER_M> GetData()
        {
            return memberList;
        }


        public static string GetNameWithId(string id)
        {
            foreach(MEMBER_M item in memberList)
            {
                if (item.USER_ID == id)
                    return item.USER_CNAME;
            }
            return "";
        }

        public MEMBER_M(string id, string name)
        {
            USER_ID = id;
            USER_CNAME = name;
        }

        public MEMBER_M() { }//設定一個空的建構子,保留原本建立物件的方式

        public string USER_ID { get; set; }

        public string USER_CNAME { get; set; }
    }
}