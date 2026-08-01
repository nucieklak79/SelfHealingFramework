# Self-Healing Automation Framework

Developed by Norbert Ucieklak.

This project is a clean, local-first Proof of Concept (PoC) demonstrating a robust automated UI testing framework built in C#. It features a Self-Healing Locator module powered by an LLM integration layer. When UI locators break due to DOM changes, the framework catches the exception, analyzes the page source and target element description via LLM, and dynamically recovers the test execution with a repaired locator.

Technologies and libraries used:
- C#
- .NET 10.0
- Selenium WebDriver
- NUnit
- Moq

## Local Execution Guide

To build and run the test suite locally on your machine, execute the following commands in the root directory:

1. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

2. Build the solution:
   ```bash
   dotnet build
   ```

3. Run the unit and UI test projects independently:
   ```bash
   dotnet test SelfHealing.Tests.Unit/SelfHealing.Tests.Unit.csproj
   dotnet test SelfHealing.Tests.UI/SelfHealing.Tests.UI.csproj
   ```
