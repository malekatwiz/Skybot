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

List of projects work with Skybot:
* [Skybot Auth](https://github.com/malekatwiz/Skybot.Auth)
* [Texto API](https://github.com/malekatwiz/Texto.Api)
* [Texto Sender Azure Function](https://github.com/malekatwiz/Texto.Sender.Function)
* [Skybot Text Guard Azure Function](https://github.com/malekatwiz/Skybot.Text.Guard)

Here's a list of other related projects where you can find inspiration for
creating the best possible README for your own project:

## Licensing

This project is licensed under MIT License license. 

MIT License

Copyright (c) 2019 Malek Atwiz

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.