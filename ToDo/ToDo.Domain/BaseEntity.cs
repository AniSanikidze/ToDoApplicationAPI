namespace ToDo.Application
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public EntityStatuses Status { get; set; }
    }

    public enum EntityStatuses
    {
        Active,
        Deleted
    }
}
