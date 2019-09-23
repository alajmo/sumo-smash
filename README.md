# Sumo Smash

A push/shoot based multiplayer game.

## Get Started

1. Clone this repo.
2. Download Unity version 2019.3 (we attempt to always stay up to date with the latest stable version).

## Unity Workflow

- We use separate scenes for debugging features. For instance, debugging the food spawn can be done in the [food scene](Assets/Scenes/DebugScenes/FoodScene.unity).
- To work on a feature or scene, you need to lock that scene so other people don't modify it since there are issues with people working on the same scene, for now just tell the other group members.
- The [DemoScene](Assets/Scenes/DemoScene.unity) is special. It's our first level and should always work (so no experimentation on this scene please!). If there is a need to combine features, copy and modify the duplicate scene.
- Check the [DemoScene](Assets/Scenes/DemoScene.unity) to see how we structure the game objects in the hierarchy view.

## Task Workflow

We use a task system with 5 columns to manage work:

- **Backlog**: Put all ideas here.
- **Iteration**: The current iteration.
- **Doing**: What each member is doing, ideally keep 1 thing per person in this column.
- **Blocked**: Cards which are blocked for some reason.
- **Done**: Cards that are finished (or if we decide to not implement card content, they are moved here as well).

Each card can be labelled (multiple labels allowed):

- **Feature**: Some programmable feature, for instance adding shooting, a new event, etc.
- **Fix**: Fixing a bug
- **Design**: Anything related to visual design
- **Task**: Arbitrary task, can be used for upgrading Unity, update some build script, etc.
- **Discussion**: Something that needs to be decided
- **Documentation**: Used whenever we're documentation something

## IDE

We use Visual Studio Code with the following plugins:

- C#
- Debugger for Unity
- Unity Code Snippets
- Unity Tools

## Design

We use [Blender](https://www.blender.org) to design 3D assets.

## Resources

- [Git Repository](https://github.com/samiralajmovic/sumo-smash)
- [Kanban Board](https://trello.com/b/rnP3Svl5/sumo-smash)
- [Game Document](https://docs.google.com/document/d/1gLZYPnvzvLzPf7-3hBPBq20SQTVK89vb4v9NcLhahVw/edit#heading=h.bgn957jq1e3p)
- [Telegram chat](https://web.telegram.org/#/im?p=g364557207)
