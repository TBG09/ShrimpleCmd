This is just a random console app for C# Learning.
Written by chatgpt cuz i lazy

# Net 8.0 needed to run and build!!1!1! #

# ShrimpleCmd - C# Console Application

## How to Compile the Application

### Option 1: Clone the Repository
If you want to clone the repository using Git, follow these steps:

1. Open a terminal/command prompt.
2. Run the following command to clone the repository:
   ```bash
   git clone https://github.com/TBG09/ShrimpleCmd.git
   ```
3. Navigate into the project directory:
   ```bash
   cd ShrimpleCmd
   ```

### Option 2: Download ZIP
Alternatively, you can download the repository as a ZIP file:

1. Go to the repository page on GitHub: [https://github.com/TBG09/ShrimpleCmd](https://github.com/TBG09/ShrimpleCmd)
2. Click the green "Code" button and select "Download ZIP."
3. Extract the ZIP file to your desired location.

## Prerequisites

Before you can build the application, make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (make sure to install the version compatible with the project)
  
## Building the Application

### Build Normally (Debug)

To build the application normally (in Debug mode):

1. Open a terminal/command prompt.
2. Navigate to the project directory if you haven’t already:
   ```bash
   cd ShrimpleCmd
   ```
3. Run the following command to restore dependencies and build the application:
   ```bash
   dotnet build
   ```

This will build the project in Debug mode. The compiled application will be in the `bin/Debug` directory.

### Build for Release

To build the application for release (optimized for production):

1. Open a terminal/command prompt.
2. Navigate to the project directory if you haven’t already:
   ```bash
   cd ShrimpleCmd
   ```
3. Run the following command to restore dependencies and build the application in Release mode:
   ```bash
   dotnet publish -c Release -r win-x64 --self-contained
   ```

This will create a release build of your application in the `bin/Release/net5.0/win-x64/publish/` (adjust based on your .NET version) directory.

### Running the Application

Once you have successfully built the application, you can run it using the following command:
```bash
dotnet run
```

This will execute the application in the terminal.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
