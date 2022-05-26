# Templates.Chatbot
[![NuGet](https://buildstats.info/nuget/GranSteL.Templates.Chatbot)](https://www.nuget.org/packages/GranSteL.Templates.Chatbot)

Template of chat bot for messengers

Install
-------
It's available via dotnet new:
`dotnet new -i GranSteL.Templates.Chatbot`
and then `dotnet new gsl.chatbot.full -n MyChatBot -o ./ --allow-scripts yes` at the required folder. You will get projects for all supported channels: Telegram, Yandex, Marusia, Sber. You can specify required channels, e.g. Telegram: `dotnet new gsl.chatbot.full -n MyChatBot -o ./ --telegram --allow-scripts yes`

Allowed channels:
* -t, --telegram
* -y, --yandex
* -m, --marusia
* -s, --sber

### Bot examples:
- https://github.com/granstel/FillInTheTextBot (https://dialogs.yandex.ru/store/skills/12ef2083-sochinyal)
