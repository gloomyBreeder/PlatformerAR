# About
This game is my first attempt to play with [ARCore](https://developers.google.com/ar/) (*basically I worked with ARCore and ARKit before in [Underverse](http://ambidexter.io/uv/) but didn't do it only on my own*).

I made a 3D Platformer in [Unity](https://unity.com/) where main character named **Boy** travels through different portals located on floating islands to find his dog named **Doggo**. The purpose of each level is to collect all **cat-loaf** (yeah, I like cats look like loafs and loafs look like cats) coins in order to open portal and travel to next level. When portal is open **Doggo** appears near it but as **Doggo** is very curious and always likes running from its owner **Boy** can't catch it.   

Click [here](https://youtu.be/slwWyz_KflI) or on screenshot to watch video:

[![screenshot_1](http://www.picshare.ru/uploads/190718/2PP4dWqkT3.jpg)](https://youtu.be/slwWyz_KflI)

### !Important: this game requires support of AR so don't forget to check if your device is supported or not [here](https://developers.google.com/ar/discover/supported-devices) before build project and play.

# Gameplay
**Boy** can run by moving joystick and jump by pushing jump button. The level can be scaled and rotated for easier and more comfortable playing experience.  There are a few invisible checkpoints located in the level to save **Boy's** position if he falls down.

It was a challenge to place a big scene in AR and make it not very difficult to travel through it. That's why I decided to help player manipulate AR by scalling and rotating so basically he or she can just sit on the chair and play. No move around!

![screenshot_2](http://www.picshare.ru/uploads/190718/0f0Fcdoay4.jpg)

Second challenge  was to make **Boy** always move and rotate in that direction a player wants. His movement is coordinated by AR camera like so:
```csharp
Vector3 camForward = Camera.transform.forward;
Vector3 camRight = Camera.transform.right;
    
camForward.y = 0;
camRight.y = 0;
    
camForward = camForward.normalized;
camRight = camRight.normalized;
    
Vector3 horDirection = (Joystick.Horizontal + Input.GetAxis("Horizontal")) * camRight;
Vector3 vertDirection = (Joystick.Vertical + Input.GetAxis("Vertical")) * camForward;
    
_moveDirection = horDirection * 1f + vertDirection * 1f;
```
Implementation of rotation and the whole character script can be found in `Assets/ARDemo/Scripts/MainCharacterController.cs`

![screenshot_3](http://www.picshare.ru/uploads/190718/pBhfE27w7H.jpg)

# Links

All 3D objects (some of them I transformed for my needs) are made by different modellers from **Sketchfab** (all are [CC BY 4.0](https://creativecommons.org/faq/)):
* https://sketchfab.com/3d-models/castle-stairs-cdeb62b1161e450ea5a5854079aad7b2 
* https://sketchfab.com/3d-models/lowpoly-gold-coin-34794c00e9d140f6b86e930fef18b009
* https://sketchfab.com/3d-models/floating-island-b776c049c25846168640cbe13ed4e658
* https://sketchfab.com/3d-models/low-poly-character-rpg-kit-animation-904c06fc53574534a3aace8dba79f796
* https://sketchfab.com/3d-models/mtn-95fbfff57f7b486290eb0d66f0a923bb
* https://sketchfab.com/3d-models/low-poly-doggy-1c8c763518ab4751bfcddf0b6a34011a
* https://sketchfab.com/3d-models/low-poly-models-for-a-game-75f1eb69f8284cbea6c33f5afb7e759a
* https://sketchfab.com/3d-models/lowpoly-nature-1-a8ebe301e9864a85ac00d9fed5ad8496
* https://sketchfab.com/3d-models/low-poly-nature-493b488007f14e3b98502ccc60f78582

#### Music:
Downtempo Maniac - Laidback Piano

----
### Feel free to contact me if you have any questions/comments.
### Thanks for attention!

![screenshot_4](http://www.picshare.ru/uploads/190718/9F8lGei256.jpg)

<a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc/4.0/80x15.png" /></a><br />This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/">Creative Commons Attribution-NonCommercial 4.0 International License</a>.
