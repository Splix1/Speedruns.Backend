# Speedruns.Backend

## This repository contains the backend for my [speedrun.com](https://www.speedrun.com/) clone. This is a fun project to further learn C#/.NET and build upon those skills.

## The backend will consist primarily of C#/ASP.NET using the Entity Framework ORM.

## General Database Schema:
* User - UserName, ImageUrl, YoutubeLink, TwitchLink
* Game - Name, ImageUrl, ReleaseYear, Series, Players, RunsPublished
* Run - Date, Console, User, Game
* Comment - Date, Text, Run, User
* Console - Name
* GameConsoles - Game, Console
