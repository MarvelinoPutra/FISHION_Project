# 🐟 Fishion - 2D Merge Puzzle Game

![Unity](https://img.shields.io/badge/Unity-2022.3%2B-black?style=flat-square&logo=unity)
![Platform](https://img.shields.io/badge/Platform-Android%20%7C%20WebGL-blue?style=flat-square)
![Language](https://img.shields.io/badge/Language-C%23-green?style=flat-square)

**Fishion** is a Suika-style 2D merge puzzle game developed in Unity. Drop and merge identical fishes to evolve them into bigger ones, achieve the highest score, and strategize to prevent the tank from overflowing!

## 🎮 Play the Game
Play it directly on your browser or download the Android APK:
> **[Play Fishion on itch.io](https://kyuuxen.itch.io/fishion-thegame)**

---

## ✨ Key Features & Technical Highlights

This project was built with a strong focus on clean code, performance, and cross-platform compatibility:

- **Physics-Optimized Merge System:** Engineered collision logic using `GetInstanceID()` to prevent double-execution and infinite loops (a common crash cause in merge games on Android devices). Spawn velocity is strictly managed to prevent explosive overlaps.
- **Cross-Platform Input & Dynamic Clamping:** Seamlessly supports both PC (Mouse/WebGL) and Mobile (Touch/Android). The dropping area dynamically clamps based on screen ratio (calculating between 16:9 and 21:9 via `Mathf.InverseLerp`).
- **Persistent Data Management:** Utilizes standard JSON Serialization (`JsonUtility`) to store High Scores and first-time Tutorial states locally (`Application.persistentDataPath`).
- **Logarithmic Audio Control:** UI volume sliders are mapped logarithmically to Unity's `AudioMixer` to simulate natural human hearing curves.

## 🛠️ Architecture Overview

The codebase implements several core programming patterns to keep the architecture clean and scalable:
- **Singleton Pattern:** Used for core managers (`GameManager`, `SaveManager`, `SoundManager`) combined with `DontDestroyOnLoad` for seamless cross-scene data flow.
- **Event-Driven UI:** Utilizes C# `Action` (`OnGameOver`, `OnMaxLevelReached`) to decouple core game logic from UI updates.

## 📂 Project Structure (Core Scripts)
- `SaveManager.cs`: Handles JSON serialization and file I/O operations for player data.
- `PlayerInput.cs`: Manages platform-agnostic inputs and mathematical screen boundaries.
- `GameManager.cs`: Controls the main game loop, timescale manipulation, and event broadcasting.
- `MenuManager.cs`: Handles dynamic "Tap-Tap" gallery tutorial integrated with the local save state.

## 🚀 Getting Started (For Developers)

To run this project locally on your machine:

1. Clone this repository:
   ```bash
   git clone [https://github.com/USERNAME_KAMU/Fishion.git](https://github.com/USERNAME_KAMU/Fishion.git)
Open Unity Hub, click Add, and select the cloned repository folder.

Ensure you are using Unity version 2022.3 LTS (or your current version).

Open MainMenu in the Scenes folder and press Play!

# 👨‍💻 Developed By
# Game Designer
Farhan Marchilian Sudarno
Game Technology Student | as Game Designer

# Game Artist
Septiana Anggi Pratiwi
Game Technology Student | as Game Artist

Wildan Aqila Fikri
Game Technology Student | as Game Artist



# Game Programmer
Fakhri Ibadurrohman
Game Technology Student | as Game Programmer

Najma Humairah Hardiman
Game Technology Student | as Game Programmer

Marvelino Rafael Junior Putra
Game Technology Student | as Lead Programmer
