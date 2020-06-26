dotnet sln add GranSteL.Chatbot.Messengers.Yandex/GranSteL.Chatbot.Messengers.Yandex.csproj -s Messengers
dotnet sln add GranSteL.Chatbot.Messengers.Yandex.Tests/GranSteL.Chatbot.Messengers.Yandex.Tests.csproj -s Tests
dotnet add GranSteL.Chatbot.Api/GranSteL.Chatbot.Api.csproj reference GranSteL.Chatbot.Messengers.Yandex/GranSteL.Chatbot.Messengers.Yandex.csproj