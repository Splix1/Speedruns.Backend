# Speedruns.Backend

## This repository contains the backend for my [speedrun.com](https://www.speedrun.com/) clone. This is a fun project to further learn C#/.NET and build upon those skills.

## The backend will consist primarily of C#/ASP.NET following the repository pattern using the Entity Framework.

## General Database Schema:
* User (1 to many w/ Runs) - UserName, ImageUrl, YoutubeLink, TwitchLink, Runs
* Game (1 to many w/ Runs) - Name, ImageUrl, ReleaseYear, Series, Players, RunsPublished, Runs
* Run (1 to many w/ User, Game, Console and Comment) - Date, Console, User, Game, Comment, Time
* Comment (1 to many w/ Runs) - Date, Text, Run, User
* Console (1 to many w/ Runs, many to many with Game) - Name
* GameConsole (junction table for Game and Console) - Game, Console

