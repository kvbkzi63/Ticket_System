using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForDemo.Enum
{
    public class EnumUtility
    {
        public enum Severity
        {
            low,
            mid,
            height
        }
        public enum Priority
        {
            low,
            mid,
            height
        }
        public enum TicketType
        {
            Bug,
            Feature_Request,
            Test_Case
        }
        public enum TicketStatus
        {
            New,
            Resolved
        }

        public enum UserRoles
        {
            QA,
            RD,
            PM,
            ADMIN
        }
    }
}
