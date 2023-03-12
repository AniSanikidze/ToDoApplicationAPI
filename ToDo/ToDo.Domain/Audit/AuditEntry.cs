using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Audit
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldResults { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewResults { get; } = new Dictionary<string, object>();
        public OperationTypes OperationType { get; set; }
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.TableName = TableName;
            audit.Date = DateTime.Now;
            audit.KeyValues = JsonConvert.SerializeObject(KeyValues);
            audit.operationType = OperationType;
            audit.OldResult = OldResults.Count == 0 ? null : JsonConvert.SerializeObject(OldResults); // In .NET Core 3.1+, you can use System.Text.Json instead of Json.NET
            audit.NewResult = NewResults.Count == 0 ? null : JsonConvert.SerializeObject(NewResults);
            return audit;
        }
    }
}
