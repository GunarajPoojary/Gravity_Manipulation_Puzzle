# Gravity Manipulation Puzzle

A Unity-based puzzle game where players manipulate gravity to solve challenging levels. Collect cubes, avoid free-fall, and beat the timer by creatively shifting gravity directions.

## 🎮 Gameplay Overview

- Collect all available collectible cubes within the time limit.
- Shift gravity dynamically to navigate tricky terrains.
- Avoid free-fall events and time-outs that result in game over.
- Enjoy a hologram preview system for visualizing gravity shifts before applying them.

## 🚀 Features

- **Gravity Shift System**: Preview and apply gravity direction changes based on player input.
- **Free-Fall Detection**: Detect when the player falls too far and trigger a game over.
- **Timer Mechanism**: Countdown timer with pause/resume capability and game over handling.
- **Collectible Cubes**: Collectible objects that increment the score and drive game progression.
- **Safe Area UI Handling**: Automatically adjust UI to respect device safe areas (especially for mobile).
- **Responsive UI**: Displays collected cubes count and remaining time.
- **Game Events Architecture**: Decoupled event system for easier event-driven logic.

## 🧱 Core Scripts Overview

| Script | Purpose |
| ------ | ------- |
| `GravityShift.cs` | Handles gravity direction input, preview hologram, and applies gravity changes. |
| `PlayerController.cs` | Manages player movement, input handling, jumping, and animations. |
| `GameTimer.cs` | Manages the countdown timer and raises events on time updates or time end. |
| `Collector.cs` | Handles the logic for collecting cubes and triggering completion event. |
| `GameManager.cs` | Manages overall game state, including game over and win conditions. |
| `FreeFallDetector.cs` | Detects if the player enters free fall and triggers an event. |
| `SafeAreaPanel.cs` | Adapts UI layout to device safe area. |
| `UIManager.cs` | Updates UI elements such as timer and collected cubes. |
| `GameEvents.cs` | ScriptableObject-based event system for event-driven architecture. |
| `HologramCube.cs` | Represents a collectible cube with hologram behavior. |
| `ICollectible.cs` | Interface for collectible items. |

## ⚡ Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/GunarajPoojary/Gravity_Manipulation_Puzzle.git
    ```

2. Open the project in Unity (version 2020.3 LTS or newer recommended).

3. Open the scene `Assets/Scenes/GameScene.unity`.

4. Ensure input actions are properly set up (via the Input System package).

## 🛠️ Usage

- Move the player using keyboard/gamepad controls.
- Use Arrow Keys to preview and Enter key apply gravity shifts.
- Collect all hologram cubes before time runs out.
- Avoid free-fall to prevent game over.
