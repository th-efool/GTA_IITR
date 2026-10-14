# GTA IITR

<p align="center">
  <strong>What if IIT Roorkee had its own GTA?</strong><br />
  An open Unity game project bringing the IIT Roorkee campus into a playable 3D world.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Engine-Unity-000000?logo=unity&logoColor=white" alt="Engine: Unity" />
  <img src="https://img.shields.io/badge/Unity-6000.0.11f1-000000?logo=unity&logoColor=white" alt="Unity version 6000.0.11f1" />
  <img src="https://img.shields.io/badge/Render%20Pipeline-URP-5A5A5A" alt="Universal Render Pipeline" />
  <img src="https://img.shields.io/badge/Status-In%20Development-orange" alt="In development" />
  <a href="https://github.com/th-efool/GTA_IITR"><img src="https://img.shields.io/github/last-commit/th-efool/GTA_IITR" alt="Last commit" /></a>
  <a href="https://github.com/th-efool/GTA_IITR/issues"><img src="https://img.shields.io/github/issues/th-efool/GTA_IITR" alt="GitHub issues" /></a>
</p>

**GTA IITR** is a Unity game project built around a simple, fun idea: reimagine IIT Roorkee as an explorable game world. Instead of a fictional city, the setting is inspired by the campus itself. It combines 3D environment assets with character and animation assets to build the foundation for a campus-scale playground.

This is a student-built project and a work in progress. The repository contains the Unity project and its assets; the exact gameplay available may vary by project revision.

## At a glance

- **Project:** GTA IITR
- **Engine:** Unity
- **Unity version:** 6000.0.11f1
- **Render pipeline:** Universal Render Pipeline (URP)
- **Platform:** Unity Editor project; standalone build targets depend on the current configuration
- **Status:** Work in progress

## The idea

IIT Roorkee is already a world full of recognizable roads, buildings, hangout spots, vehicles, and people. GTA IITR takes that familiar setting and turns it into a game-development experiment: how much of that campus feeling can we bring into an interactive 3D world?

The goal is to make the campus itself the attraction — a place players recognize, explore, and experience from a different perspective.

## What’s in the project?

The repository currently includes assets and project configuration for several parts of that experience:

### Campus-inspired environment
- 3D road-system and campus-map models.
- Building and environment assets.
- Imported `.glb` models used by the project.

### Vehicles and movement
- Vehicle-related assets, including car and buggy models.
- Project assets associated with vehicle movement.

### Characters and animation
- Character and NPC assets.
- Animation clips and animation controllers for movement, idle states, and other character actions.

### Game project
- Unity scenes configured in the build settings, including a main-menu scene and a practice scene.
- Input System configuration.
- URP, Cinemachine, AI Navigation, Animation Rigging, Shader Graph, and other Unity packages declared by the project.

> These are repository-level contents, not a promise that every listed asset is wired into a complete gameplay loop.

## Screenshots and gameplay

Screenshots and gameplay footage are the best way to show the project. Add images or a short clip here as they become available.

<!-- Suggested:
![Campus world](docs/images/campus-world.png)
![Gameplay](docs/images/gameplay.png)
-->

## Getting started

### Requirements

- **Unity Hub**
- **Unity Editor 6000.0.11f1**
- Git
- Enough disk space for the Unity project and its 3D assets

The repository includes large imported models, so cloning and importing the project may take a while.

### Open the project

1. Clone the repository:

   ```bash
   git clone https://github.com/th-efool/GTA_IITR.git
   ```

2. Open **Unity Hub**.
3. Select **Add** / **Add project from disk** and choose the cloned `GTA_IITR` folder.
4. Open the project with **Unity 6000.0.11f1**.
5. Let Unity resolve packages and import assets. The first import may take some time.
6. Open the configured scenes from the Project window:
   - `Assets/MainMenu/MainMenu.unity`
   - `Assets/Scenes/PracticeScene.unity`
7. Press **Play** to test a scene in the Editor.

If a scene does not start as expected, check the Console for missing assets, package-resolution errors, or input configuration issues.

## Build and run

The repository is a Unity Editor project. To create a standalone build:

1. Open the project in the matching Unity version.
2. Open **File → Build Profiles** (or **Build Settings**, depending on the Editor UI).
3. Select the platform you want to target.
4. Confirm that the intended scenes are included and enabled.
5. Build and test on the target platform.

A successful build depends on the current scene setup, platform settings, and any required assets or plugins. No prebuilt release is documented here yet.

## Project structure

A simplified view of the repository:

```text
GTA_IITR/
├── Assets/
│   ├── Animations/       # Character and NPC animations/controllers
│   ├── Buildings/        # Building/environment assets
│   ├── Characters/       # Character assets
│   ├── CarMvmt/          # Vehicle-movement-related assets
│   ├── *.glb             # Imported 3D models
│   └── ...
├── Packages/
│   ├── manifest.json     # Unity package dependencies
│   └── packages-lock.json
├── ProjectSettings/
│   ├── EditorBuildSettings.asset
│   └── ProjectVersion.txt
├── LICENSE
└── README.md
```

The full `Assets/` directory contains more resources than shown above; this overview highlights the main categories rather than listing every file.

## Built with

The project uses Unity and the packages declared in `Packages/manifest.json`, including:

- **Universal Render Pipeline (URP)** — rendering.
- **Input System** — input configuration.
- **Cinemachine** — camera tooling.
- **AI Navigation** — navigation support.
- **Animation Rigging** — character-rigging tools.
- **Shader Graph** — shader authoring.
- **glTFast** — glTF/GLB asset support.

These packages are part of the project configuration; their presence does not necessarily mean every package is used by every scene.

## Current status

GTA IITR is a work in progress. The repository provides the Unity project, campus/environment models, vehicle-related assets, and character animation resources needed to develop the experience further.

The most useful next steps for the project are to document the controls, add real in-game screenshots, describe the gameplay loop that is currently implemented, and provide a tested downloadable build when one is ready.

## Contributing

Contributions, feedback, and ideas are welcome.

1. Fork the repository.
2. Create a branch for your change.
3. Make and test your changes in Unity.
4. Open a pull request describing what changed and how it was tested.

For large asset additions, keep file sizes and licensing in mind. Avoid committing generated caches or local Editor files.

## Assets and credits

The project contains 3D models, animations, fonts, and other assets. Please check the relevant asset source and license before redistributing individual resources. The repository's top-level license may not automatically cover third-party assets bundled with the project.

## Disclaimer

GTA IITR is an independent, student-made project inspired by IIT Roorkee. It is not affiliated with, endorsed by, or an official product of IIT Roorkee or Rockstar Games. Grand Theft Auto is a trademark of its respective owner; the name is used here to communicate the project's game concept.

## License

See [LICENSE](LICENSE) for the repository's license. Third-party assets may be subject to separate terms.
