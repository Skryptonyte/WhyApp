# WhyApp

A simple chatting app made as part of a project for DBS Lab subject in Manipal.

## Backend


The backend is a flask-socketio server exposing socketio and http endpoints and working with Oracle SQL Server under the hood using the cx_Oracle module.

Install required dependencies as follows

` pip install -r WhyApp_backend/requirements.txt ` 

NOTE: Very important to install these specific versions! Otherwise you will face compatibility issues as none of the C# socketio client libraries support socket.io v5 as of the time of typing this README. 

To run the server:

` python <scriptLocation> <username> <password> `

NOTE: username and password refers to the credentials on the Oracle SQL installation

SQL script is provided to be run manually as well to do the necessary initialization. (TODO: Automate within the python script maybe)

## Frontend

The frontend is a C# WinForms application working on top of .NET 4.7.2 framework and developed in VS 2019. 
It uses the built in System.Net.Http library for HTTP connections.

Third Party Dependencies: 
* Socket.io-client-csharp ( https://github.com/doghappy/socket.io-client-csharp ) for socket.io v4 connections 
* NewtonSoft ( https://github.com/JamesNK/Newtonsoft.Json ) for handling JSON data




