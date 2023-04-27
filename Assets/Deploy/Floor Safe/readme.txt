This realistic floor safe is structured with movable parts and has been rigged with physics components and an LOD Group.
If you don't want to use the LOD Group, you can remove the "LOD Group" component from the root of the object  (called "Floor Safe") in the Inspector panel.
The safe has been outfitted with colliders and has two rigidbodies (one on the root of the object, and one on the "Door" part), connected by a hinge joint. The rigidbodies are set to kinematic by default. If you want the safe to behave as a physics-affected object, uncheck "Is Kinematic" on the Rigidbody components of the root object (called "Floor Safe") and the "Door" part.
To open the door, decrease the Y-axis rotation value for the "Door" part.
To turn the dial, adjust the Z-axis rotation value for the "Dial" part (a child of the "Door" part).
To turn the handle, adjust the Z-axis rotation value for the "Handle" part (a child of the "Door" part).

Thanks, from all of us at GriffinDev!
GriffinDev.com
