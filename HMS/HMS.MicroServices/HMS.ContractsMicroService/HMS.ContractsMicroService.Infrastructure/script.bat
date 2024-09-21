@echo off

dotnet ef migrations remove --context ContractContext
dotnet ef migrations remove --context WorkHoursContext

dotnet ef migrations add 1 --context ContractContext
dotnet ef migrations add 1 --context WorkHoursContext


dotnet ef database update --context ContractContext
dotnet ef database update --context WorkHoursContext