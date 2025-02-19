How to run the project:
Load MainScene, turn on sound and press play. 


The main + additional key features I will described as a full game cycle. 
At the beginning of the game, the user can pick up a fishing pole by using raycast interactor.  User transfer is available via helmet (W\S\A\D in simulator or teleportation (W in simulator)  + rotation of camera (A\D in simulator) on right controller. 
The backlight of the raycast and the outline of interactive objects have been added for a better understanding with which objects can interact. User can also pick up bait (bucket with worms) that have similar behavior. When bait is neat hook it`s set on hook. (For debug through simulator I keep the possibility to move grabbed bait on W/S).As soon as the fishing pole is in hand and the bait has been taken, the hook’s outline changes. Green if user can still attach the bait and red if there is no place for the bait. This gives the user an understanding of what to do with the bait and why it cannot repeat its action at a certain moment. After that, the user can go to the water and start fishing. To start the casting user have to press the button (B in the simulator). Then depending on how quickly and at what angle the user cast fishing pole, the bobber will cast far or near. If there is no bait on the hook, fishing will not start. If there is bait, the fishing process will be started, but can be interrupted by early reeling in. Reeling in happens if you grab the handle ( R in the simulator) and then start spinning the controller, but for more convenient use while fishing through the simulator, I made debugging inputs such as on the space reel out and on the right ctrl reel in.
At the moment of entering the water, logic that simulating ripples/floating/drifting by the bobber started. For fish, the state machine is started. First it goes to the hook. Then, depending on the attraction of the bait and its parameters with different times and different count  begins to bite the bait. After that attracted to hook and start to pull. While biting also ripples are left for a better understanding of the user when the fish has bitten. As soon as the fish starts pulling, the user has to raise the fishing rod a little, turn it in the direction of the catch and reel in. The understanding of correct execution of actions is the responsibility of the appeared toolbar. 
If the fishing was successful, the user sees the fish on the hook and gets pop-up with the results. After pressing the continue button, the fish is moved to the bucket and user can repeat the fishing. If the fishing failed, the user also sees a pop-up with appropriate title and can repeat the cycle of fishing after pressing the continue button. The total earned points can be viewed on the hand menu by turning the left controller 180 degrees.
It was important to note that all parameters can be easily changed as they are written in the configs. Several species of fish have been implemented, but as a best option, the system allow to change not only the visual and parameters, but also the behavior, it just takes more time. Also, I have inserted the phishing pole system through changing of its parts. Such modularity would be more interesting system of the fishing pole level up. 
Also, now interactive water is limited due to implementation. Firstly, the water reacts to the particle system, that requires higher speed and additional settings to create beautiful waves. But you can test it by starting the game and moving in the scene of the particle system of bobber. Also, the system works with a RenderMap, which does not allow simple copying of the plane.
Also, the fishing line now calculates on the Bezier curve. Because of this sometimes the line looks wrong. I also write the logic of bending in the opposite direction, so that the light is bent down, but maybe the situation, then the opposite side will become a top, although it rarely reproduced.
