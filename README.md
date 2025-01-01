<img src="https://github.com/TBG09/Resources/blob/1dd330da72d2d717a4940cd753de86cd8dcb8d2c/shrimple33.png" alt="Shrimple3" width="500">

# ShrimpleCmd - C# Console Application

- This is just a random console app for C# learning.
- Written by ChatGPT cuz i was too lazy
- **Linux support in the app is deprecated!**
- But building for linux should still work  

 ## Credits
 (https://github.com/TBG09)[Bluelist] - Programmer  
 (https://github.com/PolarT-py)[Polart] - Artist

## Prerequisites

Before you can build the application, make sure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (required to run and build the application)
- [Git](https://git-scm.com/downloads) (For cloning the repo, this is optional)

## How to Compile the Application

### Option 1: Clone the Repository
If you want to clone the repository using Git, follow these steps:

1. Open a Command Prompt (press `Win + R`, type `cmd`, and hit Enter).
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

## Building the Application

### Build Normally (Debug)

To build the application normally (in Debug mode):

1. Open a Command Prompt and navigate to the project directory if you haven’t already:
   ```bash
   cd ShrimpleCmd
   ```
2. Run the following command to restore dependencies and build the application:
   ```bash
   dotnet build
   ```

This will build the project in Debug mode. The compiled application will be in the `bin/Debug` directory.

### Build for Release

To build the application for release (optimized for production):

1. Open a Command Prompt and navigate to the project directory if you haven’t already:
   ```bash
   cd ShrimpleCmd
   ```
2. Run the following command to restore dependencies and build the application in Release mode:
   ```bash
   dotnet build -c Release
   ```

This will create a release build of your application in the `bin/Release/net8.0/` directory (adjust based on your .NET version).

### Running the Application

Once you have successfully built the application, you can run it using the following command:
```bash
dotnet run
```
Or you can run ```bin\build_type\net8.0\ShrimpleCmd.exe```
- **Replace build_type with the build type you used**

This will execute the application in the terminal.

