# Simple sticky note with physics scenario

## Enviroment
<p>A small square room with a desk in the middle of the room. The desk holds a monitor, keyboard, and mouse. 
Underneath the keyboard is a sticky note with the password on it.</p>

## Task
<p>Obtain access to the computer by finding the password. Introduces picking up, moving, and interacting with objects to solve problems.</p>

## User Prompt
<p>Find a way to login to the computer.<br>
See what you can do to objects in the room.</p>

## Interactibility
- Keyboard:
    - Can be picked up
    - Can be interacted to pull up a keyboard hologram for user input
        - This might be tricky to implement and design.
        - [Rough Reference image](https://i.pinimg.com/originals/ba/3a/56/ba3a5623d90e4f060328ab5b47239ccd.jpg)
- Mouse:
    - Can be picked up
- Sticky Note:
    - Can be picked up
    - Can be sticked to objects

## Solution
<p>Pick up the keyboard and look at sticky note at the bottom. Read the password off of the sticky note and interact with the keyboard to input the password to login.</p>

## Notes
<p>Maybe interacting with the monitor should bring up the VR keyboard instead of the keyboard object. It would be act more similiarly to touch screens,
which pull up a interactible keyboard when text input sections are touched. This might be more familiar to users. It also might be more natural to not
have the keyboard both be able to be picked up and bring up a UI window.<br>
Still, it might be more logical to users that the keyboard is the object that is used to write text.<p>
