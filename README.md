# XMLtoJSONConverter

This is a Web API + React XMLtoJSON Converter. 

The WebAPI exposes a POST endpoint /api/ConvertXMLtoJSON. 
The endpoint accepts a file and the filenmae as parameters
The file is saved to a specified location in the background.

The whole application can be accessed at https://xmltojsonconverter.fly.dev

Hangfire job implemented that processes file conversion in the background 
Endpoint that accepts a file name and returns the file itself if existent.
