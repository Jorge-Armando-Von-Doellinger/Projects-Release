using HMS.ContractsMicroService.Core.Entity.Base;

namespace HMS.ContractsMicroService.Core.Entity
{
    public sealed class WorkHours : EntityBaseWithId
    {
            public TimeOnly EntryTime { get; set; }
            public TimeOnly IntervalStartTime { get; set; }
            public TimeOnly IntervalEndTime { get; set; }
            public TimeOnly ExitTime { get; set; }
    }
}
