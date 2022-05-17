#!/bin/sh
dotnet sln add GranSteL.Chatbot.Messengers.Marusia/GranSteL.Chatbot.Messengers.Marusia.csproj -s Messengers
dotnet sln add GranSteL.Chatbot.Messengers.Marusia.Tests/GranSteL.Chatbot.Messengers.Marusia.Tests.csproj -s Tests
dotnet add GranSteL.Chatbot.Api/GranSteL.Chatbot.Api.csproj reference GranSteL.Chatbot.Messengers.Marusia/GranSteL.Chatbot.Messengers.Marusia.csproj