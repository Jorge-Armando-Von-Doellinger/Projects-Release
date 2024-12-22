using HMS.ContractsMicroService.Core.Entity.Base;
using HMS.ContractsMicroService.Core.Extensions;

namespace HMS.ContractsMicroService.Core.Entity
{
    public sealed class WorkHours : EntityBaseWithId
    {
        public TimeOnly EntryTime { get; set; }
        public TimeOnly IntervalStartTime { get; set; }
        public TimeOnly IntervalEndTime { get; set; }
        public TimeOnly ExitTime { get; set; }
        public void Update(WorkHours valuesToUpdate)
        {
            base.Update();
            this.Replacer(valuesToUpdate, true);
        }
    }
}
