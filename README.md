## UNC_CARVR_Morehead_MiniGame

#### Full Demo: [Youtube Link](https://youtu.be/-wKncfcl2hw)

#### Assets Structure:

##### Ind_demo_only (indepedent demo only):

- DataSaved / DataSavedScript:
  - Contains a **Texture[8] matchPuzzle_blocksTexture** where the Match Puzzle gets the texture it will use.
  - It has to be put outside of the minigames scale because it is dynamically generated and will change if the person visited the potraits in different orders.
  - <u>Should be placed in the scene before it runs.</u>
  - <u>Tagged 'data'.</u>
- ImageDetected:
  -  Three games are triggered by Tracking of different images.

##### MiniGameAssets: 

No need to change. Just Drag in. <u>Scripts use Namespace 'MiniGames'.</u>

##### Resources:

- Painting_Arts:
  - Photos of the 11 potraits.
  - No need to put all of them in the Resources folder if the patterns of match puzzle are assigned to existing textures.
  - <u>The one that assigns to the sliding puzzle must be put in the Resources folder and it should be 'Read/Write Enabled' and 'Override for IOS' with format 'RGB 24 bit'.</u>
- StoneBroke_Texture_Arts:
  - Just Drag into the Resources folder.

##### RootPrefabs:

- MatchPuzzleGamePrefab:
  - Two modes. Four pairs or Eight pairs.s
  - Can change modes (**isFour**) / time limit (**duration**) / victory message (**hint**) in the inspector.
  - Can also change them in code accessing the Prefab through **GameObject.FindWithTag("rootPrefab")**.
    - i.e. `((GameInstant)GameObject.FindWithTag("rootPrefab").GetComponent(typeof(GameInstant))).isFour`.
  - <u>4 pairs - 10 seconds / 8 pairs - 25 seconds</u> should be good.
  - Have exit button to exit the minigames. Can restart by scanning the image once more.
  
- SlidePuzzleGamePrefab:

  - n * n blocks. n should be among 3 or 4, or it will not be too easy or too difficult.
  - Can change n (**n**) / target image (**full_image**) / victory message (**hint**) in the inspector.
  - Can also change them in code accessing the Prefab through **GameObject.FindWithTag("rootPrefab")**.
    - i.e. `((MapCreate)GameObject.FindWithTag("rootPrefab").GetComponent(typeof(MapCreate))).n`.
  - <u>The image used to form the silde puzzle should always satisfy height >= width.</u>
  - Have exit button to exit the minigames. Can restart by scanning the image once more.

- StoneBrokeGamePrefab:
  - <u>Need to aim at a plane to place the stone.</u>
  - Find the golden crack and click on it three times to reveal the hint. (have sound effect)
  - Can change hint (**hint**) in the inspector.
  - Can also change it in code accessing the Prefab through **GameObject.FindWithTag("rootPrefab")**.
  
  - Have exit button to exit the minigames. Can restart by scanning the image once more.

#### Other:

- Dependency:

    ```
    "com.unity.xr.arcore": "3.0.1",
    "com.unity.xr.arfoundation": "3.0.1",
    "com.unity.xr.arkit-face-tracking": "3.0.1",
    "com.unity.xr.arsubsystems": "3.0.0"
    ```

- UI is set to a ratio of 4:3 (IPAD MINI SIZE). (Thus may look strange in the demo on a 16:9 phone).
- <u>*AR Session Origin* in the scene should have *AR Plane Manager* and be tagged 'ARSessionOrigin'. Detection mode would better be Horizontal only.</u>
- *AR camera* should better have an *Audio Listener*. 

