# PythonCoreConcept
Python 3.9 grammar implemented as a C# 9 library based on record nodes.

This is start of a Roslyn like parser / analyzer for Python code with grammar like 3.9.x implemented as a C# dotnet 5 project.
Possible extension later, will be a full core runtime system for a Python interpreter / compiler for the .net ecosystem.

You can parse sourcecode from EvalInput, FuncTypeInput & FileInput. SingleInput partialy work. UnitTest for happy path and started on 
UnitTests for valid SyntraxError handling in progress.

See UnitTest for documentation about setup of parsing of file.

We now have over 438 UnitTests written and passed. There are more on the way before i have confidence in the parser doing its job corectly.

Suite parsing seems to work now. It will work on unlimited levels of indent. Memory will limit it eventually, but it supports more than 100
levels that standard Python is limited to. For portability, do not exceed 100.

Missing now is TypeComment UnitTests and som Trivia handling in Tokenizer. Close to first package release for parser.
