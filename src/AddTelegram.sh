#!/bin/sh
dotnet sln add GranSteL.Chatbot.Messengers.Telegram/GranSteL.Chatbot.Messengers.Telegram.csproj -s Messengers
dotnet sln add GranSteL.Chatbot.Messengers.Telegram.Tests/GranSteL.Chatbot.Messengers.Telegram.Tests.csproj -s Tests
dotnet add GranSteL.Chatbot.Api/GranSteL.Chatbot.Api.csproj reference GranSteL.Chatbot.Messengers.Telegram/GranSteL.Chatbot.Messengers.Telegram.csproj