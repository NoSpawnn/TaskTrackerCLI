# Sample solution for [Roadmap](https://roadmap.sh/projects/task-tracker)

## Features
- Store and manage tasks in JSON format
- View all tasks in a tabular format

## Usage

1. Clone this repo

```shell
$ git clone git@github.com:NoSpawnn/TaskTrackerCLI.git
```

2. Run

- You can either run using the `dotnet` CLI or build the project and run the executable

```shell
# Dotnet CLI
$ dotnet run

# Build then run
$ dotnet publish TaskTrackerCLI.csproj -o .
$ ./TaskTrackerCLI
```

3. Arguments
- This message can be seen at the CLI by passing no/invalid arguments
```
add <description> - add a todo with the specified description
update <id> <description> - update todo with the specified ID
delete <id> - delete todo with the specified ID

list - list all todos
list <done | todo | in-progress> - list todos with the specified status

mark-in-progress <id> - mark todo with the specified ID as 'in progress'
mark-done <id> - mark todo with the specified ID as 'done'
```
