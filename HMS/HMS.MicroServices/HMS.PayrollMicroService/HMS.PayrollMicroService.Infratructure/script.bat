@echo off

dotnet ef database drop --context PayrollContext
dotnet ef migrations remove

dotnet ef migrations add First
dotnet ef database update