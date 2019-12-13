# Templates.Chatbot
[![Build status](https://ci.appveyor.com/api/projects/status/qg4xlrtr32vxmrad?svg=true)](https://ci.appveyor.com/project/granstel/templates-chatbot/)
[![NuGet](https://buildstats.info/nuget/GranSteL.Templates.Chatbot)](https://www.nuget.org/packages/GranSteL.Templates.Chatbot)

Template of chat bot for messengers

Install
-------
It's available via dotnet new:
`dotnet new -i GranSteL.Templates.Chatbot`
and then `dotnet new gsl.chatbot.full -n MyChatBot -o .\ --allow-scripts yes` at the required folder. You will get projects for all supported channels: Telegram, Yandex.Dialogs, Chat2Desk. You can specify required channels, e.g. Telegram: `dotnet new gsl.chatbot.full -n MyChatBot -o .\ -t --allow-scripts yes`
