FROM mcr.microsoft.com/dotnet/core/runtime:3.1
COPY ShoppingCart.Presentation.ConsoleApp/bin/Release/netcoreapp3.1/publish/ app/

ENTRYPOINT ["dotnet", "app/ShoppingCart.Presentation.ConsoleApp.dll"]