# Speedruns.Backend

## This repository contains the backend for my speedrunning leaderboards webapp. This is a fun project to further learn C#/.NET and build upon those skills.

## The backend will consist primarily of C#/ASP.NET following the repository pattern.

## Technologies

- ASP.NET (web API framework)
- Entity Framework (ORM)
- xUnit (unit testing framework)
- NSubstitute (mocking library)
- Auth0 (oAuth)

## General Database Schema
* User (1 to many w/ Runs) - UserName, ImageUrl, YoutubeLink, TwitchLink, Runs
* Game (1 to many w/ Runs) - Name, ImageUrl, ReleaseYear, Series, Players, RunsPublished, Runs
* Run (1 to many w/ User, Game, Console and Comment) - Date, Console, User, Game, Comment, Time
* Comment (1 to many w/ Runs) - Date, Text, Run, User
* Console (1 to many w/ Runs, many to many with Game) - Name
* GameConsole (junction table for Game and Console) - Game, Console
* Series (1 to many w/ Games) - Name, Players

![image](https://github.com/Splix1/Speedruns.Backend/assets/86242483/3c0ef3a1-30cd-4831-8a0e-f5f3fc344635)
