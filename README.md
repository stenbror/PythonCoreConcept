# PythonCoreConcept
Python 3.9 grammar implemented as a C# 9 library based on record nodes.

This is start of a Roslyn like parser / analyzer for Python code with grammar like 3.9.x implemented as a C# dotnet 5 project.
Possible extension later, will be a full core runtime system for a Python interpreter / compiler for the .net ecosystem.

You can parse sourcecode from EvalInput, FuncTypeInput & FileInput. SingleInput partly work. UnitTest for happy path and started on 
UnitTests for valid SyntraxError handling in progress.

See UnitTest for documentation about setup of parsing of file
