# Templates.Chatbot
[![Build status](https://ci.appveyor.com/api/projects/status/qg4xlrtr32vxmrad?svg=true)](https://ci.appveyor.com/project/granstel/templates-chatbot/)
[![NuGet](https://buildstats.info/nuget/GranSteL.Templates.Chatbot)](https://www.nuget.org/packages/GranSteL.Templates.Chatbot)

Template of chat bot for messengers

Install
-------
It's available via dotnet new:
`dotnet new -i GranSteL.Templates.Chatbot`
and then `dotnet new gsl.chatbot.full -n MyChatBot` at the required folder. You will get projects for all supported channels: Telegram, Yandex.Dialogs, Chat2Desk. You need to add the reference to required channels at main project, e.g. `dotnet add MyChatBot.Api reference MyChatBot.Messengers.Telegram`
