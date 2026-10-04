# 🐟 Fishion

![Unity](https://img.shields.io/badge/Unity-2022.3%2B-black?style=flat-square&logo=unity)
![Platform](https://img.shields.io/badge/Platform-Android-blue?style=flat-square&logo=android)
![Language](https://img.shields.io/badge/Language-C%23-green?style=flat-square)

**Fishion** is a Suika-style 2D merge puzzle game developed in Unity specifically for Android devices. Drop and merge identical fishes to evolve them into bigger species, achieve the highest score, and strategize to prevent the tank from overflowing.

> [!TIP]
> **Play the Game**
> Download the Android APK and play it directly on your device:
> [🎮 Get Fishion on itch.io](https://kyuuxen.itch.io/fishion-thegame)

## Technical Highlights

This project was built with a strong focus on clean code architecture, performance optimization, and mobile-first experience:

- **Physics-Optimized Merge System:** Engineered collision logic utilizing `GetInstanceID()` to prevent double-execution and infinite loops (a common cause of memory crashes in Android merge games). Spawn velocity is strictly managed to prevent explosive physics overlaps.
- **Mobile-Optimized Input & Dynamic Clamping:** Designed specifically for touch interfaces. The item-dropping area dynamically clamps based on the Android device's screen ratio (calculating boundaries between 16:9 and 21:9 via `Mathf.InverseLerp`).
- **Persistent Data Management:** Utilizes standard JSON Serialization (`JsonUtility`) to securely store High Scores and first-time Tutorial states locally via `Application.persistentDataPath`.
- **Logarithmic Audio Control:** UI volume sliders are mapped logarithmically to Unity's `AudioMixer` to accurately simulate natural human hearing curves.

## Architecture Overview

The codebase implements several core programming patterns to keep the architecture scalable and easy to maintain:

- **Singleton Pattern:** Used for core managers (`GameManager`, `SaveManager`, `SoundManager`) combined with `DontDestroyOnLoad` for seamless cross-scene data flow.
- **Event-Driven UI:** Utilizes C# delegates and `Action` (`OnGameOver`, `OnMaxLevelReached`) to completely decouple core game logic from UI updates.

### Core Scripts
- `SaveManager.cs`: Handles JSON serialization and file I/O operations for player data.
- `PlayerInput.cs`: Manages Android touch inputs and mathematical screen boundaries.
- `GameManager.cs`: Controls the main game loop, timescale manipulation, and event broadcasting.
- `MenuManager.cs`: Handles the dynamic "Tap-Tap" gallery tutorial integrated with the local save state.

## Getting Started

To run or modify this project locally on your machine:

1. Clone this repository:
   ```bash
   git clone [https://github.com/MarvelinoPutra/FISHION_Project.git](https://github.com/MarvelinoPutra/FISHION_Project.git)
Open Unity Hub, click Add, and select the cloned repository folder.

Ensure you are using Unity version 2022.3 LTS (or your currently installed compatible version).

Navigate to the Scenes folder, open MainMenu, and press Play.

[!IMPORTANT]
Since this project relies on specific screen-ratio clamping, ensure your Build Target is set to Android in the Build Settings, and your Game view is set to a mobile portrait resolution (e.g., 1080x1920 or 16:9 Portrait).

👨‍💻 Developed By
Game Designer

Farhan Marchilian Sudarno — Game Technology Student

Game Artists

Septiana Anggi Pratiwi — Game Technology Student

Wildan Aqila Fikri — Game Technology Student

Game Programmers

Fakhri Ibadurrohman — Game Technology Student

Najma Humairah Hardiman — Game Technology Student

Marvelino Rafael Junior Putra — Game Technology Student | Lead Programmer
