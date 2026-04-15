# Project Title: Habit Tracking Game
A Unity-based research tool designed to gamify habit formation and export behavioral data for analysis.

## 📁 Repository Structure
This repository is organized into three main components:

```
├── HabitGame/          # Unity Project (Source)
├── Json-Explorer/      # Python Data Scripts
└── WebGame/            # WebGL Build Files
```

## 🎮 Getting Started (Developers)
**Prerequisites**
- **Unity Version:** 6000.0.60f1
- **Modules:** WebGL Build Support

**Setup**
1. Clone the repository:

| Bash | 
| :--- | 
| ```git clone https://github.com/YourUsername/YourRepoName.git``` |
2. Open **Unity Hub** and add the ```HabitGame``` folder.
3. Ensure your Build Settings are set to **WebGL**.

## 📊 Research & Data Analysis
The game exports behavioral metrics in JSON format. To analyze these files, use the script provided in the /Json-Explorer directory.

https://habit-game-json-explorer.streamlit.app/

## 🌐 Playing the WebGL Build
The latest stable version is located in the /WebGame folder and can be played here: https://ambervoskamp.github.io/HabitGame/WebBuild/

Note: Most browsers will not run WebGL files directly from your local file system (file://) due to security restrictions. To test locally, use a local server like VS Code Live Server or Python's built-in module:

Bash
cd WebGame
python -m http.server 8000
Then visit localhost:8000 in your browser.

## 🛠 Built With
Unity - Game Engine

Python - Data Processing

Newtonsoft.Json - JSON handling in Unity
