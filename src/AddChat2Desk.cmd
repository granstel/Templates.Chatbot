dotnet sln add GranSteL.Chatbot.Messengers.Chat2Desk/GranSteL.Chatbot.Messengers.Chat2Desk.csproj -s Messengers
dotnet sln add GranSteL.Chatbot.Messengers.Chat2Desk.Tests/GranSteL.Chatbot.Messengers.Chat2Desk.Tests.csproj -s Tests
dotnet add GranSteL.Chatbot.Api/GranSteL.Chatbot.Api.csproj reference GranSteL.Chatbot.Messengers.Chat2Desk/GranSteL.Chatbot.Messengers.Chat2Desk.csproj