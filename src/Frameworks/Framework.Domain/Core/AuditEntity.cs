using System;

namespace Framework.Domain.Core
{
    public abstract class EntityAudit:IEntityAudit
    {
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }

    public interface IEntityAudit
    {
        DateTime CreatedAt { get; set;  }
        int CreatedBy { get; set;  }
        DateTime UpdatedAt { get; set; }
        int UpdatedBy { get; set; }
        bool IsDeleted { get; set; }
        DateTime DeletedAt { get; set; }
    }
}