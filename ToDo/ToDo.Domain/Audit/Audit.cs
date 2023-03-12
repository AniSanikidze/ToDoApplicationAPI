using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Audit
{
    public class Audit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OperationTypes operationType { get; set; }
        public string OldResult { get; set; }
        public string NewResult { get; set; }
        public string TableName { get; set; }
        public string KeyValues { get; set; }
    }

    public enum OperationTypes
    {
        Created,
        Updated,
        Deleted,
        MarkedAsDone
    }
}
