# Skybot

[![Build status](https://ma-vsts.visualstudio.com/Skybot/_apis/build/status/Skybot-Docker%20container-CI)](https://ma-vsts.visualstudio.com/Skybot/_build/latest?definitionId=7)


[![Deployment status](https://ma-vsts.vsrm.visualstudio.com/_apis/public/Release/badge/bb2300aa-d207-49d7-ba65-26338ad77a90/4/4)](https://ma-vsts.vsrm.visualstudio.com/_apis/public/Release/badge/bb2300aa-d207-49d7-ba65-26338ad77a90/4/4)

Skybot is a Web API built with .NET Core 2.1
It provides a service to process string queries for language understanding, currently working with [Language Understanding (LUIS)](https://www.luis.ai) and returns responses based on the intents trained by the model. It performs operations on the entities of recognized intents.

## Features

### Available Features:
* Recognize the language to translate to and the word/sentense queries for translation. Works with [Google Cloud Translation API](https://cloud.google.com/translate/docs/)

### Upcoming Features:
* Recognize commands for home automation (eg. turn off lights in living room)

## Related projects

List of related projects that are part of Skybot system:
* [Skybot Auth](https://github.com/malekatwiz/Skybot.Auth)
* [Texto API](https://github.com/malekatwiz/Texto.Api)
* [Skybot Functions](https://github.com/malekatwiz/Skybot.Functions)
* [Texto Sender Azure Function](https://github.com/malekatwiz/Texto.Sender.Function)
* [Skybot Text Guard Azure Function](https://github.com/malekatwiz/Skybot.Text.Guard)

Here's a list of other related projects where you can find inspiration for
creating the best possible README for your own project:

## [Licensing](/LICENSE)

This project is licensed under MIT License license.