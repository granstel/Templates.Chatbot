dotnet sln add GranSteL.Chatbot.Messengers.Sber/GranSteL.Chatbot.Messengers.Sber.csproj -s Messengers
dotnet sln add GranSteL.Chatbot.Messengers.Sber.Tests/GranSteL.Chatbot.Messengers.Sber.Tests.csproj -s Tests
dotnet add GranSteL.Chatbot.Api/GranSteL.Chatbot.Api.csproj reference GranSteL.Chatbot.Messengers.Sber/GranSteL.Chatbot.Messengers.Sber.csproj