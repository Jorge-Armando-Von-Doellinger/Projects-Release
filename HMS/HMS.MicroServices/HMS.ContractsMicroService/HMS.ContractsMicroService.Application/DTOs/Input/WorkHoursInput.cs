﻿namespace HMS.ContractsMicroService.Application.DTOs.Input
{
    public class WorkHoursInput
    {
        public TimeOnly EntryTime { get; set; }
        public TimeOnly IntervalStartTime { get; set; }
        public TimeOnly IntervalEndTime { get; set; }
        public TimeOnly ExitTime { get; set; }
    }
}