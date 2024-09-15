@echo off
dotnet ef database drop --context PayrollContext
dotnet ef migrations remove