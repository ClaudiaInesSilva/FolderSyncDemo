# Folder Sync App

This project is a C# console application designed to synchronize two folders: a source folder and a replica folder. The replica folder will be a full, identical, and up-to-date copy of the source folder. This ensures that any changes in the source folder are mirrored in the replica folder according to a specified interval.

## Table of Contents

- [Software Requirements](#tools-used)
- [Features](#features)
- [Set Up](#set-up)
	- [Command-Line Arguments](#command-line-arguments)

## Tools Used

The following tools were used in the development of this project:

- **.NET SDK 8.0**: [Download from Link](https://www.l)
- **Visual Studio 2022**: [Download from Link](https://www.)

## Features

- One-Way Synchronization: Synchronization is performed one-way, from the source folder to the replica folder. The replica folder is updated to reflect any changes in the source folder.

- Periodic Synchronization: The synchronization is performed periodically based on an interval provided as a command-line argument.

- File Comparison Using MD5 Hashing: The app uses MD5 hashing to compare files. Only files that differ in content are copied from the source to the replica folder, ensuring that the replica is an accurate reflection of the source.

- Logging:

	- The app logs file operations such as copying and deletion.
	- Logs are written to both the console and a log file specified by the user.
	- Errors during the synchronization process are captured and logged.

- Folder Structure Preservation: The app ensures that the folder structure of the source is replicated in the replica folder.

- Command-Line Interface: The app is fully controlled via command-line arguments, allowing users to specify the source path, replica path, synchronization interval, and log file path.


## Set Up

Clone the repository to your local machine using the following command:

```
git clone https://github.com/ClaudiaInesSilva/FolderSyncDemo.git
```

Build the project:

```
dotnet build
```

### Command-Line Arguments

The app takes four command-line arguments:

- ```Source Path```: The path to the source folder that you want to synchronize.
- ```Replica Path```: The path to the replica folder where the source will be mirrored.
- ```Log File Path```: The path to the log file where all operations will be logged.
- ```Synchronization Interval```: The interval (in seconds) at which synchronization will be performed.

**Example Usages:**
```
dotnet run -- <Source Path> <Replica Path> <Log File Path> <Synchronization Interval>
```

**Example Command:**
```
dotnet run -- "C:\SourceFolder" "C:\ReplicaFolder" "C:\Logs\syncLog.txt" "30"
```

In this example, the app will:

- Synchronize the contents of C:\SourceFolder to D:\ReplicaFolder.
- Log all operations to C:\Logs\syncLog.txt.
- Perform synchronization every 30 seconds.

