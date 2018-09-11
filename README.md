# CheckersPolygon
![screenshot 1](https://image.ibb.co/iJUxfU/Screen_Shot_20180911223822.png "Screenshot")
![screenshot 2](https://image.ibb.co/m1HTn9/Screen_Shot_20180911223925.png "Screenshot")
## Executable
[Download executable][exe]
## Rules
The game follows the rules of Russian checkers. Simple checkers go only 1 cell ahead and can beat back. The king walks the entire length of the board in any direction.
## Ai
There were no requirements for the complexity of AI, so I decided to make it as simple as possible. He performs a search for all available moves that meet the rules and randomly chooses the move without looking for benefits.
## Features
* Calculation of all possible moves for each checker
* Storing the state of the game, allowing you to easily save and load the game, organize a multiplayer game (not yet implemented), or cancel the move
* The architecture of the game allows you to untie it from Windows Forms and use another framework for visualization
* The general algorithm for calculating the stroke for the checker and the king allows you to change the size of the board

[exe]:https://my-files.ru/y2pkws
