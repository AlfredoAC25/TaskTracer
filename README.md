# TaskTracer

A simple Command-line program in C# to track your tasks and manage your to-do list.

## Content table
- [Features](#Features)
- [Requirements](#Requirements)
- [Installation](#Installation)
- [Usage](#Usage)
- [Project Roadmap](#Project Roadmap)

## Features
- ✅Add, Update, and Delete tasks
- ✅Mark a task as in progress or done
- ✅List all tasks
- ✅List all tasks that are done
- ✅List all tasks that are not done
- ✅List all tasks that are in progress

## Requirements
- .NET SDK: `10.0`
- SO: Windows / Linux / macOS

## Installation
Clonar e instalar dependencias:

```bash
git clone git@github.com:AlfredoAC25/TaskTracer.git
dotnet restore
```

## Usage
To add a new task
```bash
dotnet run add "Buy groceries"
```
To update and delete tasks
```bash
dotnet run update 1 "Buy groceries and cook dinner"
dotnet run delete 1
```
To mark a task as in-progress or done
```bash
dotnet run mark-in-progress 1
dotnet run mark-done 1
```
To list all tasks
```bash
dotnet run list
```
To list tasks by status
```bash
dotnet run list done
dotnet run list todo
dotnet run list in-progress
```

## Project Roadmap
This project is part of a set of practice projects from the C# roadmap.
https://roadmap.sh/projects/task-tracker