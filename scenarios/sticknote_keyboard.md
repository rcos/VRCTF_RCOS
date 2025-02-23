# Scenario 1: Sticky note & Keyboard

## Difficulty: Easy

## Environment
<p>A small square room with a desk in the middle of the room. The desk holds a monitor, keyboard, and mouse. 
Underneath the keyboard is a sticky note with the password on it.</p>

## Task
<p>Obtain access to the computer by finding the password. Introduces picking up, moving, and interacting with objects to solve problems.</p>

## User Prompt
<p>Find a way to log in to the computer.<br>
See what you can do to objects in the room.</p>

## Interactivity
- Keyboard:
    - Can be picked up
    - Has sticky note on the back with the password
- Monitor:
    - Can be interacted to pull up a keyboard hologram for user input
            - This might be tricky to implement and design.
            - [Rough Reference image](https://i.pinimg.com/originals/ba/3a/56/ba3a5623d90e4f060328ab5b47239ccd.jpg)
- Mouse:
    - Can be picked up

## Solution
<p>Pick up the keyboard and look at sticky note at the bottom. Read the password off of the sticky note and interact with the keyboard to input the password to login.</p>

## Notes
<p>Maybe interacting with the monitor should bring up the VR keyboard instead of the keyboard object. It would be acting more similarly to touch screens,
which pull up an interactive keyboard when text input sections are touched. This might be more familiar to users. It also might be more natural to not
have the keyboard both be able to be picked up and bring up a UI window.<br>
Still, it might be more logical to users that the keyboard is the object that is used to write text.<p>

## Script
Setting: User starts in the middle of the room. A desk, monitor, keyboard, and mouse is front of the user. 

Step 1: Examine the desk and other objects to look for hints. The password should on the sticky note at back of the keyboard. 
Note: The keyboard and sticky note can be one object.

Step 2: Once the user finds the password, interact with the monitor to enter the password. The monitor screen should display: "Press to enter password here". Press on the monitor. 

Step 3: User is then redirected to another scene with a popup keyboard, which have keys that can individually be viewed and interacted to input the password. There is also an exit button near the keyboard, in case the user wants to go back to the room. 

Password: CTF_wewillchangethislater
