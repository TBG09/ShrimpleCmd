This is just a random console app for C# Learning.

Written by chatgpt cuz im lazy lol:
# C# Console Application Build Instructions

This repository contains a simple C# console application to help you learn and experiment with .NET. The instructions below will guide you through the process of building and running the application on both **Windows** and **Linux**.

## Prerequisites

### Windows

1. Install **.NET SDK**:
   - Download and install the .NET SDK from [Microsoft's .NET download page](https://dotnet.microsoft.com/download).

2. Verify Installation:
   - Open a terminal (Command Prompt or PowerShell) and run:
     ```bash
     dotnet --version
     ```
   - This should return the version number of the .NET SDK.

### Linux

For Linux, you need to add Microsoft's package repository and install the .NET SDK.

#### 1. Add the Microsoft Package Repository

For **Ubuntu** or **Debian**-based distributions:

```bash
# Download the Microsoft package signing key
wget https://packages.microsoft.com/config/ubuntu/20.04/prod.list
# Move the package list to the correct location
sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
# Update package list
sudo apt-get update
