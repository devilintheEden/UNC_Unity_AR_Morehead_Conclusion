# UNC_Unity_AR_Morehead_Conclusion
UNC Morehead Unity AR Project's conclusion.

- Slide Puzzle. ( n * n )
  - Can control n with *((MapCreate)GameObject.FindWithTag("scaler").GetComponent(typeof(MapCreate))).n* in the scene.
  - n should be among 2,3,4,5, or it will not be enough space.
  - Warning: the image used to form the fragment of the silde puzzle should always satisfy height >= width.
- Match Puzzle.
  - Two modes. Four pairs or Eight pairs. 
  - Can switch bewteen modes using boolean *((GameInstant)GameObject.FindWithTag("scaler").GetComponent(typeof(GameInstant))).isFour*.
  - demo: https://youtu.be/F5wt2Q-p38A.
- Stone Broke.
  - Need to aim at a plane to place the stone.
  - Find the golden crack and click on it three times to reveal the hint.
  - demo(THIS IS NOT THE FINAL VERSION): https://youtu.be/sC7hc-RaRQc.
- Other.
  - This project need to set up the AR Foundation etc. in the package manager for it to work.
  - Resolution and UI is set to 1024 * 768 (IPAD MINI SIZE).
  - All three games is triggered by different AR images detection.
