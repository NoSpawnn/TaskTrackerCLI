# Sample solution for [Roadmap](https://roadmap.sh/projects/task-tracker)

## Features

- Store and manage tasks in JSON format
- View all tasks in a tabular format

## Usage

1. Clone this repo

```shell
$ git clone git@github.com:NoSpawnn/TaskTrackerCLI.git
```

2. Build

```shell
# Build an executable and place it in the current directory
$ dotnet publish TaskTrackerCLI.csproj -o .
```

3. Run

- The below commands can instead be prefixed with `dotnet run` if you prefer not the build the project

```shell
# Add a task
$ ./TaskTrackerCLI add "Buy food"

# Update
$ ./TaskTrackerCLI update 1 "Buy water"

# Delete
$ ./TaskTrackerCLI delete 1

# Set status
$ ./TaskTrackerCLI mark 1 todo
$ ./TaskTrackerCLI mark 1 done
$ ./TaskTrackerCLI mark 1 in-progress

# List tasks
$ ./TaskTrackerCLI list
$ ./TaskTrackerCLI list done
$ ./TaskTrackerCLI list in-progress
$ ./TaskTrackerCLI list todo
```
