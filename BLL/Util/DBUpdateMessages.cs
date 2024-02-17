using System.Collections;
using System.Collections.Generic;

namespace BLL.Util
{
    public static class DBUpdateMessages
    {
        private static readonly Dictionary<DBUpdateStatus, string> Map = new () {
            { DBUpdateStatus.Success, "Operation successed" },
            { DBUpdateStatus.DuplicateEntry, "Can't duplicate entry" },
            { DBUpdateStatus.FailUnkown, "Operation fail with unkown error" }
        };

        public static string GetMsg(this DBUpdateStatus status) => Map[status];
    }
}
