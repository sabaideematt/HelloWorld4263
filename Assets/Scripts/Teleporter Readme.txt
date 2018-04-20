Create a new GameObject to represent the trigger area. 
Choose the most appropriate shape. 
This will be the area that you have to enter in order to cause the teleport to occur.

Now select the new object you just made, and tick the "Is Trigger" option in the 
collider component, shown in the inspector pane. 
This changes the collider from a solid object to an area that you can walk into.

Un-tick the "Mesh Renderer" component, so that the GameObject becomes invisible.

You probably also want to specify a destination for the teleporter, 
and an easy way to do this is to create another invisible GameObject. 
This time however you don't need a collider either, so it's best to use "Create Empty"
 to create a completely empty GameObject. Name it "Teleport Destination".