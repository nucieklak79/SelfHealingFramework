# Test Plan: Self-Healing Automation Framework

## 1. Objective
To validate the architectural integrity and recovery logic of the self-healing locator mechanism across both isolated unit tests and live browser execution scenarios.

## 2. Scope of Testing
- **Core Healer Logic**: Verifying payload formatting and response parsing between the driver wrapper and the LLM abstraction layer.
- **UI Fallback Recovery**: Ensuring that intentional locator failures are intercepted, structurally repaired via LLM simulation, and transparently resumed without test termination.

## 3. Detailed Test Scenarios

### 3.1. Unit Tests (`SelfHealing.Tests.Unit`)
- **TC-01 (Healer Payload Validation)**: 
  - *Input*: `Healer.HealLocator` invoked with a simulated broken locator identifier, element semantic description, and dummy HTML snippet.
  - *Expected Result*: The `ILlmClient` mock receives the correct payload context and returns a valid replacement locator string (`//*[@id='repaired-id']`).

### 3.2. UI Tests (`SelfHealing.Tests.UI`)
- **TC-02 (Selenium Self-Healing Login Flow)**:
  - *Setup*: Initialize headless Chrome via Selenium Manager. Navigate to `https://www.saucedemo.com/`.
  - *Action*: Attempt to locate the username input using an intentionally broken ID (`user-name-changed-in-latest-release`).
  - *Expected Result*: 
    1. `NoSuchElementException` is thrown by standard Selenium find routine.
    2. `SelfHealingDriver` intercepts the exception and triggers the `Healer` module.
    3. Mock LLM analyzes the request and provides the fallback XPath (`//*[@id='user-name']`).
    4. The input field is successfully recovered, credentials (`standard_user` / `secret_sauce`) are entered, and login succeeds, landing on `inventory.html`.
