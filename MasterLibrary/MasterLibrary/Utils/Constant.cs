using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Utils
{
    public class ROLE
    {
        public static readonly string Admin = "Quản lý";
        public static readonly string Customer = "Khách hàng";

        
    }

    public class baseBook
    {
        public static readonly List<string> ListTheLoai = new List<string>
        {
            "Chính trị",
            "Khoa học",
            "Kinh tế",
            "Văn học",
            "Lịch sử",
            "Tiểu thuyết",
            "Tâm lý",
            "Sách thiếu nhi"
        };
    }

    public class Trouble
    {
        public static class STATUS
        {
            public static readonly string WAITTING = "Chờ giải quyết";
            public static readonly string DONE = "Đã giải quyết";
            public static readonly string CANCLE = "Đã huỷ";
        }
    }

    public class BookInBorrow
    {
        public static class STATUS
        {
            public static readonly string All = "Toàn bộ";
            public static readonly string Undue = "Chưa đến hạn trả";
            public static readonly string OutOfDay = "Quá hạn trả";
        }
    }
}
