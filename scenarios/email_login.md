# Scenario 2: Email Login

## Difficulty: Medium

## Environment

A small personal room with a desk, which holds a monitor, keyboard, and a mouse. There is wall decor above the desk, including a calendar and some photos.

## Task

Obtain access to the target's computer and email account by looking for clues around the desk.

## User Prompt

Log into the target's computer and then their email account. See what you can find in their emails. Use clues around the desk to help figure out the passwords.

## Interactivity

- **Keyboard**: Can be picked up
- **Monitor**: Can be interacted with to pull up a keyboard hologram for user input
  - This might be tricky to implement and design.
  - [Rough Reference image](https://i.pinimg.com/originals/ba/3a/56/ba3a5623d90e4f060328ab5b47239ccd.jpg)
  - Click and interact as you would with a real computer
- **Mouse**: Can be picked up

## Solution

1) Find the password to the computer lock screen, which is the name of the target's pet.  
2) Find the password to the email account, which is their best friend's birthday.  
3) Go through the emails and find the email named 'CTF'.

## Notes

For this challenge, we need to make the VR monitor operate like a real computer, especially with the web browser/pages. This can be hard to implement.

## Script

**Setting**: User starts in the middle of the room. A desk, monitor, keyboard, and mouse are in front of the user. 

**Step 1**: Examine the desk and other objects to look for hints, especially information displayed on the wall. There should be a calendar and some photos on the wall near the desk.

**Step 2**: The monitor is open and at its lock screen. Interact (click) with it to reach the sign-in screen. 

**Step 3**: Look for clues for the password to gain access to the desktop. Just below the space where you enter the password, there should be a hint: "my fav pet".  
- There should be photos of a cat near the wall with its visible name (e.g. snowball) on its collar. 

**Step 4**: The monitor screen should display: "Press to enter password here". Once the user figures out the password, enter it. 
- When entering the password, the user is redirected to another scene with a popup keyboard, where keys can individually be viewed and interacted with to input the password.  
- There is also an exit button near the keyboard in case the user wants to go back to the room.

**Step 5**: After the sign-in password is successfully input, the computer screen unlocks to an email login page. The target's email address is already autofilled, so it's time to find a second password for the email.  
- Just below the space where you enter the password, there should be a hint: "my bestfriend's birthday". 
- A circled message is marked on the calendar: "Vivi's 20th birthday!"  
- The month and year displayed on the calendar are currently February and 2025, respectively.  
- The birthday password should be in the form: month, day, year (00-00-0000). So in this case, the password is `02052005`.

**Step 6**: Once the user unlocks access to the email account, go to Mail and check the emails. There is an email among a few with the title 'CTF'. After clicking the email, it displays the CTF code.  

Example: `CTF_calender_Addict`
