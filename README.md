# ENVParser
[![.NET](https://github.com/bladekey88/simple-P5R-ENV-Parser/actions/workflows/dotnet.yml/badge.svg)](https://github.com/bladekey88/simple-P5R-ENV-Parser/actions/workflows/dotnet.yml)

This is a small script that will output the values from an ENV to a csv file with labels (where known). This work is based on the 010 templates created by the P5R community. 

## Usage
To run, either drop an ENV file onto the executable and the csv will be generated alongside it, or from the commandline, pass the full env path as an argument. Make sure that the file extension (.ENV) is included.

e.g.
ENVParser.exe -path-to-env-file
