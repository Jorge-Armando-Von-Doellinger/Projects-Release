@echo off
rmdir /s /q Migrations


dotnet ef migrations add First2 --context DefaultContext
dotnet ef database update --context DefaultContext
