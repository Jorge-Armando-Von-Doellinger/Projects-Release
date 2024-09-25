using Nuget.Contracts.Inputs;

namespace Nuget.Contracts.Outputs
{
    public sealed class WorkHoursOutput : WorkHoursInput
    {
        public Guid ID { get; set; }
    }
}
