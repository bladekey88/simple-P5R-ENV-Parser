# ENVParser
[![.NET](https://github.com/bladekey88/simple-P5R-ENV-Parser/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bladekey88/simple-P5R-ENV-Parser/actions/workflows/dotnet.yml)

This is a small script that will output the values from an ENV to a json or csv file with labels (where known). This work is based on the 010 templates created by the P5R community, and references this page https://amicitia.miraheze.org/wiki/Persona_5_Royal/ENV/Structure. 

## Usage
To run, either drop an ENV file onto the executable and the csv will be generated alongside it, or from the commandline, pass the full env path as an argument. Make sure that the file extension (.ENV) is included.

ENVParser.exe -path-to-env-file <optionalfileformat>

you can supply either csv or json as a file format
e.g.
- ENVParser.exe "C:\users\Persona5\Modding\ENV\ENV2001_001_010.ENV" csv
- ENVParser.exe "C:\users\Persona5\Modding\ENV\ENV2001_001_010.ENV" json 

_(json is the default if no file format is provided)_


You can also drop a converted JSON (.ENV.json) back to ENV by droppping it on
- ENVParser.exe "C:\users\Persona5\Modding\ENV\ENV2001_001_010.ENV.json"  

