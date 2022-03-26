using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ForDemo.Enum;

namespace ForDemo.Models
{
    public class TicketRecordViewModel
    {
        public int Ticket_Seq { get; set; }
        [DisplayName("主旨　")]
        public string Ticket_Title { get; set; }
        [DisplayName("內容描述　")]
        public string Ticket_Desc { get; set; }
        [DisplayName("分類　")]
        public EnumUtility.TicketType Ticket_Type { get; set; }
        [DisplayName("優先級　")]
        public EnumUtility.Priority Priority { get; set; }
        [DisplayName("嚴重性　")]
        public EnumUtility.Severity Severity { get; set; }
        [DisplayName("處理狀態　")]
        public EnumUtility.TicketStatus Ticket_Status { get; set; }
        public string Create_User { get; set; }
        public string Create_Time { get; set; }
        public string Modified_User { get; set; }
        public string Modified_Time { get; set; }

    }
}
